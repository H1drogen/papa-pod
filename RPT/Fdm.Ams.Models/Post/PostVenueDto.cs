namespace Fdm.Ams.Models.Post
{
    public class PostVenueDto
    {
        public bool Active { get; set; }
        public int MaxCapacity { get; set; }
        public string Name { get; set; } = default!;
        public int? OfficeId { get; set; }
    }
}