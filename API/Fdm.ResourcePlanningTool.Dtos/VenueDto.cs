namespace Fdm.ResourcePlanningTool.Dtos
{
    public class VenueDto : IGenericDto
    {
        public bool Active { get; set; }
        public int Id { get; set; }
        public int MaxCapacity { get; set; }
        public string Name { get; set; }
        public int? OfficeId { get; set; }
    }
}