using Fdm.Data.ResourcePlanningTool.Models.BaseModel;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Fdm.Data.ResourcePlanningTool.Models
{
    [Index(propertyNames: nameof(Name), IsUnique = true)]
    [Index(propertyNames: nameof(Abbreviation), IsUnique = true)]
    [Index(propertyNames: nameof(Colour), IsUnique = true)]
    public class PathwayType : Model
    {
        /// <summary>
        /// Pathway Types are the generic types of courses that will apply to PathwayTemplates and Pathways univerally.
        /// </summary>
        public PathwayType()
        {
            Pathways = new HashSet<Pathway>();
            PathwayTemplates = new HashSet<PathwayTemplate>();
        }

        [Required]
        public string Abbreviation { get; set; }

        [Required]
        public string Colour { get; set; }

        public bool IsConcludingPathway { get; set; }
        public string? LMSAccessGroup { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Pathway> Pathways { get; set; }
        public virtual ICollection<PathwayTemplate> PathwayTemplates { get; set; }
    }
}