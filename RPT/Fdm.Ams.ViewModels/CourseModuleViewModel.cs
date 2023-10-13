using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fdm.Ams.ViewModels
{
    public class CourseModuleViewModel
    {
        public DateTime? CourseEndDate { get; set; }
        public int? PathwayId { get; set; }
        public DateTime? CourseStartDate { get; set; }

        [RegularExpression(@"^(?!\s*$).+", ErrorMessage = "Spaces are not allowed as a value")]
        [Required]
        public string Description { get; set; } = default!;

        public int? Duration { get; set; }
        public DateTime? EndDate { get; set; }
        public int Id { get; set; }

        [RegularExpression(@"^(?!\s*$).+", ErrorMessage = "Spaces are not allowed as a value")]
        [Required]
        public string Name { get; set; } = default!;

        [RegularExpression(@"^(?!\s*$).+", ErrorMessage = "Spaces are not allowed as a value")]
        [Required]
        public string PreparationNotes { get; set; } = default!;

        public bool Provisional { get; set; }
        public DateTime? StartDate { get; set; }
        public int? TrainerId { get; set; }
        public int? VenueId { get; set; }

        public Dictionary<int, string> TrainerDictionary { get; set; }
        public Dictionary<int, string> PathwayDictionary { get; set; }
        public Dictionary<int, string> VenueDictionary { get; set; }
    }
}