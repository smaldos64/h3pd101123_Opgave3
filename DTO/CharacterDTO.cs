namespace Opgave3.DTO
{
    public class CharacterForSaveDTO
    {
        public int CharacterValue { get; set; }
    }

    public class CharacterForUpdateDTO : CharacterForSaveDTO
    {
        public int CharacterId { get; set; }
    }

    public class CharacterDTOMinusRelations : CharacterForUpdateDTO
    {

    }

    public class CharacterDTO : CharacterForUpdateDTO
    {

    }
}
