using Fdm.ResourcePlanningTool.Dtos.CustomAttribute;
using System.ComponentModel.DataAnnotations;

namespace Fdm.ResourcePlanningTool.Dtos
{
    /// <summary>
    /// Course is made up of Modules, Courses last normally between three days and a week
    /// eg. Pro Skills, OOD1, SQL
    /// </summary>
    public class CourseDto : IGenericDto
    {
        [Required(AllowEmptyStrings = false)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DateValidation("StartDate")]
        public DateTime EndDate { get; set; }

        public int Id { get; set; }

        [CharsAndNumbersOnly(ErrorMessage = "No spaces at beginning and end, First letter must be char, afterwards char, numbers and symbols(hyphen, apostrophe) allowed")]
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        public int? PathwayId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string PreparationNotes { get; set; }

        public bool Provisional { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        public int? TrainerId { get; set; }
        public int? VenueId { get; set; }
        public bool Virtual { get; set; }
    }
}