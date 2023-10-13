using Fdm.ResourcePlanningTool.Dtos.CustomAttribute;
using System.ComponentModel.DataAnnotations;

namespace Fdm.ResourcePlanningTool.Dtos.Post
{
    public class PostTrainerRoleDto
    {
        [Required(AllowEmptyStrings = false)]
        public string Description { get; set; }

        [CharsAndNumbersOnly(ErrorMessage = "No spaces at beginning and end, First letter must be char, afterwards char, numbers and symbols(hyphen, apostrophe) allowed")]
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
    }
}