using Fdm.Data.ResourcePlanningTool.Models.BaseModel;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Fdm.Data.ResourcePlanningTool.Models
{
    /// <summary>
    /// PathwayTemplates are templated for reuse when scheduling pathways, these are specific to regions and other specifics
    /// </summary>
    [Index(propertyNames: nameof(Name), IsUnique = true)]
    [Index(propertyNames: nameof(Description), IsUnique = true)]
    public class PathwayTemplate : Model
    {
        public PathwayTemplate()
        {
            CourseTemplates = new HashSet<CourseTemplate>();
        }

        public virtual ICollection<CourseTemplate> CourseTemplates { get; set; }

        [Required]
        public string CreatedBy { get; set; } = default!;

        [Required]
        public string Description { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public virtual PathwayType PathwayType { get; set; }

        [Required]
        public int PathwayTypeId { get; set; }

        [Required]
        public virtual Region Region { get; set; }

        [Required]
        public int RegionId { get; set; }
    }
}