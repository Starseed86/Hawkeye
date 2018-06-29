#region "  using  "
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MvcContrib.Filters;
using System.IO;
using System.Text;
using DCS_Hawkeye_Server.Models;
using Newtonsoft.Json;
using DCS_Hawkeye_Server.DataLayer;
#endregion

namespace DCS_Hawkeye_Server.Controllers
{
    public class LocationController : Controller
    {
        public string TestData = string.Empty;
        public int FirstGo = 0;
        
        //set location data
        public ActionResult SetLocationData()
        {
            return Content(LocationDataProvider.GetLocationData());
        }

        [HttpGet]
        public ActionResult Index()
        {
            TestLocationModel model = new TestLocationModel();
            model.TestLocationData = "This is a test!";
            return View("TestLocationView", model);
        }

        [HttpGet]
        public ActionResult DummySetTestLocationData()
        {
            int LocationData = LocationDataProvider.DummySetLocationDataDB();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult SetTestLocationData()
        {
            byte[] data = Request.BinaryRead(Request.ContentLength);
            int LocationData = LocationDataProvider.SetLocationDataDB(data);
            return RedirectToAction("Index");
        }
    }
}
