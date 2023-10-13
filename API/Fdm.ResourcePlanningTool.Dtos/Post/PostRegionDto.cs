using Fdm.ResourcePlanningTool.Dtos.CustomAttribute;
using System.ComponentModel.DataAnnotations;

namespace Fdm.ResourcePlanningTool.Dtos.Post
{
    public class PostRegionDto
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(1, MinimumLength = 1, ErrorMessage = "Must be a 1 character")]
        [UpperCaseCharsOnly(ErrorMessage = "Only UpperCase Character Allowed")]
        public string Abbreviation { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [CharsOnly(ErrorMessage = "Only characters allowed, No spaces at beginning and end")]
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
    }
}