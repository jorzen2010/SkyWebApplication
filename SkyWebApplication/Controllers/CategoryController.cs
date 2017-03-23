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
    //这是一个用于bootstraptreeview的node类
    public class bvnode
    {
        public string text;
        public int pid;
        public int id;
        public List<bvnode> nodes;
    
    }
    public class CategoryController : Controller
    {
        private SkyWebContext db = new SkyWebContext();

       
        public ActionResult Index()
        {
            return View(db.Categorys.ToList());
        }


        //获取一个Category
        public JsonResult GetOneCategory(int? id)
        {
            if (id == null)
            {
                return null;
            }
            Category category = db.Categorys.Find(id);
            category.CategoryParentName = db.Categorys.Find(category.CategoryParentID).CategoryName;
            
            if (category == null)
            {
                return null;
            }
            return Json(category, JsonRequestBehavior.AllowGet);
            
        }
        //构建一个CategoryList的json
        public JsonResult TreeJson()
        {

            Category root = db.Categorys.Find(1);
            bvnode rootnode = new bvnode();
            rootnode.text = root.CategoryName;
            rootnode.id = root.ID;
            rootnode.pid = root.CategoryParentID;
            LoopToAppendChildren(rootnode);
            return Json(rootnode.nodes, JsonRequestBehavior.AllowGet);
        }
        //递归出所有字典数据
        public void LoopToAppendChildren(bvnode rootnode)
        {
            var subItems = Getnodes(rootnode.id);
            if (subItems.Count > 0)
            {
                rootnode.nodes = new List<bvnode>();
                rootnode.nodes.AddRange(subItems);
                foreach (var subItem in subItems)
                {
                    LoopToAppendChildren(subItem);
                }
            
            }
           
        }

        public List<bvnode> Getnodes(int ParentID)
        {
            var categorys = from s in db.Categorys
                            orderby s.CategorySort ascending
                            where s.CategoryParentID == ParentID
                            select s;
            List<bvnode> nodes = new List<bvnode>();
            foreach(var category in categorys)
            { 
                bvnode node=new bvnode();
                node.id=category.ID;
                node.pid=category.CategoryParentID;
                node.text=category.CategoryName;
                nodes.Add(node);
            
            }

            return nodes.ToList();

        }

      
     
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
