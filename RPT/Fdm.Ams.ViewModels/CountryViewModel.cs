using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Fdm.Ams.ViewModels
{
    public class CountryViewModel
    {
        public int Id { get; set; }

        [DisplayName("Status")]
        public bool IsActive { get; set; }

        [RegularExpression(@"^[A-Za-z]+(?: [A-Za-z]+)*\s*$", ErrorMessage = "Only characters allowed, No spaces at beginning.")]
        [Required]
        public string Name { get; set; } = default!;

        public Dictionary<int, string> RegionDictionaries { get; set; }

        [DisplayName("Region")]
        public int RegionId { get; set; }


        public string RegionName { get; set; }

    }
}