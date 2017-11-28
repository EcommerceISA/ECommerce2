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

        public ActionResult AddProduct()
        {
            ViewBag.ProductId = new SelectList(CombosHelper.GetProducts(), "ProductId", "Description");
            return PartialView();
        }

        [HttpPost]
        public ActionResult AddProduct(AddProductView view)
        {
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();

            if (ModelState.IsValid)
            {
                var orderDetailTmp = db.OrderDetailTmps
                    .Where(odt => odt.UserName == User.Identity.Name && odt.ProductId == view.ProductId)
                    .FirstOrDefault();
                if (orderDetailTmp == null)
                {


                    var product = db.Products.Find(view.ProductId);
                    orderDetailTmp = new OrderDetailTmp
                    {
                        Description = product.Description,
                        Price = product.Price,
                        ProductId = product.ProductId,
                        Quantity = view.Quantity,
                        TaxRate = product.Tax.Rate,
                        UserName = User.Identity.Name
                    };
                    db.OrderDetailTmps.Add(orderDetailTmp);
                }
                else
                {
                    orderDetailTmp.Quantity += view.Quantity;
                    db.Entry(orderDetailTmp).State = EntityState.Modified;
                }
                db.SaveChanges();
                return Redirect("Create");
            }

            ViewBag.ProductId = new SelectList(CombosHelper.GetProducts(user.CompanyId), "ProductId", "Description");
            return PartialView();
        }


        public ActionResult ShopingCart()
        {
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewBag.ProductId = new SelectList(CombosHelper.GetProducts(), "ProductId", "Description");
            return PartialView();
        }

        [HttpPost]
        public ActionResult ShopingCart(AddProductView view)
        {
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();

            if (ModelState.IsValid)
            {
                var orderDetailTmp = db.OrderDetailTmps
                    .Where(odt => odt.UserName == User.Identity.Name && odt.ProductId == view.ProductId)
                    .FirstOrDefault();
                if (orderDetailTmp == null)
                {


                    var product = db.Products.Find(view.ProductId);
                    orderDetailTmp = new OrderDetailTmp
                    {
                        Description = product.Description,
                        Price = product.Price,
                        ProductId = product.ProductId,
                        Quantity = view.Quantity,
                        TaxRate = product.Tax.Rate,
                        UserName = User.Identity.Name
                    };
                    db.OrderDetailTmps.Add(orderDetailTmp);
                }
                else
                {
                    orderDetailTmp.Quantity += view.Quantity;
                    db.Entry(orderDetailTmp).State = EntityState.Modified;
                }
                db.SaveChanges();
                return Redirect("Create");
            }

            ViewBag.ProductId = new SelectList(CombosHelper.GetProducts(user.CompanyId), "ProductId", "Description");
            return PartialView();
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