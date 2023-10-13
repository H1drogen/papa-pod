using Fdm.Data.ResourcePlanningTool.Models.BaseModel;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Fdm.Data.ResourcePlanningTool.Models
{
    /// <summary>
    /// Pathway is a scheduled pathway template that has a start and end date.
    /// eg. Java, BI, .Net
    /// </summary>
    [Index(propertyNames: nameof(PathwayCode), IsUnique = true)]
    public class Pathway : Model
    {
        public Pathway()
        {
            Courses = new HashSet<Course>();
        }

        [Required]
        public bool Cancelled { get; set; }

        public virtual ICollection<Course> Courses { get; set; }

        [Required]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public bool IsPond { get; set; }

        [Required]
        public int MaxCapacity { get; set; }

        [Required]
        public string PathwayCode { get; set; }

        [Required]
        public virtual PathwayType PathwayType { get; set; }

        [Required]
        public int PathwayTypeId { get; set; }

        [Required]
        public virtual Programme Programme { get; set; }

        [Required]
        public int ProgrammeId { get; set; }

        [Required]
        public virtual Region Region { get; set; }

        [Required]
        public int RegionId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
    }
}