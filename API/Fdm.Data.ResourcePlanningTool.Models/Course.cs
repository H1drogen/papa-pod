using Fdm.Data.ResourcePlanningTool.Models.BaseModel;
using System.ComponentModel.DataAnnotations;

namespace Fdm.Data.ResourcePlanningTool.Models
{
    /// <summary>
    /// Course is made up of Modules, Courses last normally between three days and a week
    /// eg. Pro Skills, OOD1, SQL
    /// </summary>
    public class Course : Model
    {
        public Course()
        {
            TrainerCourses = new HashSet<Trainer_Course>();
        }
        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public virtual Pathway Pathway { get; set; }
        public int? PathwayId { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string PreparationNotes { get; set; }

        public bool Provisional { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public int? trainerId { get; set; }  
        public virtual Venue Venue { get; set; }
        public int? VenueId { get; set; }
        public bool Virtual { get; set; }

        public virtual ICollection<Trainer_Course> TrainerCourses { get; set; }
    }
}