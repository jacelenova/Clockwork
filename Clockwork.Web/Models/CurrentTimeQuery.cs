using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clockwork.Web.Models
{
    public class CurrentTimeQuery
    {
        public int CurrentTimeQueryId { get; set; }
        public DateTime Time { get; set; }
        public string ClientIp { get; set; }
        public DateTime UTCTime { get; set; }
        public string TimeZone { get; set; }
    }

    public class CurrentTimeQueries
    {
        public IEnumerable<CurrentTimeQuery> TimeQueries { get; set; }
    }

    public class HomeModel
    {
        public IEnumerable<CurrentTimeQuery> TimeQueries { get; set; }
        public IReadOnlyCollection<TimeZoneInfo> TimeZones { get; set; }
    }
}