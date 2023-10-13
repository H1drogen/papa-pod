using Fdm.ResourcePlanningTool.Dtos.CustomAttribute;
using System.ComponentModel.DataAnnotations;

namespace Fdm.ResourcePlanningTool.Dtos.Post
{
    /// <summary>
    /// PathwayTemplates are templated for reuse when scheduling pathways, these are specific to regions and other specifics
    /// </summary>
    public class PostPathwayTemplateDto
    {
        [Required(AllowEmptyStrings = false)]
        public string CreatedBy { get; set; } = default!;

        [Required(AllowEmptyStrings = false)]
        public string Description { get; set; }

        [CharsAndNumbersOnly(ErrorMessage = "No spaces at beginning and end, First letter must be char, afterwards char, numbers and symbols(hyphen, apostrophe) allowed")]
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [Required]
        public int PathwayTypeId { get; set; }

        [Required]
        public int RegionId { get; set; }
    }
}