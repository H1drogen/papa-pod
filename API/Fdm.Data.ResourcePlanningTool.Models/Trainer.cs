using Fdm.Data.ResourcePlanningTool.Models.BaseModel;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Fdm.Data.ResourcePlanningTool.Models
{
    [Index(propertyNames: nameof(Email), IsUnique = true)]
    [Index(propertyNames: nameof(Username), IsUnique = true)]
    public class Trainer : Model
    {
        public Trainer()
        {
            Trainer_Courses = new HashSet<Trainer_Course>();
        }
        public bool Active { get; set; }


        [Required]
        public string Email { get; set; } = default!;

        [Required]
        public string FirstName { get; set; } = default!;

        public bool? IsCoreTrainer { get; set; }

        public bool IsTutor { get; set; }

        [Required]
        public string LastName { get; set; } = default!;

        [Required]
        public virtual Office Office { get; set; }

        [Required]
        public int OfficeId { get; set; }

        public byte[] Photo { get; set; } = default!;

        public string TeamName { get; set; } = default!;

        [Required]
        public string Username { get; set; } = default!;

        public virtual ICollection<Trainer_Course> Trainer_Courses { get; set; }
    }
}