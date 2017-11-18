using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ECommerce2.Models;

namespace ECommerce2.Controllers
{
    public class WarehousesController : Controller
    {
        private ECommerceContext db = new ECommerceContext();

        // GET: Warehouses
        public ActionResult Index()
        {
            var warehouse = db.Warehouse.Include(w => w.City).Include(w => w.Company).Include(w => w.State);
            return View(warehouse.ToList());
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
            ViewBag.CityId = new SelectList(db.Cities, "CityId", "Name");
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name");
            ViewBag.StateId = new SelectList(db.States, "StateId", "Name");
            return View();
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

            ViewBag.CityId = new SelectList(db.Cities, "CityId", "Name", warehouse.CityId);
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name", warehouse.CompanyId);
            ViewBag.StateId = new SelectList(db.States, "StateId", "Name", warehouse.StateId);
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
            ViewBag.CityId = new SelectList(db.Cities, "CityId", "Name", warehouse.CityId);
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name", warehouse.CompanyId);
            ViewBag.StateId = new SelectList(db.States, "StateId", "Name", warehouse.StateId);
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
            ViewBag.CityId = new SelectList(db.Cities, "CityId", "Name", warehouse.CityId);
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name", warehouse.CompanyId);
            ViewBag.StateId = new SelectList(db.States, "StateId", "Name", warehouse.StateId);
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
