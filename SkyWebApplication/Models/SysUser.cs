using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace SkyWebApplication.Models
{
    public class SysUser
    {
        public SysUser()
        {
            this.Birthday = Convert.ToDateTime("1900-01-01");
            this.RegisterDateTime = System.DateTime.Now;
            this.LastLoginDateTime = Convert.ToDateTime("1799-01-01");
            this.AvatarUrl = "/plugins/flashavatar/img/avatar.jpg";
        
        }
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }
 
        public string RealName { get; set; }
        public string Sex { get; set; }
        public string AvatarUrl { get; set; }
        public string BirthdayType { get; set; }
        public DateTime Birthday { get; set; }
        public string MobileTelephone { get; set; }
        public string Telephone{ get; set; }
        public string QQNumber { get; set; }
        public string WebchatNumber { get; set; }
        public DateTime  RegisterDateTime { get; set; }
        public DateTime LastLoginDateTime { get; set; }
        public virtual ICollection<SysUserRole> SysUserRoles { get;set;}
    }
}