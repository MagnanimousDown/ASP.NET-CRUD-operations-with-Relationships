using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DemoAPI.Model
{
    public class StudentEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string Address  { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

        [ForeignKey("Classroom")]
        public int ClassroomId { get; set; }

        [JsonIgnore]
        public ClassroomEntity? Classroom { get; set; }
    }
}
