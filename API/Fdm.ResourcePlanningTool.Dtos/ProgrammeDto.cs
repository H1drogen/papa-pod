using Fdm.ResourcePlanningTool.Dtos.CustomAttribute;
using System.ComponentModel.DataAnnotations;

namespace Fdm.ResourcePlanningTool.Dtos
{
    public class ProgrammeDto : IGenericDto
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(1, MinimumLength = 1, ErrorMessage = "Must be a 1 character")]
        [UpperCaseCharsOnly(ErrorMessage = "Only UpperCase Character Allowed")]
        public string Abbreviation { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Description { get; set; }

        public int Id { get; set; }

        [CharsAndNumbersOnly(ErrorMessage = "No spaces at beginning and end, First letter must be char, afterwards char, numbers and symbols(hyphen, apostrophe) allowed")]
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
    }
}