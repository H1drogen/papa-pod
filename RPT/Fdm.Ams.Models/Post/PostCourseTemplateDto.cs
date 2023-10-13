namespace Fdm.Ams.Models.Post
{
    public class PostCourseTemplateDto
    {
        public string? CreatedBy { get; set; }
        public int PathwayTypeId { get; set; }
        public string Description { get; set; } = default!;
        public string Name { get; set; } = default!;
        public int RegionId { get; set; }
    }
}