namespace Fdm.Ams.Models.Post
{
    public class PostCourseModuleTemplateDto
    {
        public int? PathwayTemplateId { get; set; }
        public string Description { get; set; } = default!;
        public int Duration { get; set; }
        public bool IsExtra { get; set; }
        public string Name { get; set; } = default!;
        public string PreparationNotes { get; set; } = default!;
        public int Sequence { get; set; }
    }
}