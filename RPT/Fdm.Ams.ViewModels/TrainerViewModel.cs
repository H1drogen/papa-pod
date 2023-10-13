using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Fdm.Ams.ViewModels
{
    public class TrainerViewModel
    {
        [DisplayName("Status")]
        public bool Active { get; set; }

        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        [Required]
        public string Email { get; set; } = default!;

        [RegularExpression(@"^(?=.{1,})([a-zA-Z]+[0-9]{0,})+(?:[\s'\-][\w'\-]+)*\s*$", ErrorMessage = "Only alphanumeric values are allowed and no spaces at the beginning.")]
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; } = default!;

        public int Id { get; set; }

        [RegularExpression(@"^(?=.{1,})([a-zA-Z]+[0-9]{0,})+(?:[\s'\-][\w'\-]+)*\s*$", ErrorMessage = "Only alphanumeric values are allowed and no spaces at the beginning.")]
        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; } = default!;

        public Dictionary<int, string> OfficeDictionaries { get; set; }

        [DisplayName("Office")]
        public int OfficeId { get; set; }

        [DisplayName("Team Name")]
        public string TeamName { get; set; } = default!;

        [RegularExpression(@"^(?!\s*$).+", ErrorMessage = "Spaces are not allowed as a value.")]
        [Required]
        public string Username { get; set; } = default!;
    }
}