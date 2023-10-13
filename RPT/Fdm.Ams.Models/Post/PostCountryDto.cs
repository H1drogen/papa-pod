namespace Fdm.Ams.Models.Post
{
    public class PostCountryDto
    {
        public bool IsActive { get; set; }
        public string Name { get; set; } = default!;
        public int RegionId { get; set; }
        public string RegionName { get; set; } = default!;
    }
}