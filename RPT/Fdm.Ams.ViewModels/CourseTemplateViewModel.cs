using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Fdm.Ams.ViewModels
{
    public class CourseTemplateViewModel
    {
        public IList<CourseTemplateCalendarEventViewModel> CourseTemplateCalendarEventViewModel;
        public string CourseTemplates { get; set; }
        public string CreatedBy { get; set; }

        [RegularExpression(@"^[^-\s][\w\s!@#£$%^&*()_+=\-`~\\\]\[{}|';:/.,?><""]*$", ErrorMessage = "No spaces at the beginning.")]
        [Required]
        public string Description { get; set; } = default!;

        public int Id { get; set; }

        [RegularExpression(@"^(?=.{1,})([a-zA-Z]+[0-9]{0,})+(?:[\s'\-][\w'\-]+)*\s*$", ErrorMessage = "Only alphanumeric values are allowed and no spaces at the beginning.")]
        [Required]
        public string Name { get; set; } = default!;

        public Dictionary<int, string> PathwayTypeDictionaries { get; set; }

        [DisplayName("Pathway Type")]
        [Required]
        public int PathwayTypeId { get; set; }

        public string PathwayTypeName { get; set; }
        public Dictionary<int, string> RegionDictionaries { get; set; }

        [DisplayName("Region")]
        [Required]
        public int RegionId { get; set; }

        public string RegionName { get; set; }
    }
}