using Grover.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Grover.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            return View("index", "Home");
        }

        [HttpPost]
        public ActionResult Order(string product1, string product2, string product3, string amount1, string amount2, string amount3, string address, string comment)
        {
            List<Product> products = (List<Product>)HttpContext.Application["products"];
            User currentUser = (User)HttpContext.Session["user"];

            double totalPrice = 0;
            int totalAmmount = 100; //delivery fee

            if (!string.IsNullOrEmpty(amount1))
                foreach (Product p in products)
                {
                    if (p.Name.Equals(product1))
                        if (Int32.Parse(amount1) > 0)
                        { 
                            totalPrice += p.Price * Int32.Parse(amount1);
                            totalAmmount += Int32.Parse(amount1);
                        }
                }

            if (!string.IsNullOrEmpty(amount2))
                foreach (Product p in products)
                {
                    if (p.Name.Equals(product2))
                        if (Int32.Parse(amount2) > 0)
                        {
                            totalPrice += p.Price * Int32.Parse(amount2);
                            totalAmmount += Int32.Parse(amount2);
                        }
                }


            if (!string.IsNullOrEmpty(amount3))
            {
                foreach (Product p in products)
                    if (p.Name.Equals(product3))
                        if (Int32.Parse(amount3) > 0)
                        {
                            totalPrice += p.Price * Int32.Parse(amount3);
                            totalAmmount += Int32.Parse(amount3);
                        }
            }

            DAO.AddOrder(totalPrice, totalAmmount, comment, address, "false", currentUser.Id);
            HttpContext.Application["orders"] = DAO.LoadAllOrders();

            List<Order> orders = (List<Order>)HttpContext.Application["orders"];
            int currentOrderID = 0;

            for (int i = 0; i < orders.Count; i++)
            {
                if (orders[i].UserID == currentUser.Id)
                    currentOrderID = orders[i].OrderID;
            }

            if (!string.IsNullOrEmpty(amount1))
                foreach (Product p in products)
                {
                    if (p.Name.Equals(product1))
                        for (int i = 0; i < Int32.Parse(amount1); i++)
                            DAO.AddContainsData(currentOrderID, p.Id);
                }

            if (!string.IsNullOrEmpty(amount2))
                foreach (Product p in products)
                {
                    if (p.Name.Equals(product2))
                        for (int i = 0; i < Int32.Parse(amount2); i++)
                                DAO.AddContainsData(currentOrderID, p.Id);
                }

            if (!string.IsNullOrEmpty(amount3))
                foreach (Product p in products)
                {
                    if (p.Name.Equals(product3))
                        for (int i = 0; i < Int32.Parse(amount3); i++)
                            DAO.AddContainsData(currentOrderID, p.Id);
                }

            HttpContext.Application["containData"] = DAO.LoadAllContainData();

            return RedirectToAction("index", "Home");
        }

    }
}