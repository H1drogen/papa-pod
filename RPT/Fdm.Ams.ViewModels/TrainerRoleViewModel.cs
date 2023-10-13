using System.ComponentModel.DataAnnotations;

namespace Fdm.Ams.ViewModels
{
    public class TrainerRoleViewModel
    {
        [RegularExpression(@"^(?!\s*$).+", ErrorMessage = "No spaces at the beginning.")]
        [Required]
        public string Description { get; set; }

        public int Id { get; set; }

        [RegularExpression(@"^(?=.{1,})([a-zA-Z]+[0-9]{0,})+(?:[\s'\-][\w'\-]+)*\s*$", ErrorMessage = "Only alphanumeric values are allowed and no spaces at the beginning.")]
        [Required]
        public string Name { get; set; }
    }
}