using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class BlogsController : Controller
    {
        private BlogContext db = new BlogContext();
        // GET: Blogs
        public ActionResult Index()
        {
            if (Request.Cookies["Cookie"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(db.Blogss.ToList());
            }
        }

        

        // GET: Blogs/Details/5
        public ActionResult Details(string id)
        {
            if (Request.Cookies["Cookie"] == null)
            {
                return RedirectToAction("Index");
            }
            else
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
                return View(blogs);
            }
        }

        // GET: Blogs/Create
        public ActionResult Create()
        {
            if (Request.Cookies["Cookie"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        // POST: Blogs/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,user_image,name,summary,content,created_at")] Blogs blogs, IEnumerable<HttpPostedFileBase> files)
        {
            if (Request.Cookies["Cookie"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    blogs.id = Guid.NewGuid().ToString();
                    blogs.created_at = DateTime.Now.ToString("yyyy-MM-dd");                   
                    foreach (var file in files) {
                        if (file == null || file.ContentLength == 0)
                        {
                            ViewBag.Error = "Please select a file";
                            return View();
                        }
                        else
                        {
                            var fileName = Path.GetFileName(file.FileName);
                            
                            if (file.FileName.EndsWith("txt"))
                            {
                                var path = Path.Combine(Server.MapPath("~/BlogContent/" + blogs.content), fileName);
                                if (System.IO.File.Exists(path))
                                {
                                    return View();
                                }
                                else
                                {
                                    Directory.CreateDirectory(Server.MapPath("~/BlogContent/" + blogs.content));
                                    file.SaveAs(path);
                                }
                            }

                            if (file.FileName.EndsWith("jpg")|| file.FileName.EndsWith("gif"))
                            {
                                var path = Path.Combine(Server.MapPath("~/Blogsimg/"), fileName);
                                if (System.IO.File.Exists(path))
                                {
                                    return View();
                                }
                                else
                                {         
                                    file.SaveAs(path);
                                }
                            }                            
                        }
                    }
                    db.Blogss.Add(blogs);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(blogs);
            }
        }

        public ActionResult UploadMaterial()
        {
            if (Request.Cookies["Cookie"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadMaterial([Bind(Include = "name")] Material materials, HttpPostedFileBase file)
        {
            if (Request.Cookies["Cookie"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    if (file == null || file.ContentLength == 0)
                    {
                        ViewBag.Error = "Please select a file";
                        return View();
                    }
                    else
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        materials.name = fileName;
                        var path = Path.Combine(Server.MapPath("~/BlogMaterial/"),fileName);
                        if (System.IO.File.Exists(path))
                        {
                            return View();
                        }
                        else
                        {
                            file.SaveAs(path);
                        }
                    }
                    db.Materials.Add(materials);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(materials);
            }
        }

        // GET: Blogs/Edit/5
        public ActionResult Edit(string id)
        {
            if (Request.Cookies["Cookie"] == null)
            {
                return RedirectToAction("Index");
            }
            else
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
                return View(blogs);
            }
        }

        // POST: Blogs/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,user_image,name,summary,content,created_at")] Blogs blogs)
        {
            if (Request.Cookies["Cookie"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Entry(blogs).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(blogs);
            }
        }

        // GET: Blogs/Delete/5
        public ActionResult Delete(string id)
        {
            if (Request.Cookies["Cookie"] == null)
            {
                return RedirectToAction("Index");
            }
            else
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
                return View(blogs);
            }
        }

        // POST: Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            if (Request.Cookies["Cookie"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                Blogs blogs = db.Blogss.Find(id);
                db.Blogss.Remove(blogs);
                db.SaveChanges();
                return RedirectToAction("Index");
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
    }
}
