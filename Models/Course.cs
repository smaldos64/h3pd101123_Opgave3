namespace Opgave3.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public ICollection<StudentCourse> StudentCourses { get; set; }
              = new List<StudentCourse>();
    }
}
