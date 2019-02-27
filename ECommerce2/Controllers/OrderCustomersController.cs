using ECommerce2.Classes;
using ECommerce2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ECommerce2.Controllers
{
    public class OrderCustomersController : Controller
    {

        private ECommerceContext db = new ECommerceContext();


        // GET: Create Customer

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.CityId = new SelectList(
                CombosHelper.GetCities(),
                "CityId", "Name");

            ViewBag.StateId = new SelectList(
                CombosHelper.GetStates(),
                "StateId", "Name");

            return View();
        }

        [HttpPost]
        //POST: Create Customer
        [Authorize(Roles = "Admin")]
        public ActionResult Create(Customer customer)
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

                        var companycustomer = new CompanyCustomer
                        {
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



        [Authorize(Roles = "Customer")]
        //GET: List Customer
        public ActionResult Index()
        {
            var orders = db.Orders.Where(o => o.Customer.UserName == User.Identity.Name);
            return View(orders.ToList());
        }


        [Authorize(Roles = "Customer")]
        //GET: Details Customers
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        //GET: Create Order Customer
        public ActionResult CreateOrder()
        {
            var user = db.Customers.Where(c => c.UserName == User.Identity.Name).FirstOrDefault();

            if (user == null)
            {
                return View("Index", "OrderCustomers");
            }

            var view = new ShopingCart
            {
                CustomerId = user.CustomerId,
                Date = DateTime.Now,
                Details = db.OrderDetailTmps
                .Where(odt => odt.UserName == user.UserName)
                .ToList()
            };

            return View(view);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //POST: Create Order Customer
        public ActionResult CreateOrder(ShopingCart view)
        {
            if (ModelState.IsValid)
            {
                var response = MovementsHelper.NewOrder(view, User.Identity.Name);

                if (response.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty, response.Message);
            }

            view.Details = db.OrderDetailTmps
                .Where(odt => odt.UserName == User.Identity.Name)
                .ToList();

            return View(view);
        }
        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.


      


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