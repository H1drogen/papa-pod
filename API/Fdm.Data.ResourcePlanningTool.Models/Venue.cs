using Fdm.Data.ResourcePlanningTool.Models.BaseModel;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Fdm.Data.ResourcePlanningTool.Models
{
    [Index(propertyNames: nameof(Name), IsUnique = true)]
    public class Venue : Model
    {
        /// <summary>
        /// Venues are the rooms used for training in the offices.
        /// </summary>
        public Venue()
        {
            Course = new HashSet<Course>();
        }

        public bool Active { get; set; }
        public virtual ICollection<Course> Course { get; set; }
        public int MaxCapacity { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual Office Office { get; set; }

        [Required]
        public int OfficeId { get; set; }
    }
}