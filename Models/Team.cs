namespace Opgave3.Models
{
    public class Team
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; } = string.Empty;

        public string TeamDescription { get; set; } = string.Empty;

        public ICollection<Student> Students { get; set; }
        = new List<Student>();
    }
}
