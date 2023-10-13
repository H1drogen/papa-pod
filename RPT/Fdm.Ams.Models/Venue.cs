namespace Fdm.Ams.Models
{
    public class Venue
    {
        public bool Active { get; set; }
        public int Id { get; set; }
        public int MaxCapacity { get; set; }
        public string Name { get; set; } = default!;
        public int? OfficeId { get; set; }
    }
}