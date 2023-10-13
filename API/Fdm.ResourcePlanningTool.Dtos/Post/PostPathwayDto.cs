using Fdm.ResourcePlanningTool.Dtos.CustomAttribute;
using System.ComponentModel.DataAnnotations;

namespace Fdm.ResourcePlanningTool.Dtos.Post
{
    /// <summary>
    /// Pathway is a scheduled pathway template that has a start and end date.
    /// eg. Java, BI, .Net
    /// </summary>
    public class PostPathwayDto
    {
        public bool Cancelled { get; set; }
        public string CreatedBy { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DateValidation("StartDate")]
        public DateTime EndDate { get; set; }

        public bool IsPond { get; set; }

        [Required]
        [Range(1, 100, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int MaxCapacity { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(15, MinimumLength = 1, ErrorMessage = "Must be at least 1 character")]
        public string PathwayCode { get; set; }

        [Required]
        public int PathwayTypeId { get; set; }

        [Required]
        public int ProgrammeId { get; set; }

        [Required]
        public int RegionId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
    }
}