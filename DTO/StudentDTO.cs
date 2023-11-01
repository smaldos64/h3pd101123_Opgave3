using Opgave3.Models;

namespace Opgave3.DTO
{
    public class StudentForSaveDTO
    {
        public string StudentName { get; set; }
        public int StudentAge { get; set; }
        public int TeamId { get; set; }
    }

    public class StudentForUpdateDTO : StudentForSaveDTO
    {
        public int StudentId { get; set; }
    }

    public class StudentDTO : StudentForUpdateDTO
    {
        public TeamDTOMinusRelations Team { get; set; }
        public ICollection<StudentCourseMinusStudentDTO> StudentCourses { get; set; }
              = new List<StudentCourseMinusStudentDTO>();
    }

    public class StudentDTOMinusTeamRelations : StudentForUpdateDTO
    {
        public ICollection<StudentCourseMinusStudentDTO> StudentCourses { get; set; }
              = new List<StudentCourseMinusStudentDTO>();
    }

    public class StudentDTOMinusStudentCourseRelations : StudentForUpdateDTO
    {
        public TeamDTOMinusRelations Team { get; set; }
    }
}
