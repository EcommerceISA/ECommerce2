using ECommerce2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce2.Controllers
{
    public class GenericController : Controller
    {
        private ECommerceContext db = new ECommerceContext();

        // GET: Generic
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetCities(int stateId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var cities = db.Cities.Where(m => m.StateId == stateId);
            return Json(cities);
        }
        public JsonResult GetCompanies(int cityId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var companies = db.Companies.Where(m => m.CityId == cityId);
            return Json(companies);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}