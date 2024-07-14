using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DemoAPI.Data;
using DemoAPI.Model;
using System.Collections.Generic;
using System.Linq;

namespace DemoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassroomsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<ClassroomsController> _logger;

        public ClassroomsController(ApplicationDbContext context, ILogger<ClassroomsController> logger)
        {
            _db = context;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ClassroomEntity>> GetAllClassrooms()
        {
            _logger.LogInformation("Fetching all classrooms.");
            var classrooms = _db.Classrooms.ToList();
            return Ok(classrooms);
        }

        [HttpGet("{id}")]
        public ActionResult<ClassroomEntity> GetClassroomById(int id)
        {
            _logger.LogInformation($"Fetching details for classroom with ID {id}.");
            var classroom = _db.Classrooms.FirstOrDefault(c => c.ClassroomId == id);
            if (classroom == null)
            {
                _logger.LogWarning($"Classroom with ID {id} not found.");
                return NotFound();
            }
            return Ok(classroom);
        }

        [HttpPost]
        public ActionResult<ClassroomEntity> AddClassroom([FromBody] ClassroomEntity classroom)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid classroom data.");
                return BadRequest(ModelState);
            }

            //Ensures the students collection is initialized if it's null
            //classroom.Students = classroom.Students ?? new List<StudentEntity>();

            _db.Classrooms.Add(classroom);
            _db.SaveChanges();
            _logger.LogInformation($"Classroom with ID {classroom.ClassroomId} created.");

            return CreatedAtAction(nameof(GetClassroomById), new { id = classroom.ClassroomId }, classroom);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateClassroom(int id, [FromBody] ClassroomEntity classroom)
        {
            if (classroom == null || id != classroom.ClassroomId)
            {
                _logger.LogWarning("Invalid classroom update data.");
                return BadRequest();
            }

            var classroomInDb = _db.Classrooms.FirstOrDefault(c => c.ClassroomId == id);
            if (classroomInDb == null)
            {
                _logger.LogWarning($"Classroom with ID {id} not found for update.");
                return NotFound();
            }

            classroomInDb.ClassName = classroom.ClassName;
            classroomInDb.Division = classroom.Division;
            classroomInDb.TeacherName = classroom.TeacherName;
            classroomInDb.Location = classroom.Location;

            _db.SaveChanges();
            _logger.LogInformation($"Classroom with ID {id} updated.");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteClassroom(int id)
        {
            var classroomInDb = _db.Classrooms.FirstOrDefault(c => c.ClassroomId == id);
            if (classroomInDb == null)
            {
                _logger.LogWarning($"Classroom with ID {id} not found for deletion.");
                return NotFound();
            }

            _db.Classrooms.Remove(classroomInDb);
            _db.SaveChanges();
            _logger.LogInformation($"Classroom with ID {id} deleted.");

            return NoContent();
        }
    }
}
