using ECommerce2.Models;
using System;
using System.Collections.Generic;
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

        public ActionResult ShopingCart(int companyId, int productId)
        {
            var product = db.Products
                .Where(p => p.CompanyId == companyId)
                .Select(p => p.ProductId == productId)
                .FirstOrDefault();

            return View(product);
        }

        public ActionResult ViewCategories(string categoryName)
        {
            var category = db.Categories.Where(c => c.Description.Equals(categoryName)).FirstOrDefault();
            var products = db.Products.Where(c => c.CategoryId == category.CategoryId).ToList();

            if (products == null)
            {
                return RedirectToAction("Index");

            }
            //string hola = "hola";
            return View("Index", products);
        }

    }
}