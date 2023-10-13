using Fdm.Data.ResourcePlanningTool.Models.BaseModel;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Fdm.Data.ResourcePlanningTool.Models
{
    [Index(propertyNames: nameof(Name), IsUnique = true)]
    public class Country : Model
    {
        public Country()
        {
            Offices = new HashSet<Office>();
        }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public string Name { get; set; } = default!;

        public virtual ICollection<Office> Offices { get; set; }

        [Required]
        public virtual Region Region { get; set; }

        [Required]
        public int RegionId { get; set; }
    }
}