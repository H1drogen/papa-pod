using System.ComponentModel.DataAnnotations;

namespace Fdm.Ams.ViewModels
{
    public class CourseTypeViewModel
    {
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Must be a 3 characters")]
        [RegularExpression(@"^[A-Z]*$", ErrorMessage = "Only UpperCase Character Allowed.")]
        [Required]
        public string Abbreviation { get; set; } = default!;

        public string Colour { get; set; } = default!;

        public int Id { get; set; }

        public bool IsConcludingPathway { get; set; }

        [RegularExpression(@"^(?=.{1,})([a-zA-Z]+[0-9]{0,})+(?:[\s'\-][\w'\-]+)*\s*$", ErrorMessage = "Only alphanumeric values are allowed and no spaces at the beginning.")]
        [Required]
        public string Name { get; set; } = default!;
    }
}