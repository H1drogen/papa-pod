using System;

namespace Fdm.Ams.Models
{
    public class Activity
    {
        public int ActivityTypeId { get; set; }
        public int? ConsultantId { get; set; }
        public string Details { get; set; } = default!;
        public DateTime End { get; set; }
        public int Id { get; set; }
        public bool IsAllDayActivity { get; set; }
        public DateTime Start { get; set; }
    }
}