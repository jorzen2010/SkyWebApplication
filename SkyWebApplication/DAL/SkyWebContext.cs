using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using SkyWebApplication.Models;

namespace SkyWebApplication.DAL
{
    public class SkyWebContext:DbContext
    {
        public SkyWebContext()
            : base("SkyWebContext")
        { 

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           // base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<SysUser> SysUsers { get; set; }
        public DbSet<SysRole> SysRoles { get; set; }
        public DbSet<SysUserRole> SysUserRoles { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<Dictionary> Dictionarys { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Article> Articles { get; set; }


    }
}