using System;

namespace Fdm.Ams.ViewModels
{
    public class CourseTemplateCalendarEventViewModel
    {
        public int Duration { get; set; }
        public DateTime End { get; set; }

        public int Id { get; set; }

        public int Sequence { get; set; }
        public DateTime Start { get; set; }
        public string Title { get; set; }
    }
}