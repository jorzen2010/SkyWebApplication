﻿using System;
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
    public class SettingController : Controller
    {
        private SkyWebContext db = new SkyWebContext();
      
        public ActionResult Index()
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            int id = 1;
            Setting setting = db.Settings.Find(id);
            if (setting == null)
            {
                return HttpNotFound();
            }
            return View(setting);
        }

        // POST: /Setting/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Index([Bind(Include="Id,SiteName,DomainName,Logo,Copyright,Statistics,Protocol,Title,Keywords,Description,FileUploadUrl,EditorUploadUrl,ImgUploadUrl,AvatarUploadUrl,BaseUrl,WXAppID,WXAppSecret,WBAppID,WBAppSecret,QQAppID,QQAppSecret,MsgUserName,MsgPassword,MsgAPI,LockedMinutes,FailedPassword,CodeSeconds,CodeMinutes,EmailHost,EmailPort,EmailFrom,EmailUser,EmailPassword,ActiveMinutes,EmailCodeTitle,EmailCodeContent,EmailLinkTitle,EmailLinkContent,ResetLinkTitle,ResetLinkContent")] Setting setting)
        {
            if (ModelState.IsValid)
            {
                db.Entry(setting).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Setting", new { msg="success"});
            }
            return RedirectToAction("Index", "Setting", new {msg="error"});
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
