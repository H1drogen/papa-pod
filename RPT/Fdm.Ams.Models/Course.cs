using System;

namespace Fdm.Ams.Models
{
    public class Course
    {
        public bool Cancelled { get; set; }
        public string CreatedBy { get; set; } = default!;
        public DateTime? EndDate { get; set; }
        public int Id { get; set; }
        public bool IsPond { get; set; }
        public int? MaxCapacity { get; set; }
        public string PathwayCode { get; set; } = default!;
        public int? PathwayTypeId { get; set; }
        public int ProgrammeId { get; set; }
        public int? RegionId { get; set; }
        public DateTime? StartDate { get; set; }
        public virtual string RegionName { get; set; }
        public virtual string PathwayTypeName { get; set; }
        public virtual string ProgrammeName { get; set; }

    }
}