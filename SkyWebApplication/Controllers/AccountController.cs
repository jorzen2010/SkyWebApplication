using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using SkyWebApplication.Attributes;
using SkyWebApplication.Models;
using SkyWebApplication.DAL;
using System.Data;
using System.Collections;

namespace SkyWebApplication.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private SkyWebContext db = new SkyWebContext();
        
        //
        // GET: /Account/
        public ActionResult Login()
        {
            FormsAuthentication.SignOut();
            if (!string.IsNullOrEmpty(Request["returnUrl"]))
            {
                
                ViewBag.returnUrl = Request["returnUrl"].ToString();
                ViewBag.msg = "对不起，您的权限不够，不允许访问！";
            
            }
            
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Response.Cookies["uname"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["uid"].Expires = DateTime.Now.AddDays(-1);
            System.Web.HttpContext.Current.Session["uid"] = null;
            System.Web.HttpContext.Current.Session["uname"] = null;
            return View("Login");
        }
        [HttpPost]
        public ActionResult Login(FormCollection fc)
        {
            string username = fc["UserName"];
            string password = fc["Password"];
            bool rememberme = fc["RememberMe"]==null?false:true;
            string reurnUrl = fc["ReturnUrl"];




            var sysUsers = from s in db.SysUsers
                           orderby s.ID ascending
                           where (s.UserName == username && (s.Password == password))
                           select s;


            if (sysUsers.Count()>0)
            {
                HttpCookie cookie = new HttpCookie("uname");
                cookie.Value = username;
                System.Web.HttpContext.Current.Response.Cookies.Add(cookie);

                HttpCookie cookieid = new HttpCookie("uid");
                cookieid.Value = sysUsers.First().ID.ToString();
                System.Web.HttpContext.Current.Response.Cookies.Add(cookieid);

                System.Web.HttpContext.Current.Session["uid"] = sysUsers.First().ID.ToString();
                System.Web.HttpContext.Current.Session["uname"] = username;

                FormsAuthentication.SetAuthCookie(username, rememberme);

                if (string.IsNullOrEmpty(reurnUrl))
                {
                    return Redirect("~/");
                }
                else
                {
                    ViewBag.msg = "没有权限";
                    return Redirect(reurnUrl);
                }
            }
            else
            {
                ViewBag.msg = "用户名或密码错误了";
                return View();
            
            }
            
        
        }

        public ActionResult About()
        { return View(); }
	}
}