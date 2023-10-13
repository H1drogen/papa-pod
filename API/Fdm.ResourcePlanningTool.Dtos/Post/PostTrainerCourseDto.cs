using System.ComponentModel.DataAnnotations;

namespace Fdm.ResourcePlanningTool.Dtos.Post
{
    public class PostTrainerCourseDto
    {
        [Required]
        public int CourseId { get; set; }

        [Required]
        public int TrainerId { get; set; }

        [Required]
        public int TrainerRoleId { get; set; }
    }
}