using System;

namespace Fdm.Ams.Models.Post
{
    public class PostActivityDto
    {
        public int ActivityTypeId { get; set; }
        public int? ConsultantId { get; set; }
        public string Details { get; set; } = default!;
        public DateTime End { get; set; }
        public bool IsAllDayActivity { get; set; }
        public DateTime Start { get; set; }
    }
}