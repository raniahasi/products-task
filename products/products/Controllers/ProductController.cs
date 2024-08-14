
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace products.Controllers
{
    public class ProductController : Controller
    {
        private List<Dictionary<string, string>> GetProductList()
        {
            var products = Session["Products"] as List<Dictionary<string, string>>;
            if (products == null)
            {
                products = new List<Dictionary<string, string>>();
                Session["Products"] = products;
            }
            return products;
        }


        // GET: Product
        public ActionResult Index()
        {
            var products = GetProductList(); 
            return View(products); 
        }


        public ActionResult Create()
        {
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(string name, string description, decimal price, string imageUrl)
        {
            if (ModelState.IsValid)
            {
                var products = GetProductList();

                var newProduct = new Dictionary<string, string>
            {
                { "Id", (products.Count + 1).ToString() },
                { "Name", name },
                { "Description", description },
                { "Price", price.ToString() },
                { "ImagePath", imageUrl }
            };
                 
                products.Add(newProduct);
                Session["Products"] = products;

                return RedirectToAction("Index");
            }

            return View();
        }
    }
}