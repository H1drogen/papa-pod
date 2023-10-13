using Fdm.ResourcePlanningTool.Dtos.CustomAttribute;
using System.ComponentModel.DataAnnotations;

namespace Fdm.ResourcePlanningTool.Dtos
{
    public class TrainerDto : IGenericDto
    {
        public bool Active { get; set; }

        [EmailAddress]
        [Required(AllowEmptyStrings = false)]
        public string Email { get; set; } = default!;

        [CharsAndNumbersOnly(ErrorMessage = "No spaces at beginning and end, First letter must be char, afterwards char, numbers and symbols(hyphen, apostrophe) allowed")]
        [Required(AllowEmptyStrings = false)]
        public string FirstName { get; set; } = default!;

        public int Id { get; set; }
        public bool? IsCoreTrainer { get; set; }
        public bool IsTutor { get; set; }

        [CharsAndNumbersOnly(ErrorMessage = "No spaces at beginning and end, First letter must be char, afterwards char, numbers and symbols(hyphen, apostrophe) allowed")]
        [Required(AllowEmptyStrings = false)]
        public string LastName { get; set; } = default!;

        [Required]
        public int OfficeId { get; set; }

        public byte[] Photo { get; set; } = default!;
        public string TeamName { get; set; } = default!;

        [Required(AllowEmptyStrings = false)]
        public string Username { get; set; } = default!;
    }
}