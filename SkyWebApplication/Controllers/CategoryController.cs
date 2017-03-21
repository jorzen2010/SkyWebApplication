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
    public class CategoryController : Controller
    {
        private SkyWebContext db = new SkyWebContext();

        public List<Category> GetAllcategorys()
        {
            var categorys = from s in db.Categorys
                            orderby s.ID descending
                            select s;

            return categorys.ToList();

        }


        private List<Category> CategoryCacheAllList { get; set; }

        [Route("")]
        public HttpResponseMessage Get()
        {

            var list =new List<Category> ();
            if (list == null)
            {
                CategoryCacheAllList = GetAllcategorys(); //取得数据库里面所有数据

               // CategoryCacheAllList = CategoryService.GetCacheList(); //取得数据库里面所有数据
                list = new List<Category>();
                CategoryJson(list, "1");
              //  CacheHelper<List<Category>>.SetCache(CategoryAllListCacheKEY, list);
            }
            //return Request.CreateResponse(HttpStatusCode.OK, list);
            //下面的代码这个没试
            string json = JsonConvert.SerializeObject(categoryList, Formatting.Indented);
            return json; 
        }


        /// <summary>
        /// 取得兄弟节点
        /// </summary>
        /// <param name="categoryList"></param>
        /// <param name="parentCode"></param>
        public void CategoryJson(List<Category> categoryList, string parentCode)
        {
            var list = CategoryCacheAllList.FindAll(p => p.ParentCode == parentCode);
            if (list.Count > 0)
            {
                foreach (var item in list)
                {
                    CategoryTreeJson(item, item.Code);
                    categoryList.Add(item);
                }
            }
        }

        /// <summary>
        /// 递归出子对象
        /// </summary>
        /// <param name="sbCategory"></param>
        /// <param name="parentCode"></param>
        private void CategoryTreeJson(Category sbCategory, string parentCode)
        {
            var list = CategoryCacheAllList.FindAll(p => p.ParentCode == parentCode);
            if (list.Count > 0)
            {
                sbCategory.Children = new List<Category>();
                foreach (var item in list)
                {
                    CategoryTreeJson(item, item.Code);
                    sbCategory.Children.Add(item);
                }
            }
        }
        public ActionResult Index()
        {
            return View(db.Categorys.ToList());
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
