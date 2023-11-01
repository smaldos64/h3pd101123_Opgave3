using Opgave3.Models;

namespace Opgave3.DTO
{
    public class TeamForSaveDTO
    {
        public string TeamName { get; set; } = string.Empty;
        public string TeamDescription { get; set; } = string.Empty;
    }

    public class TeamForUpdateDTO : TeamForSaveDTO 
    {
        public int TeamId { get; set; }
    }

    public class TeamDTO : TeamForUpdateDTO
    {
        public ICollection<StudentDTOMinusTeamRelations> Students { get; set; }
        = new List<StudentDTOMinusTeamRelations>();
    }

    public class TeamDTOMinusRelations : TeamForUpdateDTO
    {
        
    }
}
