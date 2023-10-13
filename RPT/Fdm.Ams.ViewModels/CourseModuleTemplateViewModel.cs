using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Fdm.Ams.ViewModels
{
    public class CourseModuleTemplateViewModel
    {
        public string? CreatedBy { get; set; }

        [RegularExpression(@"^[^-\s][\w\s!@#£$%^&*()_+=\-`~\\\]\[{}|';:/.,?><""]*$", ErrorMessage = "No spaces at the beginning.")]
        [Required]
        public string Description { get; set; } = default!;

        [DisplayName("Duration (Days)")]
        public int Duration { get; set; }

        public int Id { get; set; }

        [DisplayName("Type")]
        public bool IsExtra { get; set; }

        [RegularExpression(@"^(?=.{1,})([a-zA-Z]+[0-9]{0,})+(?:[\s'\-][\w'\-]+)*\s*$", ErrorMessage = "Only alphanumeric values are allowed and no spaces at the beginning.")]
        [Required]
        public string Name { get; set; } = default!;

        public int? PathwayTemplateId { get; set; }

        [RegularExpression(@"^[^-\s][\w\s!@#£$%^&*()_+=\-`~\\\]\[{}|';:/.,?><\-""]*$", ErrorMessage = "No spaces at the beginning.")]
        [Required]
        [DisplayName("Preparation Notes")]
        public string PreparationNotes { get; set; } = default!;

        public int Sequence { get; set; }
    }
}