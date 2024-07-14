using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DemoAPI.Data;
using DemoAPI.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace DemoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<StudentsController> _logger;

        public StudentsController(ApplicationDbContext context, ILogger<StudentsController> logger)
        {
            _db = context;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<StudentEntity>> GetAllStudents()
        {
            _logger.LogInformation("Fetching All Student List");
            var students = _db.StudentRegister.Include(s => s.Classroom).ToList();
            return Ok(students);
        }

        [HttpGet("GetStudentsById/{id:int}")]
        public ActionResult<StudentEntity> GetStudentDetails(int id)
        {
            if (id == 0)
            {
                _logger.LogError("Student Id was not passed");
                return BadRequest();
            }

            var studentDetails = _db.StudentRegister.Include(s => s.Classroom).FirstOrDefault(x => x.Id == id);

            if (studentDetails == null)
            {
                return NotFound();
            }
            return Ok(studentDetails);
        }

        [HttpPost]
        public ActionResult<StudentEntity> AddStudent([FromBody] StudentEntity studentDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.StudentRegister.Add(studentDetails);
            _db.SaveChanges();

            return CreatedAtAction(nameof(GetStudentDetails), new { id = studentDetails.Id }, studentDetails);
        }

        [HttpPut("UpdateStudentDetails/{id:int}")]
        public ActionResult<StudentEntity> UpdateStudent(int id, [FromBody] StudentEntity studentDetails)
        {
            if (studentDetails == null || id != studentDetails.Id)
            {
                return BadRequest();
            }

            var studentInDb = _db.StudentRegister.FirstOrDefault(x => x.Id == id);
            if (studentInDb == null)
            {
                return NotFound();
            }

            studentInDb.Name = studentDetails.Name;
            studentInDb.Age = studentDetails.Age;
            studentInDb.EmailAddress = studentDetails.EmailAddress;
            studentInDb.DateOfBirth = studentDetails.DateOfBirth;
            studentInDb.Address = studentDetails.Address;
            studentInDb.PhoneNumber = studentDetails.PhoneNumber;
            studentInDb.ClassroomId = studentDetails.ClassroomId;

            _db.SaveChanges();

            return NoContent();
        }

        [HttpDelete("DeleteStudent/{id:int}")]
        public ActionResult<StudentEntity> Delete(int id)
        {
            var studentInDb = _db.StudentRegister.FirstOrDefault(x => x.Id == id);
            if (studentInDb == null)
            {
                return NotFound();
            }

            _db.StudentRegister.Remove(studentInDb);
            _db.SaveChanges();

            return NoContent();
        }


        [HttpGet("GetStudentsByClassAndDivision")]
        public ActionResult<IEnumerable<StudentEntity>> GetStudentsByClassAndDivision(string className, string division)
        {
            var students = _db.StudentRegister
                              .Include(s => s.Classroom)
                              .Where(s => s.Classroom.ClassName == className && s.Classroom.Division == division)
                              .ToList();

            if (students == null || students.Count == 0)
            {
                return NotFound();
            }

            return Ok(students);
        }

    }
}
