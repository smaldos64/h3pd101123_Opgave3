namespace Opgave3.DTO
{
    public class CourseForSaveDTO
    {
        public string CourseName { get; set; }
    }

    public class CourseForUpdateDTO : CourseForSaveDTO
    {
        public int CourseId { get; set; }
    }

    public class CourseDTO : CourseForUpdateDTO
    {
        public ICollection<StudentCourseMinusCourseDTO> StudentCourses { get; set; }
              = new List<StudentCourseMinusCourseDTO>();
    }

    public class CourseDTOMinusRelations : CourseForUpdateDTO
    {

    }

}
