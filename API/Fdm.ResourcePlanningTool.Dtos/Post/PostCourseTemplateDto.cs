using Fdm.ResourcePlanningTool.Dtos.CustomAttribute;
using System.ComponentModel.DataAnnotations;

namespace Fdm.ResourcePlanningTool.Dtos.Post
{
    /// <summary>
    /// Course Templates are the basic template for courses used in the Pathway Templates.
    /// eg. Pro Skills, OOD1, SQL
    /// </summary>
    public class PostCourseTemplateDto
    {
        [Required(AllowEmptyStrings = false)]
        public string CreatedBy { get; set; } = default!;

        [Required(AllowEmptyStrings = false)]
        public string Description { get; set; } = default!;

        [Required]
        [Range(1, 5, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int Duration { get; set; }

        public bool IsExtra { get; set; }

        [CharsAndNumbersOnly(ErrorMessage = "No spaces at beginning and end, First letter must be char, afterwards char, numbers and symbols(hyphen, apostrophe) allowed")]
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; } = default!;

        public int? PathwayTemplateId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string PreparationNotes { get; set; }

        public int? Sequence { get; set; }
    }
}