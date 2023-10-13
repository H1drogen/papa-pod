using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Fdm.Ams.ViewModels
{
    public class RegionViewModel
    {
        [StringLength(1, MinimumLength = 1, ErrorMessage = "Must be 1 character")]
        [RegularExpression(@"^[A-Z]*$", ErrorMessage = "Spaces, Special charaters and Numbers are not allowed.")]
        [Required]
        public string Abbreviation { get; set; }

        public int Id { get; set; }

        [Required]
        [DisplayName("Status")]
        public bool IsActive { get; set; }

        [RegularExpression(@"^[A-Za-z]+(?: [A-Za-z]+)*\s*$", ErrorMessage = "Only characters allowed, No spaces at beginning.")]
        [Required]
        public string Name { get; set; }
    }
}