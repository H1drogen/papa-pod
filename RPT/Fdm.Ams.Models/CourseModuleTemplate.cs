namespace Fdm.Ams.Models
{
    public class CourseModuleTemplate
    {
        public int? PathwayTemplateId { get; set; }
        public string? CreatedBy { get; set; }
        public string Description { get; set; } = default!;
        public int Duration { get; set; }
        public int Id { get; set; }
        public bool IsExtra { get; set; }
        public string Name { get; set; } = default!;
        public string PreparationNotes { get; set; } = default!;
        public int Sequence { get; set; }
    }
}