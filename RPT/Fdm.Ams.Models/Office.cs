namespace Fdm.Ams.Models
{
    public class Office
    {
        public string Abbreviation { get; set; }
        public int CountryId { get; set; }
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsPopUp { get; set; }
        public string Name { get; set; } = default!;
    }
}