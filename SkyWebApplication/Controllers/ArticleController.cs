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

namespace SkyWebApplication.Controllers
{
    public class ArticleController : Controller
    {
        private SkyWebContext db = new SkyWebContext();


        public List<Category> GetCategoryListByParentID(int ParentID)
        {
            var categorys = from s in db.Categorys
                            orderby s.CategorySort ascending
                            where s.CategoryParentID == ParentID
                            select s;
            List<Category> CategoryList = new List<Category>();
            foreach (var category in categorys)
            {
                CategoryList.Add(category);
            }
            return CategoryList.ToList();

        }

        //构建一个CategoryList的SelectListItem

        public List<SelectListItem> GetCategorySelectList(int id)
        {
            List<SelectListItem> items = new List<SelectListItem>();

            Category root = db.Categorys.Find(id);
            SelectListItem item = new SelectListItem { Text = root.CategoryName, Value = root.ID.ToString() };


            LoopToAppendChildrenSelectListItem(items, item);
            return items;
        }
        private string a = "";
        //
        public void LoopToAppendChildrenSelectListItem(List<SelectListItem> items, SelectListItem rootItem)
        {


            var subItems = GetCategoryListByParentID(int.Parse(rootItem.Value));

            if (subItems.Count > 0)
            {

                foreach (var subItem in subItems)
                {
                    if (subItem.CategoryParentID == 2)
                    {
                        a = "";
                    }

                    SelectListItem Item = new SelectListItem { Text = a + subItem.CategoryName, Value = subItem.ID.ToString() };
                    items.Add(Item);
                    a += "……";
                    LoopToAppendChildrenSelectListItem(items, Item);

                }

            }

        }

        // GET: /Article/
        public ActionResult Index()
        {
            return View(db.Articles.ToList());
        }

        // GET: /Article/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // GET: /Article/Create
        public ActionResult Create()
        {

            
            ViewData["Categorylist"] = GetCategorySelectList(2);
            return View();

            
        }

        // POST: /Article/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "ID,Title,Category,Content,Author,CodeTitle,Keywords,Description,Hot,Essence,Top,Tags,CreatTime,LastUpdateTime,Password,Comment")] Article article)
        {
            if (ModelState.IsValid)
            {
                db.Articles.Add(article);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(article);
        }

        // GET: /Article/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            ViewData["Categorylist"] = GetCategorySelectList(2);
            return View(article);
        }

        // POST: /Article/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "ID,Title,Category,Content,Author,CodeTitle,Keywords,Description,Hot,Essence,Top,Tags,CreatTime,LastUpdateTime,Password,Comment")] Article article)
        {
            if (ModelState.IsValid)
            {
                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(article);
        }

        // GET: /Article/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: /Article/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = db.Articles.Find(id);
            db.Articles.Remove(article);
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
