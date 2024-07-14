using Microsoft.EntityFrameworkCore;
using DemoAPI.Model;

namespace DemoAPI.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<StudentEntity> StudentRegister { get; set; }
        public DbSet<ClassroomEntity> Classrooms { get; set; }
    }
}
