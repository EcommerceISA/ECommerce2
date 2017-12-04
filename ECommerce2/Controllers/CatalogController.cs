using ECommerce2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ECommerce2.Controllers
{
    public class CatalogController : Controller
    {
        private ECommerceContext db = new ECommerceContext();

        // GET: Catalog
        public ActionResult Index()
        {
            var products = db.Products.ToList();

            return View(products);
        }

        //GET: Details Gatalog
        public ActionResult Details(int? companyId, int? productId)
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

            return View(product);
        }

        //GET: Shopping Cart
        public ActionResult ShopingCart(int companyId, int productId)
        {
            var product = db.Products
                .Where(p => p.CompanyId == companyId && p.ProductId == productId)
                .FirstOrDefault();
            return View(product);
        }

        public ActionResult ViewCategories(string categoryName)
        {
            try
            {
                var category = db.Categories.Where(c => c.Description.Equals(categoryName)).FirstOrDefault();

                if (categoryName == null)
                {
                    return RedirectToAction("Index");

                }

                var products = db.Products.Where(c => c.CategoryId == category.CategoryId).ToList();
                if (products == null)
                {
                    return RedirectToAction("Index");

                }
                return View("Index", products);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction("Index","Catalog");
            }
        }

        public ActionResult Search(string searchString) {

            var products = db.Products.Where(p => p.Description.Contains(searchString));
            

            return View("Index", products.ToList());
        }

        [Authorize(Roles = "Customer")]
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

        [Authorize(Roles ="Customer")]
        public ActionResult ShoppingCart(int? companyId, int? productId, int? quantity)
        {
            if (companyId == null || productId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (quantity == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (quantity < 0)
            {
                quantity = 1;
            }

            var product = db.Products
                .Where(p => p.CompanyId == companyId &&
                p.ProductId == productId)
                .FirstOrDefault();

            var orderDetailTmp = db.OrderDetailTmps
                .Where(odt => odt.UserName == User.Identity.Name && odt.ProductId == product.ProductId)
                .FirstOrDefault();

            if (orderDetailTmp == null)
            {
                orderDetailTmp = new OrderDetailTmp
                {
                    Description = product.Description,
                    Price = product.Price,
                    ProductId = product.ProductId,
                    Quantity = Convert.ToDouble(quantity),
                    TaxRate = product.Tax.Rate,
                    UserName = User.Identity.Name
                };
                db.OrderDetailTmps.Add(orderDetailTmp);
            }
            else
            {
                orderDetailTmp.Quantity += Convert.ToDouble(quantity);
                db.Entry(orderDetailTmp).State = EntityState.Modified;
            }
            db.SaveChanges();

            return RedirectToAction("Index", "Catalog");
        }




    }
}