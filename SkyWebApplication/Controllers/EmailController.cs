using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using SkyWebApplication.DAL;
using Common;

namespace SkyWebApplication.Controllers
{
    public class EmailController : Controller
    {
        //
        // GET: /Email/
        public JsonResult SendEmail(EmailEntity emailEntity)
        {
            Message msg = new Message();
            try
            {

                EmailServices.SendEmail(emailEntity);
                msg.MessageStatus = "true";
                msg.MessageInfo = "上传成功";

            }
            catch (Exception ex)
            {
                msg.MessageStatus = "false";
                msg.MessageInfo = ex.Message;
            }

            return Json(msg, JsonRequestBehavior.AllowGet);
        }
    }
}