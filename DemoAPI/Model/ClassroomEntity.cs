using System.ComponentModel.DataAnnotations;

namespace DemoAPI.Model
{
    public class ClassroomEntity
    {
        [Key]
        public int ClassroomId { get; set; }
        [Required]
        public string ClassName { get; set; }
        [Required]
        public string Division { get; set; }
        [Required]
        public string TeacherName { get; set; }
        [Required]
        public string Location { get; set; }
        
        public ICollection<StudentEntity> Students { get; set; }

    }
}
