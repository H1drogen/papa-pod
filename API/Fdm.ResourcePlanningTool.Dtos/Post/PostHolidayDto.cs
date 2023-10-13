using Fdm.ResourcePlanningTool.Dtos.CustomAttribute;
using System.ComponentModel.DataAnnotations;

namespace Fdm.ResourcePlanningTool.Dtos.Post
{
    public class PostHolidayDto
    {
        [Required]
        public int CountryId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DateValidation("StartDate")]
        public DateTime EndDate { get; set; }

        [CharsAndNumbersOnly(ErrorMessage = "No spaces at beginning and end, First letter must be char, afterwards char, numbers and symbols(hyphen, apostrophe) allowed")]
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
    }
}