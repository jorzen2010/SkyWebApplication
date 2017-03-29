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
   
    
    public class DepartmentController : Controller
    {
        private SkyWebContext db = new SkyWebContext();

       
        public ActionResult Index()
        {
            ViewBag.smallTitle = "警告：请谨慎操作分类设置，不当操作可能会造成系统崩溃";
            return View();
        }


        //获取一个Department
        public JsonResult GetOneDepartment(int? id)
        {
            if (id == null)
            {
                return null;
            }
            Department department = db.Departments.Find(id);
            department.DepartmentParentName = db.Departments.Find(department.DepartmentParentID).DepartmentName;
            
            if (department == null)
            {
                return null;
            }
            return Json(department, JsonRequestBehavior.AllowGet);
            
        }
        //构建一个DepartmentList的json
        public JsonResult TreeJson()
        {

            Department root = db.Departments.Find(1);
            bvnode rootnode = new bvnode();
            rootnode.text = root.DepartmentName;
            rootnode.id = root.ID;
            rootnode.pid = root.DepartmentParentID;
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
            var departments = from s in db.Departments
                            orderby s.DepartmentSort ascending
                            where s.DepartmentParentID == ParentID
                            select s;
            List<bvnode> nodes = new List<bvnode>();
            foreach(var department in departments)
            { 
                bvnode node=new bvnode();
                node.id=department.ID;
                node.pid=department.DepartmentParentID;
                node.text=department.DepartmentName;
                nodes.Add(node);
            
            }

            return nodes.ToList();

        }

      
     
     

        // GET: /Department/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: /Department/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DepartmentName,DepartmentInfo,DepartmentParentID,DepartmentStatus,DepartmentSort")] Department department)
        {

            if (ModelState.IsValid)
            {
                db.Departments.Add(department);
                db.SaveChanges();

                return RedirectToAction("Index", "Department", new { option = department.DepartmentName });
            }
            return RedirectToAction("Index", "Department", new { option = department.DepartmentName });
        }


      
        // POST: /Department/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,DepartmentName,DepartmentInfo,DepartmentParentID,DepartmentStatus,DepartmentSort")] Department department)
        {
            if (ModelState.IsValid)
            {
                db.Entry(department).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Department", new { option = department.DepartmentName });
            }
            return RedirectToAction("Index", "Department", new { option = department.DepartmentName });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Delete(int? id)
        {
            Message msg = new Message();

            if (id == null)
            {
                msg.MessageStatus = "false";
                msg.MessageInfo = "找不到ID";
            }
            else
            {
                var childdepartments = from s in db.Departments
                                     orderby s.DepartmentSort ascending
                                     where s.DepartmentParentID == id
                                     select s;

                if (childdepartments.Count() > 0)
                {
                    msg.MessageStatus = "false";
                    msg.MessageInfo = "此ID节点下有子节点，不能删除";

                }
                else
                {
                    if (id == 2)
                    {
                        msg.MessageStatus = "false";
                        msg.MessageInfo = "此节点为系统内置节点不能删除，你可以修改此节点内容！";

                    }
                    else
                    {
                        Department department = db.Departments.Find(id);
                        db.Departments.Remove(department);
                        db.SaveChanges();
                        msg.MessageStatus = "true";
                        msg.MessageInfo = "删除成功";
                    }
                }
            }

            return Json(msg, JsonRequestBehavior.AllowGet);
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
