using Facebook;
using Grover.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Grover.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        //FACEBOOK LOGIN

        private Uri RedirectUri
        {
            get
            {
                UriBuilder uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }

        [AllowAnonymous]
        public ActionResult Facebook()
        {
            FacebookClient facebookClient = new FacebookClient();
            Uri loginURL = facebookClient.GetLoginUrl(new
            {
                client_id = "586355429719486",
                client_secret = "f77a6ca218f55914cba9eaa09e1d2a7b",
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email"
            });

            return Redirect(loginURL.AbsoluteUri);
        }

        public ActionResult FacebookCallback(string code)
        {
            FacebookClient facebookClient = new FacebookClient();
            dynamic result = facebookClient.Post("oauth/access_token", new
            {
                client_id = "586355429719486",
                client_secret = "f77a6ca218f55914cba9eaa09e1d2a7b",
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
            });
            var accessToken = result.access_token;
            Session["AccessToken"] = accessToken;
            facebookClient.AccessToken = accessToken;
            dynamic me = facebookClient.Get("me?fields=link,first_name,currency,last_name,email,gender,locale,timezone,verified,picture,age_range");

            User faceboookUser = new User(me.email, me.first_name, me.last_name, me.picture.data.url);
            Session["user"] = faceboookUser;

            return RedirectToAction("index", "Home");
        }

        //FACEBOOK LOGIN

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            Dictionary<string, User> users = (Dictionary<string, User>)HttpContext.Application["users"];
            string encryptedPassword = DAO.EncryptPassword(password);

            foreach (User user in users.Values)
                if (user.Username.Equals(username) && user.Password.Equals(encryptedPassword))
                {
                    Session["user"] = user;
                    return RedirectToAction("index", "Home");
                }
                else
                    ViewBag.error = "Invalid username or password, please try again.";

            return View("index");
        }


        public ActionResult SignOut()
        {
            Session["user"] = null;
            return RedirectToAction("index", "Home");
        }
    }
}