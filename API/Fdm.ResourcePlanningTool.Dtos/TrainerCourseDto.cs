using System.ComponentModel.DataAnnotations;

namespace Fdm.ResourcePlanningTool.Dtos
{
    public class TrainerCourseDto : IGenericDto
    {
        [Required]
        public int CourseId { get; set; }

        public int Id { get; set; }

        [Required]
        public int TrainerId { get; set; }

        [Required]
        public int TrainerRoleId { get; set; }
    }
}