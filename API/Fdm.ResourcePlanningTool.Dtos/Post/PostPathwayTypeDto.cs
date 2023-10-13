using Fdm.ResourcePlanningTool.Dtos.CustomAttribute;
using System.ComponentModel.DataAnnotations;

namespace Fdm.ResourcePlanningTool.Dtos.Post
{
    public class PostPathwayTypeDto
    {
        /// <summary>
        /// Pathway Types are the generic types of courses that will apply to PathwayTemplates and Pathways univerally.
        /// </summary>
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Must be a 3 characters")]
        [UpperCaseCharsOnly(ErrorMessage = "Only UpperCase Character Allowed")]
        [Required(AllowEmptyStrings = false)]
        public string Abbreviation { get; set; }

        [Required]
        [HexCodeOnly(ErrorMessage = "Only Hex Color code allowed")]
        public string Colour { get; set; }

        public bool IsConcludingPathway { get; set; }
        public string LMSAccessGroup { get; set; }

        [CharsAndNumbersOnly(ErrorMessage = "No spaces at beginning and end, First letter must be char, afterwards char, numbers and symbols(hyphen, apostrophe) allowed")]
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
    }
}