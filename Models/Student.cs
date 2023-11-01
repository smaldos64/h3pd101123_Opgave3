namespace Opgave3.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int StudentAge { get; set; }

        public int TeamId { get; set;}
        public Team Team { get; set; }

        public ICollection<StudentCourse> StudentCourses { get; set; }
              = new List<StudentCourse>();
    }
}
