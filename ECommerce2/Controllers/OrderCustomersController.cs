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
    [Authorize(Roles ="Customer")]
    public class OrderCustomersController : Controller
    {
        private ECommerceContext db = new ECommerceContext();
        // GET: OrderCustomers
        public ActionResult Index()
        {
            var orders = db.Orders.Where(o => o.Customer.UserName == User.Identity.Name);
            return View(orders.ToList());
        }

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



        public ActionResult Create()
        {
            var customer = db.Customers.Where(c => c.UserName == User.Identity.Name).FirstOrDefault();

            var view = new NewOrderView
            {
                CustomerId = customer.CustomerId,
                Date = DateTime.Now,
                Details = db.OrderDetailTmps
                .Where(odt => odt.UserName == User.Identity.Name)
                .ToList()
            };

            return View(view);
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NewOrderView view)
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

            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            view.Details = db.OrderDetailTmps
                .Where(odt => odt.UserName == User.Identity.Name)
                .ToList();

            ViewBag.CustomerId = new SelectList(CombosHelper.GetCustomers(user.CompanyId), "CustomerId", "FullName");
            return View(view);
        }

        public ActionResult ShopingCart(int? companyId, int? productId)
        {
            if (companyId == null || productId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = db.Products.Where(p => p.CompanyId == companyId && p.ProductId == productId).First();

            if (product == null)
            {
                return HttpNotFound();
            }


            return View("Index");

        }




        public ActionResult DeleteProduct(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var orderDetailTmp = db.OrderDetailTmps
                .Where(odt => odt.UserName == User.Identity.Name && odt.ProductId == id)
                .FirstOrDefault();

            if (orderDetailTmp == null)
            {
                return HttpNotFound();
            }
            db.OrderDetailTmps.Remove(orderDetailTmp);
            db.SaveChanges();
            return RedirectToAction("Create");

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