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
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AdminsController : Controller
    {
        private AdminContext db = new AdminContext();

        // GET: Admins
        

        // GET: Admins/Create
        public ActionResult Create()
        {
            if (Request.Cookies["Cookie"] == null)
            {
                return RedirectToAction("Create");
            }
            else
            {
                return View();
            }
        }

        
        // POST: Admins/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "name,passwd,created_at,salt")] Admin admin)
        {
            if (Request.Cookies["Cookie"] == null)
            {
                return RedirectToAction("Create");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    byte[] randomBytes = new byte[32];
                    RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                    rng.GetBytes(randomBytes);
                    admin.salt = Convert.ToBase64String(randomBytes);
                    admin.name = Convert.ToBase64String(new HMACSHA256().ComputeHash(Encoding.UTF8.GetBytes(admin.name)));
                    admin.passwd = Convert.ToBase64String(new HMACSHA256().ComputeHash(Encoding.UTF8.GetBytes(admin.passwd)));
                    admin.created_at = DateTime.Now;
                    db.Admins.Add(admin);
                    db.SaveChanges();
                    

                }
                return RedirectToAction("Success");
            }
        }

        public ActionResult Success()
        {
            if (Request.Cookies["Cookie"] == null)
            {
                return RedirectToAction("Create");
            }
            else
            {
                return View("Success");
            }
        }
        public ActionResult Login()
        {
            if (Request.Cookies["Cookie"] != null)
            {
                return RedirectToAction("Create");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "name,passwd,created_at,salt")] Admin admin)
        {
            
            var v = db.Admins.Where(a => a.name.Equals(admin.name) && a.passwd.Equals(admin.passwd)).FirstOrDefault();
            if (v != null)
            {
                HttpCookie aCookie = new HttpCookie("Cookie");
                aCookie.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Add(aCookie);
                return RedirectToAction("Create");
            }
            else
            {
                return View(admin);
                
            }
        }
        
        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult SignOut()
        {
            if (Request.Cookies["Cookie"] == null)
            {
                return RedirectToAction("Create");
            }
            else
            {
                HttpCookie aCookie = new HttpCookie("Cookie");
                aCookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(aCookie);

                return RedirectToAction("Login");
            }
        }
    }
}
