using Grover.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Grover
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            Dictionary<string, User> users = DAO.LoadAllUsers();
            HttpContext.Current.Application["users"] = users;

            List<Product> products = DAO.LoadAllProducts();
            HttpContext.Current.Application["products"] = products;

            List<Order> orders = DAO.LoadAllOrders();
            HttpContext.Current.Application["orders"] = orders;

            List<ChowNow_Contains> containData = DAO.LoadAllContainData();
            HttpContext.Current.Application["containData"] = containData;
        }
    }
}
