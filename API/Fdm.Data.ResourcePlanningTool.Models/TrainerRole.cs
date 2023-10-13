using Fdm.Data.ResourcePlanningTool.Models.BaseModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fdm.Data.ResourcePlanningTool.Models
{
    [Index(propertyNames: nameof(Name), IsUnique = true)]
    public class TrainerRole:Model
    {
        public TrainerRole()
        {
            Trainer_Courses = new HashSet<Trainer_Course>();
        }
        [Required]
        public string Name { get; set; }
        
        public string Description { get; set; }

        public virtual ICollection<Trainer_Course> Trainer_Courses { get; set; }
    }
}
