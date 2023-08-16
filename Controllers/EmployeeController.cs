using Grover.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Grover.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Manage(string Username)
        {
            Dictionary<string, User> users = (Dictionary<string, User>)HttpContext.Application["users"];

            DAO.ActivateUser(users[Username].Id, !users[Username].Enabled);

            HttpContext.Application["users"] = DAO.LoadAllUsers();

            return RedirectToAction("index", "Employee");
        }

    }
}