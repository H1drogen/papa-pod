using Fdm.Data.ResourcePlanningTool.Models.BaseModel;
using System.ComponentModel.DataAnnotations;

namespace Fdm.Data.ResourcePlanningTool.Models
{
    public class Holiday : Model
    {
        public virtual Country Country { get; set; }

        [Required]
        public int CountryId { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
    }
}