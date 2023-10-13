namespace Fdm.ResourcePlanningTool.Dtos.Post
{
    public class PostVenueDto
    {
        public bool Active { get; set; }
        public int MaxCapacity { get; set; }
        public string Name { get; set; }
        public int? OfficeId { get; set; }
    }
}