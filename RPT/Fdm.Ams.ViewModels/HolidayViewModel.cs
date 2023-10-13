using Fdm.Ams.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Fdm.Ams.ViewModels
{
    public class HolidayViewModel
    {
        public Dictionary<int, string> CountryDictionary { get; set; }

        [DisplayName("Country")]
        [Required(ErrorMessage = "Country Name field is required.")]
        public int CountryId { get; set; }

        [RegularExpression(@"^(?!\s*$).+", ErrorMessage = "No spaces at the beginning.")]
        [Required]
        public string Description { get; set; }

        [Required]
        [DisplayName("End Date")]
        public DateTime? EndDate { get; set; }

        public int Id { get; set; }

        [RegularExpression(@"^(?=.{1,})([a-zA-Z]+[0-9]{0,})+(?:[\s'\-][\w'\-]+)*\s*$", ErrorMessage = "Only alphanumeric values are allowed and no spaces at the beginning.")]
        [Required]
        public string Name { get; set; }

        [Required]
        [DisplayName("Start Date")]
        public DateTime? StartDate { get; set; }

        public virtual ICollection<Country> Countries { get; set; }
        public virtual ICollection<Holiday> Holidays { get; set; }

        public virtual string CountriesName { get; set; }
    }
}