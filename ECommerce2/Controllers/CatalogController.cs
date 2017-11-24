using ECommerce2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public ActionResult Details(int companyId, int productId)
        {
            var product = db.Products
                .Where(p => p.CompanyId==companyId)
                .Select(p => p.ProductId==productId)
                .FirstOrDefault();

            return View(product);
        }

        public ActionResult ViewCategories(int categoryId)
        {
            var products = db.Products.Where(c => c.CategoryId == categoryId);

            return View(products.ToList());
        }

    }
}