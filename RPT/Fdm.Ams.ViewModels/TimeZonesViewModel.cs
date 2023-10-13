using System.Collections.Generic;

public class TimeZonesViewModel
{
    public TimeZonesViewModel()
    {
        TimeZoneDictionary = new Dictionary<int, string>() {
                {1,  "UTC" },
                {2, "Europe/London" },
                {3, "Europe/Berlin"},
                {4, "Australia/Sydney"},
                {5, "Asia/Shanghai"},
                {6,"Asia/Hong_Kong" },
                {7, "US/Eastern" },
                {8,"US/Central" },
            };
    }

    public Dictionary<int, string> TimeZoneDictionary { get; set; }
}