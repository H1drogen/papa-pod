using Fdm.Data.ResourcePlanningTool.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fdm.Data.ResourcePlanningTool.Models
{
    public class Trainer_Course :Model
    {
      
        [Required]
        public virtual Course Course { get; set; }
        [Required]
        public int CourseId { get; set; }

        [Required]
        public virtual Trainer Trainer { get; set; }
        [Required]
        public int TrainerId { get; set; }

        [Required]
        public virtual TrainerRole TrainerRole { get; set; }
        [Required]
        public int TrainerRoleId { get; set; }
    }
}
