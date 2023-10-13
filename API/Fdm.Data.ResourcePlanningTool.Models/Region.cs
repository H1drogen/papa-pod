using Fdm.Data.ResourcePlanningTool.Models.BaseModel;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Fdm.Data.ResourcePlanningTool.Models
{
    [Index(propertyNames: nameof(Name), IsUnique = true)]
    [Index(propertyNames: nameof(Abbreviation), IsUnique = true)]
    public class Region : Model
    {
        public Region()
        {
            Countries = new HashSet<Country>();
            Courses = new HashSet<Course>();
            CourseTemplates = new HashSet<CourseTemplate>();
            Trainers = new HashSet<Trainer>();
            Pathways = new HashSet<Pathway>();
        }

        [Required]
        [StringLength(1)]
        public string Abbreviation { get; set; }

        public virtual ICollection<Country> Countries { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<CourseTemplate> CourseTemplates { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Pathway> Pathways { get; set; }
        public virtual ICollection<Trainer> Trainers { get; set; }
    }
}