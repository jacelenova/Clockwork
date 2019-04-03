using Clockwork.Web.Helpers;
using Clockwork.Web.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace Clockwork.Web.Controllers
{
    public class HomeController : Controller
    {
        private string apiUrl = "http://localhost:53557/api/";

        public async Task<ActionResult> Index()
        {
            var mvcName = typeof(Controller).Assembly.GetName();
            var isMono = Type.GetType("Mono.Runtime") != null;

            ViewData["Version"] = mvcName.Version.Major + "." + mvcName.Version.Minor;
            ViewData["Runtime"] = isMono ? "Mono" : ".NET";

            var homeModel = new HomeModel();
            homeModel.TimeZones = GetTimeZones();
            homeModel.TimeQueries = await GetTimeQueries();

            return View(homeModel);
        }

        public async Task<IEnumerable<CurrentTimeQuery>> GetTimeQueries()
        {
            var timeQueries = await HttpClientHelper.GetItemsAsync<CurrentTimeQuery>(apiUrl + "currenttime");
            foreach (CurrentTimeQuery item in timeQueries)
            {
                item.TimeZoneName = GetTimeZoneById(item.TimeZone).DisplayName;
            }
            return timeQueries;
        }

        [HttpPost]
        public async Task<JsonResult> CreateTimeQuery(CurrentTimeQuery data)
        {
            var timeQuery = await HttpClientHelper.PostRequest<CurrentTimeQuery>(apiUrl + "currenttime", data);
            timeQuery.TimeZoneName = GetTimeZoneById(timeQuery.TimeZone).DisplayName;
            return Json(timeQuery);
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

        public TimeZoneInfo GetTimeZoneById(string id)
        {
            TimeZoneInfo value = default(TimeZoneInfo);
            foreach(TimeZoneInfo info in TimeZoneInfo.GetSystemTimeZones())
            {
                if (info.Id == id)
                {
                    value = info;
                    break;
                }
            }

            return value;
        }

        public ActionResult GetPartial()
        {
            return View("_TimeQueryPartial");
        }
    }
}
