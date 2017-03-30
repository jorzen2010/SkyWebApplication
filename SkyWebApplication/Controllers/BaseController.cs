using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SkyWebApplication.Models;
using SkyWebApplication.DAL;

namespace SkyWebApplication.Controllers
{
    public class BaseController : Controller
    {
        [HttpPost]
        public JsonResult SetFieldOneByOne(string table, string strwhere, string colname, string colvalue)
        {

            Message message = new Message();
            message.MessageStatus = Common.CommonDal.SetFiledOneByOne(table,strwhere,colname,colvalue);
            return Json(message, JsonRequestBehavior.AllowGet);
        }
       
	}
}