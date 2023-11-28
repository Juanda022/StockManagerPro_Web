using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using StockManager.Models;

namespace StockManagerPro_Web.Controllers
{
    public class AccessController : Controller
    {
        private DBStockManagerEntities3 db = new DBStockManagerEntities3();
        // GET: Access

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login([Bind(Include = "UserID,Password,Role")] Users oUser)
        {
            if (oUser.UserID == 0 || string.IsNullOrEmpty(oUser.Password))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string hashedPassword = ConvertSha256(oUser.Password);
            Users users = db.Users.SingleOrDefault(u => u.UserID == oUser.UserID && u.Password == hashedPassword);
            if (users != null)
            {
                Session["user"] = users;
                Session["userID"] = users.UserID;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }

        }

        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Register([Bind(Include = "UserID,Password,Role")] Users oUser)
        {
            bool create = false;
            if (ModelState.IsValid)
            {
                oUser.Password = ConvertSha256(oUser.Password);
                db.Users.Add(oUser);
                db.SaveChanges();
                create = true;
            }
            if (create)
            {
                return RedirectToAction("Login", "Access");
            }

            return View();
        }

        public static string ConvertSha256(string password)
        {
            StringBuilder Sb = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(password));

                foreach (byte b in result)
                {
                    Sb.Append(b.ToString("x2"));
                }
            }
            return Sb.ToString();
        }
    }
}