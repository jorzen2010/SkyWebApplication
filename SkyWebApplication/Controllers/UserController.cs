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
using PagedList.Mvc;

namespace SkyWebApplication.Controllers
{
    public class UserController : Controller
    {
        private SkyWebContext db = new SkyWebContext();
        private UnitOfWork unitOfWork = new UnitOfWork();
        

        // GET: /不带分页
        //public ActionResult Index()
        //{
        //    var sysUsers = unitOfWork.SysUserRepository.Get().ToList();
        //    return View(sysUsers);
        //}


        // GET: /全部取出数据后分页
        //public ActionResult Index(int? page)
        //{
        //    int pageNumber = page ?? 1;
        //    int pageSize = 1;
        //    var sysUsers = unitOfWork.SysUserRepository.Get().ToList();
        //    return View(sysUsers.ToPagedList(pageNumber, pageSize));

        //}
        // GET: 按需要取数据分页
        public ActionResult Index(int? page)
        {
            int pageIndex = page ?? 1;
            int pageSize = 1;
            int totalCount = 0;
            var sysUsers = GetsysUsers(pageIndex, pageSize, ref totalCount);
            var sysUsersAsIPageList = new StaticPagedList<SysUser>(sysUsers, pageIndex, pageSize, totalCount);
            return View(sysUsersAsIPageList);

        }
        // GET: Ajax方式获取数据，分页方式带解决
        public ActionResult List(int? page)
        {
            return View();
        
        }

        public ActionResult AjaxPage(int? page)
        {
            int pageIndex = page ?? 1;
            int pageSize = 1;
            int totalCount = 0;
            var sysUsers = GetsysUsers(pageIndex, pageSize, ref totalCount);
            var sysUsersAsIPageList = new StaticPagedList<SysUser>(sysUsers, pageIndex, pageSize, totalCount);
            return Json(new { sysUsers = sysUsersAsIPageList, pager = sysUsersAsIPageList.GetMetaData() }, JsonRequestBehavior.AllowGet);
        }

        public List<SysUser> GetsysUsers(int pageIndex, int pageSize, ref int totalCount)
        {
            var sysUsers = (from p in db.SysUsers
                           orderby p.ID descending
                           select p).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            totalCount = db.SysUsers.Count();
            return sysUsers.ToList();

        }

        // GET: /User/Details/5
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

        // GET: /User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /User/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,UserName,Password,Email")] SysUser sysuser)
        {
            if (ModelState.IsValid)
            {
                db.SysUsers.Add(sysuser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sysuser);
        }

        // GET: /User/Edit/5
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

        // POST: /User/Edit/5
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

        // GET: /User/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: /User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SysUser sysuser = db.SysUsers.Find(id);
            db.SysUsers.Remove(sysuser);
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
