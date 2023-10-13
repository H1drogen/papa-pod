using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Fdm.Ams.ViewModels
{
    public class OfficeViewModel
    {
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Must be a 3 characters")]
        [RegularExpression(@"^[A-Z]*$", ErrorMessage = "Spaces, Special charaters and Numbers are not allowed.")]
        [Required]
        public string Abbreviation { get; set; }

        public Dictionary<int, string> CountriesDictionary { get; set; }

        [DisplayName("Country")]
        public int CountryId { get; set; }

        public int Id { get; set; }

        [DisplayName("Status")]
        public bool IsActive { get; set; }

        [DisplayName("IsPop-Up")]
        public bool IsPopUp { get; set; }

        [RegularExpression(@"^[A-Za-z]+(?: [A-Za-z]+)*\s*$", ErrorMessage = "Only characters allowed, No spaces at beginning.")]
        [Required]
        public string Name { get; set; } = default!;
    }
}