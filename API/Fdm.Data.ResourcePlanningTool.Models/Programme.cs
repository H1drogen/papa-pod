using Fdm.Data.ResourcePlanningTool.Models.BaseModel;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Fdm.Data.ResourcePlanningTool.Models
{
    [Index(propertyNames: nameof(Name), IsUnique = true)]
    [Index(propertyNames: nameof(Abbreviation), IsUnique = true)]
    public class Programme : Model
    {
        public Programme()
        {
            Pathways = new HashSet<Pathway>();
        }

        [StringLength(1)]
        [Required]
        public string Abbreviation { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Pathway> Pathways { get; set; }
    }
}