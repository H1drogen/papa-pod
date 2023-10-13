using Fdm.ResourcePlanningTool.Dtos.CustomAttribute;
using System.ComponentModel.DataAnnotations;

namespace Fdm.ResourcePlanningTool.Dtos.Post
{
    public class PostCountryDto
    {
        [Required]
        public bool IsActive { get; set; }

        [CharsOnly(ErrorMessage = "Only characters allowed, No spaces at beginning and end")]
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [Required]
        public int RegionId { get; set; }
    }
}