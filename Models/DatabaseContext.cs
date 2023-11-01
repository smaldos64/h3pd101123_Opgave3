using Microsoft.EntityFrameworkCore;
using Opgave3.Models;
using System.Reflection.Metadata;

namespace Opgave3.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<StudentCourse>()
            //    .HasKey(cl => new
            //    {
            //        cl.StudentId,
            //        cl.CourseId
            //    });

            //base.OnModelCreating(modelBuilder);
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
    }

}
