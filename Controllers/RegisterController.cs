using Grover.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Grover.Controllers
{
    public class RegisterController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user, string ConfirmPassword)
        {
            Dictionary<string, User> users = (Dictionary<string, User>)HttpContext.Application["users"];

            if(users.ContainsKey(user.Username))
            {
                ViewBag.error = "Username already exists, please try again.";
                return View("index");
            }

            if(user.Password != ConfirmPassword)
            {
                ViewBag.error = "Passwords did not match, please try again.";
                return View("index");
            }

            switch (user.Type)
            {
                case UserType.KORISNIK:
                {
                    user.Enabled = true;
                    break;
                }
                case UserType.DOSTAVLJAC:
                {
                    user.Enabled = false;
                    break;
                }
            }

            user.Password = DAO.EncryptPassword(user.Password);
            user.Image = String.Empty;
            users.Add(user.Username, user);
            DAO.AddUser(user);

            return RedirectToAction("index", "Login");
        }
    }
}