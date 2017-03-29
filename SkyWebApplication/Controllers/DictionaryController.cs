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
    public class DictionaryController : Controller
    {
        private SkyWebContext db = new SkyWebContext();

        // 页面显示
        public ActionResult Index()
        {
            return View();

        }
        //获取一个Dictionary
        public JsonResult GetOneDictionary(int? id)
        {
            if (id == null)
            {
                return null;
            }
            Dictionary dictionary = db.Dictionarys.Find(id);
            dictionary.DictionaryParentName = db.Dictionarys.Find(dictionary.DictionaryParentID).DictionaryName;

            if (dictionary == null)
            {
                return null;
            }
            return Json(dictionary, JsonRequestBehavior.AllowGet);

        }
        //构建一个DictionaryList的json
        public JsonResult TreeJson()
        {

            Dictionary root = db.Dictionarys.Find(1);
            bvnode rootnode = new bvnode();
            rootnode.text = root.DictionaryName;
            rootnode.id = root.ID;
            rootnode.pid = root.DictionaryParentID;
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
        //数据库中的数据封装成jsonlist格式的node
        public List<bvnode> Getnodes(int ParentID)
        {
            var dictionarys = from s in db.Dictionarys
                            orderby s.DictionarySort ascending
                            where s.DictionaryParentID == ParentID
                            select s;
            List<bvnode> nodes = new List<bvnode>();
            foreach (var dictionary in dictionarys)
            {
                bvnode node = new bvnode();
                node.id = dictionary.ID;
                node.pid = dictionary.DictionaryParentID;
                node.text = dictionary.DictionaryName;
                nodes.Add(node);

            }

            return nodes.ToList();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DictionaryName,DictionaryInfo,DictionaryParentID,DictionaryStatus,DictionarySort")] Dictionary dictionary)
        {

            if (ModelState.IsValid)
            {
                db.Dictionarys.Add(dictionary);
                db.SaveChanges();

                return RedirectToAction("Index", "Dictionary", new { option = dictionary.DictionaryName });
            }
            return RedirectToAction("Index", "Dictionary", new { option = dictionary.DictionaryName });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DictionaryName,DictionaryInfo,DictionaryParentID,DictionaryStatus,DictionarySort")] Dictionary dictionary)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dictionary).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Dictionary", new { option = dictionary.DictionaryName });
            }
            return RedirectToAction("Index", "Dictionary", new { option = dictionary.DictionaryName });
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
                var childdictionarys = from s in db.Dictionarys
                                     orderby s.DictionarySort ascending
                                     where s.DictionaryParentID == id
                                     select s;

                if (childdictionarys.Count() > 0)
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
                    else { 
                    Dictionary dictionary = db.Dictionarys.Find(id);
                    db.Dictionarys.Remove(dictionary);
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
