using System.ComponentModel.DataAnnotations;

namespace Fdm.Ams.ViewModels
{
    public class ProgrammeViewModel
    {
        [StringLength(1, MinimumLength = 1, ErrorMessage = "Must be a 1 character")]
        [RegularExpression(@"^[A-Z]*$", ErrorMessage = "Only UpperCase Character Allowed.")]
        [Required]
        public string Abbreviation { get; set; }

        [RegularExpression(@"^(?!\s*$).+", ErrorMessage = "No spaces at the beginning.")]
        [Required]
        public string Description { get; set; }

        public int Id { get; set; }

        [RegularExpression(@"^(?=.{1,})([a-zA-Z]+[0-9]{0,})+(?:[\s'\-][\w'\-]+)*\s*$", ErrorMessage = "Only alphanumeric values are allowed and no spaces at the beginning.")]
        [Required]
        public string Name { get; set; }
    }
}