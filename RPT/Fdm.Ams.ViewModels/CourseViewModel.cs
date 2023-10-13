using Fdm.Ams.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Fdm.Ams.ViewModels
{
    public class CourseViewModel
    {
        public bool Cancelled { get; set; }
        public Dictionary<int, string> CoursesDictionary { get; set; }
        public Dictionary<int, string> CourseTemplatesDictionary { get; set; }

        [DisplayName("Created By")]
        public string CreatedBy { get; set; } = default!;

        [DisplayName("End Date")]
        public DateTime? EndDate { get; set; }

        public int Id { get; set; }

        [DisplayName("Is Pond")]
        public bool IsPond { get; set; }

        [DisplayName("Max Capacity")]
        [Required]
        [Range(0, 100)]
        public string MaxCapacity { get; set; }

        [Required(ErrorMessage = "Office field is required.")]
        public int OfficeId { get; set; }

        public Dictionary<int, string> OfficesDictionary { get; set; }

        [DisplayName("Pathway Code")]
        public string PathwayCode { get; set; } = default!;

        [Required(ErrorMessage = "Pathway Template field is required.")]
        public int? PathwayTemplateId { get; set; }

        [DisplayName("Pathway Type")]
        public int PathwayTypeId { get; set; }

        public Dictionary<int, string> PathwayTypesDictionary { get; set; }

        [Required(ErrorMessage = "Programme field is required.")]
        [DisplayName("Programme")]
        public int ProgrammeId { get; set; }

        public Dictionary<int, string> ProgrammesDictionary { get; set; }

        [DisplayName("Region")]
        public int RegionId { get; set; }

        public Dictionary<int, string> RegionsDictionary { get; set; }
        public string SelectedTimeZone { get; set; }

        [DisplayName("Start Date")]
        [Required]
        public DateTime? StartDate { get; set; }

        [Required]
        public TimeZonesViewModel Timezones { get; set; }
        public virtual ICollection<Course> Courses { get; set; }

        public virtual string RegionName { get; set; }
        public virtual string PathwayTypeName { get; set; }
        public virtual string ProgrammeName { get; set; }
    }
}