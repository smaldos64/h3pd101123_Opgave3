using Opgave3.Models;

namespace Opgave3
{
    public class Character
    {
        public int CharacterId { get; set; }
        public int CharacterValue { get; set; }
        public ICollection<StudentCourse> StudentCourses { get; set; }
              = new List<StudentCourse>();
    }
}
