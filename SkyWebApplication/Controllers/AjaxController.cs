using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SkyWebApplication.DAL;

namespace SkyWebApplication.Controllers
{
    public class AjaxController : Controller
    {
        private SkyWebContext db = new SkyWebContext();
        private UnitOfWork unitOfWork = new UnitOfWork();
        public JsonResult updateBySql(string sql)
        {
            //string sql = "update sysUser set Status=";
            Message msg = new Message();
            try
            {
                unitOfWork.SysUserRepository.UpdateWithRawSql(sql);
                msg.MessageStatus = "true";
                msg.MessageInfo = "更新成功";
            }
            catch
            {
                msg.MessageStatus = "false";
                msg.MessageInfo = "邮箱格式不正确";
            
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

       
       
	}
}