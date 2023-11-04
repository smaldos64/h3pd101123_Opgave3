using System.ComponentModel.DataAnnotations.Schema;

namespace Opgave3.DTO
{
    public class StudentCourseForSaveDTO
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public int CharacterId { get; set; }
        public int YearForCharacter { get; set; }
    }

    public class StudentCourseForUpdateDTO : StudentCourseForSaveDTO
    {
        public int StudentCourseId { get; set; }
    }

    public class StudentCourseMinusStudentDTO : StudentCourseForUpdateDTO
    {
        public CourseDTOMinusRelations Course { get; set; }
        public CharacterDTOMinusRelations Character { get; set; }
    }

    public class StudentCourseMinusCourseDTO : StudentCourseForUpdateDTO
    {
        public StudentDTOMinusStudentCourseRelations Student { get; set; }
        public CharacterDTOMinusRelations Character { get; set; }
    }

    public class StudentCourseMinusCharacterDTO : StudentCourseForUpdateDTO
    {
        public StudentDTOMinusStudentCourseRelations Student { get; set; }
        public CourseDTOMinusRelations Course { get; set; }
    }

    public class StudentCourseDTO : StudentCourseMinusCharacterDTO
    {
      public CharacterDTOMinusRelations Character { get; set; }
    }




}
