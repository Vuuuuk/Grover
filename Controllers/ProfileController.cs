using Grover.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Grover.Controllers
{
    public class ProfileController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EditProfile(User user, string ConfirmPassword, string profilePicture, string OldUsername)
        {
            Dictionary<string, User> users = (Dictionary<string, User>)HttpContext.Application["users"];

            user.Id = users[OldUsername].Id;
            user.BirthDate = users[OldUsername].BirthDate;
            user.Enabled = users[OldUsername].Enabled;

            if (!string.IsNullOrEmpty(user.Password) && user.Password.Equals(ConfirmPassword))
                user.Password = DAO.EncryptPassword(user.Password);

            else if (string.IsNullOrEmpty(user.Password))
                user.Password = users[OldUsername].Password;

            else
            {
                ViewBag.error = "Passwords did not match, please try again.";
                return View("index");
            }

            if (!String.IsNullOrEmpty(profilePicture))
                user.Image = "/Content/Images/Profile/" + profilePicture;
            else
                user.Image = users[OldUsername].Image;

            DAO.UpdateUser(user);
            users = DAO.LoadAllUsers();
            HttpContext.Application["users"] = users;
            Session["user"] = user;

            return View("index");
        }
    }
}