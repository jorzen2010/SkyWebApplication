using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using SkyWebApplication.Models;

namespace SkyWebApplication.DAL
{
    public class SkyWebInitializer :DropCreateDatabaseIfModelChanges<SkyWebContext>
    {
        protected override void Seed(SkyWebContext context)
        {
            base.Seed(context);
            var sysUsers = new List<SysUser>
            {
                new SysUser{UserName="Tom",Email="Tom@163.com",Password="1",Status=true},
                new SysUser{UserName="Jerry",Email="Jerry@163.com",Password="2",Status=true},
                new SysUser{UserName="Jeem",Email="Jeem@163.com",Password="2",Status=false}
            };
            sysUsers.ForEach(s => context.SysUsers.Add(s));
            context.SaveChanges();

            var sysRoles = new List<SysRole>
            {
                new SysRole{RoleName="超级管理员",RoleDesc="超级管理员"},
                new SysRole{RoleName="管理员",RoleDesc="管理员"}
            };
            sysRoles.ForEach(s => context.SysRoles.Add(s));
            context.SaveChanges();


        }
    }
}