using System;

namespace Fdm.Ams.Models.Post
{
    public class PostCourseModuleDto
    {
        public int? PathwayId { get; set; }
        public string Description { get; set; } = default!;
        public int Duration { get; set; }
        public DateTime? EndDate { get; set; }
        public string Name { get; set; } = default!;
        public string PreparationNotes { get; set; } = default!;
        public bool Provisional { get; set; }
        public DateTime? StartDate { get; set; }
        public int? TrainerId { get; set; }
        public int? VenueId { get; set; }
    }
}