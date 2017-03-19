//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using Common;
//using Newtonsoft.Json;
//using SkyWebApplication.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Common;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using SkyWebApplication.DAL;

namespace SkyWebApplication.Controllers
{
    public class FileController : Controller
    {
        //
        // GET: /File/

  
        
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