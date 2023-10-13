using System;

namespace Fdm.Ams.Models
{
    public class CourseModule
    {
        public int? PathwayId { get; set; }
        public string Description { get; set; } = default!;
        public DateTime? EndDate { get; set; }
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string PreparationNotes { get; set; } = default!;
        public bool Provisional { get; set; }
        public DateTime? StartDate { get; set; }
        public int? TrainerId { get; set; }
        public int? VenueId { get; set; }
    }
}