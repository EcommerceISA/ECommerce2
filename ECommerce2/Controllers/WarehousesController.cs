using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ECommerce2.Models;
using ECommerce2.Classes;

namespace ECommerce2.Controllers
{
    public class WarehousesController : Controller
    {
        private ECommerceContext db = new ECommerceContext();

        // GET: Warehouses
        public ActionResult Index()
        {
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            var warehouses = db.Warehouse.Where(w => w.CompanyId == user.CompanyId).Include(w => w.City)
                .Include(w => w.State);
            return View(warehouses.ToList());
        }

        // GET: Warehouses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Warehouse warehouse = db.Warehouse.Find(id);
            if (warehouse == null)
            {
                return HttpNotFound();
            }
            return View(warehouse);
        }

        // GET: Warehouses/Create
        public ActionResult Create()
        {
            ViewBag.CityId = new SelectList(
                CombosHelper.GetCities(), 
                "CityId", "Name");

            ViewBag.StateId = new SelectList(
                CombosHelper.GetStates(), 
                "StateId", "Name");

            var user = db.Users.Where(u => u.UserName == User.Identity.Name)
                .FirstOrDefault();

            var warehouse = new Warehouse {
                CompanyId = user.CompanyId
            };

            return View(warehouse);
        }

        // POST: Warehouses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WarehouseId,CompanyId,Name,Phone,Address,StateId,CityId")] Warehouse warehouse)
        {
            if (ModelState.IsValid)
            {
                db.Warehouse.Add(warehouse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CityId = new SelectList(
                CombosHelper.GetCities(warehouse.StateId), 
                "CityId", "Name");

            ViewBag.StateId = new SelectList(
                CombosHelper.GetStates(), 
                "StateId", "Name");

            return View(warehouse);
        }

        // GET: Warehouses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Warehouse warehouse = db.Warehouse.Find(id);
            if (warehouse == null)
            {
                return HttpNotFound();
            }

            ViewBag.CityId = new SelectList(
                CombosHelper.GetCities(warehouse.StateId),
                "CityId", "Name");

            ViewBag.StateId = new SelectList(
                CombosHelper.GetStates(),
                "StateId", "Name");

            return View(warehouse);
        }

        // POST: Warehouses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WarehouseId,CompanyId,Name,Phone,Address,StateId,CityId")] Warehouse warehouse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(warehouse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CityId = new SelectList(
                CombosHelper.GetCities(warehouse.StateId),
                "CityId", "Name");

            ViewBag.StateId = new SelectList(
                CombosHelper.GetStates(),
                "StateId", "Name");
            return View(warehouse);
        }

        // GET: Warehouses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Warehouse warehouse = db.Warehouse.Find(id);
            if (warehouse == null)
            {
                return HttpNotFound();
            }
            return View(warehouse);
        }

        // POST: Warehouses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Warehouse warehouse = db.Warehouse.Find(id);
            db.Warehouse.Remove(warehouse);
            db.SaveChanges();
            return RedirectToAction("Index");
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
