using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SkyWebApplication.Models;
using SkyWebApplication.DAL;
using Common;

namespace SkyWebApplication.Controllers
{
    public class CategoryController : Controller
    {
        private SkyWebContext db = new SkyWebContext();

       
        public ActionResult Index()
        {
            return View(db.Categorys.ToList());
        }

        public JsonResult Test()
        {
           // List<Category> Categorylist = GetAllCategorys();

            Category root = db.Categorys.Find(1);
            LoopToAppendChildren(root);
            return Json(root.ChildCategory, JsonRequestBehavior.AllowGet);
        }
        public void LoopToAppendChildren(Category curItem)
        {
            var subItems = GetCategorys(curItem.ID);
            curItem.ChildCategory = new List<Category>();
            curItem.ChildCategory.AddRange(subItems);
            foreach (var subItem in subItems)
            {
                LoopToAppendChildren(subItem);
            }
        }

        public List<Category> GetCategorys(int ParentID)
        {
            var categorys = from s in db.Categorys
                            orderby s.ID descending
                            where s.CategoryParentID == ParentID
                            select s;

            return categorys.ToList();

        }

        public List<Category> GetAllCategorys()
        {
            var categorys = from s in db.Categorys
                            orderby s.ID descending
                            select s;

            return categorys.ToList();

        }
        // GET: /Category/
        //public ActionResult Index()
        //{
        //    return View(db.Categorys.ToList());
        //}

        // GET: /Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categorys.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: /Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Category/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,CategoryName,CategoryInfo,CategoryParentID,CategoryStatus,CategorySort")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categorys.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: /Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categorys.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: /Category/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,CategoryName,CategoryInfo,CategoryParentID,CategoryStatus,CategorySort")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: /Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categorys.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: /Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categorys.Find(id);
            db.Categorys.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
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
