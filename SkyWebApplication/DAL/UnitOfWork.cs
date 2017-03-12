using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using SkyWebApplication.Models;


namespace SkyWebApplication.DAL
{
    public class UnitOfWork : IDisposable
    {
        private SkyWebContext context = new SkyWebContext();

        private GenericRepository<SysUser> sysUsertRepository;

        public GenericRepository<SysUser> SysUserRepository
        {
            get
            {

                if (this.sysUsertRepository == null)
                {
                    this.sysUsertRepository = new GenericRepository<SysUser>(context);
                }
                return sysUsertRepository;
            }
        }

        

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}