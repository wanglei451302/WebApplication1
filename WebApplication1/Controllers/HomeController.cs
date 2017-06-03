using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private BlogContext db = new BlogContext();
        public ActionResult Index()
        {
            return View(db.Blogss.ToList());
        }        

        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blogs blogs = db.Blogss.Find(id);
            if (blogs == null)
            {
                return HttpNotFound();
            }
            MatchEvaluator evaluator = new MatchEvaluator(word => { return "<img src=" + word.Value.Substring(1) + "/>"; });
            blogs.content = Regex.Replace(blogs.content, "#\"/BlogMaterial.*(jpg|gif)\"", evaluator);
            return View(blogs);
        }
    }
}