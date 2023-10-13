using System;

namespace Fdm.Ams.Models
{
    public class Holiday
    {
        public int CountryId { get; set; }
        public string Description { get; set; }
        public DateTime EndDate { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }

        public virtual string CountriesName { get; set; }
    }
}