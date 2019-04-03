using System;
using Microsoft.AspNetCore.Mvc;
using Clockwork.API.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Clockwork.API.Controllers
{
    [Route("api/[controller]")]
    public class CurrentTimeController : Controller
    {
        private ClockworkContext dbContext;

        public CurrentTimeController(ClockworkContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET api/currenttime
        [HttpGet]
        public IActionResult Get()
        {
            var timeQueries = dbContext.CurrentTimeQueries;
            return Ok(timeQueries);
        }

        [HttpPost]
        public IActionResult Create([FromBody]CurrentTimeQuery data)
        {
            var utcTime = DateTime.UtcNow;
            var serverTime = DateTime.Now;
            var tzTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(utcTime, data.TimeZone);
            var ip = HttpContext.Connection.RemoteIpAddress.ToString();

            var timeQuery = new CurrentTimeQuery
            {
                UTCTime = utcTime,
                ClientIp = ip,
                Time = tzTime
            };

            dbContext.Add(timeQuery);
            dbContext.SaveChanges();

            return Ok(timeQuery);
        }

        public ReadOnlyCollection<TimeZoneInfo> GetTimeZones()
        {
            ReadOnlyCollection<TimeZoneInfo> timeZones;
            timeZones = TimeZoneInfo.GetSystemTimeZones();

            foreach (var x in timeZones)
            {
                Debug.WriteLine(x.DisplayName);
            }

            return timeZones;
        }
    }
}
