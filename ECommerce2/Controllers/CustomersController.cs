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
    [Authorize(Roles ="User")]
    public class CustomersController : Controller
    {
        private ECommerceContext db = new ECommerceContext();

        // GET: Customers
        public ActionResult Index()
        {
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();

            var qry = (from cu in db.Customers
                       join cc in db.CompanyCustomers on cu.CustomerId equals cc.CustomerId
                       join co in db.Companies on cc.CompanyId equals co.CompanyId
                       where co.CompanyId == user.CompanyId
                       select new { cu }).ToList();

            var customers = new List<Customer>();
            foreach (var item in qry)
            {
                customers.Add(item.cu);
            }

            return View(customers.ToList());
        }

        // GET: Customers/Details/5

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();

            ViewBag.CityId = new SelectList(
                CombosHelper.GetCities(),
                "CityId", "Name");

            ViewBag.StateId = new SelectList(
                CombosHelper.GetStates(),
                "StateId", "Name");

            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerId,CompanyId,UserName,FirstName,LastName,Phone,Address,StateId,CityId")] Customer customer)
        {
            if (ModelState.IsValid)
            {

                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Customers.Add(customer);
                        var response = DBHelper.SaveChanges(db);
                        if (!response.Succeeded)
                        {
                            ModelState.AddModelError(string.Empty, response.Message);
                            transaction.Rollback();
                            ViewBag.CityId = new SelectList(db.Cities, "CityId", "Name", customer.CityId);
                            ViewBag.StateId = new SelectList(db.States, "StateId", "Name", customer.StateId);
                            return View(customer);
                        }

                        UsersHelper.CreateUserASP(customer.UserName, "Customer");
                        var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();

                        var companycustomer = new CompanyCustomer{
                            CompanyId = user.CompanyId,
                            CustomerId = customer.CustomerId
                        };

                        db.CompanyCustomers.Add(companycustomer);
                        db.SaveChanges();

                        transaction.Commit();
                        return RedirectToAction("Index");

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();

                    }
                }



                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CityId = new SelectList(db.Cities, "CityId", "Name", customer.CityId);
            ViewBag.StateId = new SelectList(db.States, "StateId", "Name", customer.StateId);

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            ViewBag.CityId = new SelectList(
                CombosHelper.GetCities(),
                "CityId", "Name",
                customer.CityId);

            ViewBag.StateId = new SelectList(
                CombosHelper.GetStates(),
                "StateId", "Name",
                customer.StateId);

            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerId,CompanyId,UserName,FirstName,LastName,Phone,Address,StateId,CityId")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CityId = new SelectList(db.Cities, "CityId", "Name", customer.CityId);
            ViewBag.StateId = new SelectList(db.States, "StateId", "Name", customer.StateId);
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            var user = db.Users.Where(u => u.UserName ==User.Identity.Name).FirstOrDefault();
            var companyCustomer = db.CompanyCustomers
                .Where(cc => cc.CompanyId == user.CompanyId 
                && cc.CustomerId == customer.CustomerId)
                .FirstOrDefault();

            using (var transaction = db.Database.BeginTransaction())
            {
                db.CompanyCustomers.Remove(companyCustomer);
                db.Customers.Remove(customer);
                var response = DBHelper.SaveChanges(db);
                if (response.Succeeded)
                {
                    transaction.Commit();
                    return RedirectToAction("Index");
                }
                transaction.Rollback();
                ModelState.AddModelError(string.Empty, response.Message);
                return View(customer);
            }
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
