namespace Fdm.Ams.Models.Post
{
    public class PostOfficeDto
    {
        public string Abbreviation { get; set; }
        public int CountryId { get; set; }
        public bool IsActive { get; set; }
        public bool IsPopup { get; set; }
        public string Name { get; set; } = default!;
    }
}