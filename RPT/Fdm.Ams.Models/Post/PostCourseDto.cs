using System;

namespace Fdm.Ams.Models.Post
{
    public class PostCourseDto
    {
        public bool Cancelled { get; set; }
        public string CreatedBy { get; set; } = default!;
        public DateTime? EndDate { get; set; }
        public bool IsPond { get; set; }
        public int? MaxCapacity { get; set; }
        public string PathwayCode { get; set; } = default!;
        public int? PathwayTemplateId { get; set; }
        public int? PathwayTypeId { get; set; }
        public int? ProgrammeId { get; set; }
        public int? RegionId { get; set; }
        public DateTime? StartDate { get; set; }
    }
}