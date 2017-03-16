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
using PagedList;

namespace SkyWebApplication.Controllers
{
    public class SysUserController : Controller
    {
        private SkyWebContext db = new SkyWebContext();

        // GET: /获取分页数据/
        public ActionResult Index(int? page)
        {
            int pageIndex = page ?? 1;
            int pageSize = 2;
            int totalCount = 0;
            var sysUsers = GetsysUsers(pageIndex, pageSize, ref totalCount);
            var sysUsersAsIPageList = new StaticPagedList<SysUser>(sysUsers, pageIndex, pageSize, totalCount);
            return View(sysUsersAsIPageList);

        }

        public List<SysUser> GetsysUsers(int pageIndex, int pageSize, ref int totalCount)
        {
            var sysUsers = (from p in db.SysUsers
                            orderby p.ID descending
                            select p).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            totalCount = db.SysUsers.Count();
            return sysUsers.ToList();

        }

        //原来的首页，准备删除
        //// GET: /SysUser/
        //public ActionResult Index()
        //{
        //    return View(db.SysUsers.ToList());
        //}

        // GET: /SysUser/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SysUser sysuser = db.SysUsers.Find(id);
            if (sysuser == null)
            {
                return HttpNotFound();
            }
            return View(sysuser);
        }

        // GET: /SysUser/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /SysUser/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SysUser sysuser)
        {
            if (ModelState.IsValid)
            {
                db.SysUsers.Add(sysuser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sysuser);
        }

        // GET: /SysUser/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SysUser sysuser = db.SysUsers.Find(id);
            if (sysuser == null)
            {
                return HttpNotFound();
            }
            return View(sysuser);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult UpdateStatus(int? id,bool status)
        {
            Message msg = new Message();
            if (id == null)
            {
                msg.MessageStatus = "false";
                msg.MessageInfo = "找不到ID";
            }
            SysUser sysuser = db.SysUsers.Find(id);
            sysuser.Status = status;
            if (ModelState.IsValid)
            {
                db.Entry(sysuser).State = EntityState.Modified;
                db.SaveChanges();
                msg.MessageStatus = "true";
                msg.MessageInfo = "已经更改为" + sysuser.Status.ToString() ;
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        // POST: /SysUser/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,UserName,Password,Email")] SysUser sysuser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sysuser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sysuser);
        }

        // GET: /SysUser/Delete/5
        public ActionResult Deleteq(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SysUser sysuser = db.SysUsers.Find(id);
            if (sysuser == null)
            {
                return HttpNotFound();
            }
            return View(sysuser);
        }

        // POST: /SysUser/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    SysUser sysuser = db.SysUsers.Find(id);
        //    db.SysUsers.Remove(sysuser);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteConfirmed(int id)
        {
            Message msg = new Message();
            if (id == null)
            {
                msg.MessageStatus = "false";
                msg.MessageInfo = "找不到ID";
            }
            else
            {
                SysUser sysuser = db.SysUsers.Find(id);
                db.SysUsers.Remove(sysuser);
                db.SaveChanges();
                msg.MessageStatus = "true";
                msg.MessageInfo = "删除成功";
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
