using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using SkyWebApplication.Attributes;
using SkyWebApplication.Models;
using SkyWebApplication.DAL;
using Common;
using PagedList;

namespace SkyWebApplication.Controllers
{
    public class ImgFileController : Controller
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
            SelectListItem itemDefault = new SelectListItem { Text = "请选择文件类别", Value = "" };
            items.Add(itemDefault);
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

        public ActionResult Index(int? page)
        {
            Pager pager = new Pager();
            pager.table = "ImgandFile";
            pager.strwhere = "1=1";
            pager.PageSize = 2;
            pager.PageNo = page ?? 1;
            pager.FieldKey = "ID";
            pager.FiledOrder = "ID desc";
            pager = CommonDal.GetPager(pager);
            IList<ImgandFile> articles = DataConvertHelper<ImgandFile>.ConvertToModel(pager.EntityDataTable);
            var sysUsersAsIPageList = new StaticPagedList<ImgandFile>(articles, pager.PageNo, pager.PageSize, pager.Amount);
            return View(sysUsersAsIPageList);
        }

        public ActionResult Search(int? page, string keywords)
        {
            Pager pager = new Pager();
            pager.table = "ImgandFile";
            pager.strwhere = "FileTitle like '%" + keywords + "%'";
            pager.PageSize = 2;
            pager.PageNo = page ?? 1;
            pager.FieldKey = "ID";
            pager.FiledOrder = "ID desc";
            pager = CommonDal.GetPager(pager);
            IList<ImgandFile> articles = DataConvertHelper<ImgandFile>.ConvertToModel(pager.EntityDataTable);
            var sysUsersAsIPageList = new StaticPagedList<ImgandFile>(articles, pager.PageNo, pager.PageSize, pager.Amount);
            return View(sysUsersAsIPageList);
        }

        // GET: /ImgFile/
        //public ActionResult Index()
        //{
        //    return View(db.ImgandFiles.ToList());
        //}

        // GET: /ImgFile/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImgandFile imgandfile = db.ImgandFiles.Find(id);
            if (imgandfile == null)
            {
                return HttpNotFound();
            }
            return View(imgandfile);
        }

        // GET: /ImgFile/Create
        public ActionResult Create()
        {
            ViewData["Categorylist"] = GetCategorySelectList(3);
            return View();
        }

        // POST: /ImgFile/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,FileCategroy,FileTitle,FileInfo,FilePath,HrefUrl")] ImgandFile imgandfile)
        {
            if (ModelState.IsValid)
            {
                db.ImgandFiles.Add(imgandfile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(imgandfile);
        }

        // GET: /ImgFile/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImgandFile imgandfile = db.ImgandFiles.Find(id);
            if (imgandfile == null)
            {
                return HttpNotFound();
            }
            return View(imgandfile);
        }

        // POST: /ImgFile/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,FileCategroy,FileTitle,FileInfo,FilePath,HrefUrl")] ImgandFile imgandfile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(imgandfile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(imgandfile);
        }

        // GET: /ImgFile/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImgandFile imgandfile = db.ImgandFiles.Find(id);
            if (imgandfile == null)
            {
                return HttpNotFound();
            }
            return View(imgandfile);
        }

        // POST: /ImgFile/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteConfirmed(int? id)
        {
            Message msg = new Message();
            if (id == null)
            {
                msg.MessageStatus = "false";
                msg.MessageInfo = "找不到ID";
            }
            else
            { 
                ImgandFile imgandfile = db.ImgandFiles.Find(id);
            try
            {
                FileTools.DeleteFile(imgandfile.FilePath);
                db.ImgandFiles.Remove(imgandfile);

                db.SaveChanges();
                msg.MessageStatus = "true";
                msg.MessageInfo = "删除成功";

            }
            catch { 
            db.ImgandFiles.Remove(imgandfile);
            
            db.SaveChanges();
            msg.MessageStatus = "true";
            msg.MessageInfo = "文件删除失败，数据删除成功，请联系管理员手动删除文件！";
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
