using Fdm.ResourcePlanningTool.Dtos.CustomAttribute;
using System.ComponentModel.DataAnnotations;

namespace Fdm.ResourcePlanningTool.Dtos
{
    public class PathwayTypeDto : IGenericDto
    {
        /// <summary>
        /// Pathway Types are the generic types of courses that will apply to PathwayTemplates and Pathways univerally.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Must be a 3 characters")]
        [UpperCaseCharsOnly(ErrorMessage = "Only UpperCase Character Allowed")]
        public string Abbreviation { get; set; }

        [Required]
        [HexCodeOnly(ErrorMessage = "Only Hex Color code allowed")]
        public string Colour { get; set; }

        public int Id { get; set; }
        public bool IsConcludingPathway { get; set; }

        public string LMSAccessGroup { get; set; }

        [CharsAndNumbersOnly(ErrorMessage = "No spaces at beginning and end, First letter must be char, afterwards char, numbers and symbols(hyphen, apostrophe) allowed")]
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
    }
}