using Fdm.Data.ResourcePlanningTool.Models.BaseModel;
using System.ComponentModel.DataAnnotations;

namespace Fdm.Data.ResourcePlanningTool.Models
{
    /// <summary>
    /// Course Templates are the basic template for courses used in the Pathway Templates.
    /// eg. Pro Skills, OOD1, SQL
    /// </summary>
    public class CourseTemplate : Model
    {
        [Required]
        public string CreatedBy { get; set; } = default!;

        [Required]
        public string Description { get; set; } = default!;

        public int Duration { get; set; }
        public bool IsExtra { get; set; }

        [Required]
        public string Name { get; set; } = default!;

        public virtual PathwayTemplate? PathwayTemplate { get; set; }
        public int? PathwayTemplateId { get; set; }
        public string? PreparationNotes { get; set; }
        public int? Sequence { get; set; }
    }
}