using Fdm.Data.ResourcePlanningTool.Models.BaseModel;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Fdm.Data.ResourcePlanningTool.Models
{
    [Index(propertyNames: nameof(Name), IsUnique = true)]
    [Index(propertyNames: nameof(Abbreviation), IsUnique = true)]
    public class Office : Model
    {
        public Office()
        {
            Trainers = new HashSet<Trainer>();
            Venues = new HashSet<Venue>();
        }

        [StringLength(3)]
        [Required]
        public string Abbreviation { get; set; }

        [Required]
        public virtual Country Country { get; set; }

        [Required]
        public int CountryId { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public bool IsPopUp { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Trainer> Trainers { get; set; }
        public virtual ICollection<Venue> Venues { get; set; }
    }
}