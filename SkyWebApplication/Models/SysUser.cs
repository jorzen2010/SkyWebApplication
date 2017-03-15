using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyWebApplication.Models
{
    public class SysUser
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }
        public virtual ICollection<SysUserRole> SysUserRoles { get;set;}
    }
}