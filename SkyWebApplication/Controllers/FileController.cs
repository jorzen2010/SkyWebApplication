using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using Newtonsoft.Json;
using SkyWebApplication.DAL;

namespace SkyWebApplication.Controllers
{
    public class FileController : Controller
    {
        //
        // GET: /File/
        //public ActionResult Upload(string folder)
        //{
        //    if (Request.Files.Count > 0 && Request.Files[0] != null && !string.IsNullOrEmpty(Request.Files[0].FileName))
        //    {
        //        try
        //        {
        //            string fileName = CommonServices.Uploadfiles(folder, Request.Files[0]);
        //            var a = new { success = true, file = fileName, url = fileName };


        //            return Content(JsonConvert.SerializeObject(a));
        //        }
        //        catch (Exception ex)
        //        {
        //            var b = new { success = false, msg = ex.Message };
        //            return Content(JsonConvert.SerializeObject(b));
        //        }
        //    }
        //    var c = new { success = false, msg = "未选择文件" };
        //    return Content(JsonConvert.SerializeObject(c));
        //}

        public JsonResult Upload(string folder)
        {
            Message msg = new Message();
            if (Request.Files.Count > 0 && Request.Files[0] != null && !string.IsNullOrEmpty(Request.Files[0].FileName))
            {
                try
                {
                    string fileName = CommonServices.Uploadfiles(folder, Request.Files[0]);
                    msg.MessageStatus = "true";
                    msg.MessageInfo = "上传成功";
                    msg.MessageUrl = fileName;

                }
                catch (Exception ex)
                {
                    var b = new { success = false, msg = ex.Message };
                    msg.MessageStatus = "false";
                    msg.MessageInfo = "上传失败";
                }
            }
            else
            {
                msg.MessageStatus = "false";
                msg.MessageInfo = "未选择文件";
            
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

       
	}
}