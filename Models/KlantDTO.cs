namespace API.Models
{
    public class KlantDTO
    {
        public int Id { get; set; }
        public string? Naam { get; set; }
        public string? Email { get; set; }

        public KlantDTO()
        {
            Naam = string.Empty;
            Email = string.Empty;
        }
    }
}
