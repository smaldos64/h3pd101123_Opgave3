using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Opgave3.Models
{
    public class StudentCourse
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentCourseId { get; set; }

        [Required]
        [ForeignKey("StudentId")]
        public int StudentId { get; set; }

        [Required]
        [ForeignKey("CourseId")]
        public int CourseId { get; set; }

        [Required]
        [ForeignKey("CharacterId")]
        public int CharacterId { get; set; }

        public int YearForCharacter { get; set; }

        public Student Student { get; set; }
        public Course Course { get; set; }
        public Character Character { get; set; }    
    }
}
