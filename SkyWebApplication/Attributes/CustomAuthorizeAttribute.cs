using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Collections;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using SkyWebApplication.Models;
using SkyWebApplication.DAL;

namespace SkyWebApplication.Attributes
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        private SkyWebContext db = new SkyWebContext();
        // 只需重载此方法，模拟自定义的角色授权机制  
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string actionName = filterContext.ActionDescriptor.ActionName;
            

            //从数据库中获取一个控制器方法的权限
            // string roles = db.SkyAuthorizes.Where(s=>s.SkyControllerName==controllerName&&s=>s.SkyActionName==actionName);
            var roles = from r in db.SkyAuthorizes
                        where (r.SkyControllerName == controllerName && r.SkyActionName == actionName)
                        orderby r.ID ascending
                        select r.SkyRolesID;

            if (roles.Count()>0)
            {
                Roles = roles.FirstOrDefault().ToString();
            }
            else
            { Roles = ""; }

           

            base.OnAuthorization(filterContext);
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {

            if (httpContext == null)
            {
                throw new ArgumentException("HttpContext");
            }
            if (Roles == null || Roles.Length == 0)
            {
                return true;
            }
            if (!httpContext.User.Identity.IsAuthenticated)
            {
                return false;
            }
            string UserName = httpContext.User.Identity.Name;

            //找到这个UserID

            var sysUsers = db.SysUsers.Where(s => s.UserName == UserName);
            //找到这个userID对应的roleID
            var b = sysUsers.ToList();
            var userRoles = db.SysUserRoles.Where(d => d.SysUserID == sysUsers.FirstOrDefault().ID).Select(d => d.SysRoleID);
            //找到这个roleid对应的rolename
            var a = userRoles.ToList();
            var loginroles = from r in db.SysRoles
                             where (userRoles.Contains(r.ID))
                             orderby r.ID ascending
                             select r.RoleName;
            var userlist = loginroles.ToList();
            //将Roles用数组切割一下
            string[] sArray = Roles.Split(',');

            for (int i = 0; i < sArray.Length; i++)
            {
                string s = sArray[i].ToString();
                if (userlist.Contains(s))
                {
                    return true;
                }
            }
            return false;
        }

    }
}