using Fdm.ResourcePlanningTool.Dtos.CustomAttribute;
using System.ComponentModel.DataAnnotations;

namespace Fdm.ResourcePlanningTool.Dtos.Post
{
    public class PostOfficeDto
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Must be a 3 characters")]
        [UpperCaseCharsOnly(ErrorMessage = "Only UpperCase Character Allowed")]
        public string Abbreviation { get; set; }

        [Required]
        public int CountryId { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public bool IsPopUp { get; set; }

        [CharsOnly(ErrorMessage = "Only characters allowed, No spaces at beginning and end")]
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
    }
}