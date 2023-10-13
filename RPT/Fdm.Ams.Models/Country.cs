using System.Collections.Generic;

namespace Fdm.Ams.Models
{
    public class Country
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; } = default!;
        public int RegionId { get; set; }

        public string RegionName { get; set; }

    }
}