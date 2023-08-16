using Grover.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Grover.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddProduct(Product product)
        { 
            DAO.AddProduct(product.Name, product.Price, product.Ingredients);
            HttpContext.Application["products"] = DAO.LoadAllProducts();

            return View("index");
        }
    }
}