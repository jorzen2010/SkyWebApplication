using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.IO;

namespace Common
{
    public class CommonServices
    {
        //上传文件服务
        public static string Uploadfiles(string folder, HttpPostedFileBase file)
        {
            string UploadPath = "Resource/Upload";

            //提供平台特定的替换字符，该替换字符用于在反映分层文件系统组织的路径字符串中分隔目录级别
            var sep = Path.AltDirectorySeparatorChar.ToString();
            //指定为根目录
            var root = "~" + sep + UploadPath + sep;
            //拼接成路径
            var path = root + folder + sep;
            //找到这个路径
            var phicyPath = HostingEnvironment.MapPath(path);
            //判断是否存在，不存在创建一个
            if (!Directory.Exists(phicyPath))
            {
                Directory.CreateDirectory(phicyPath);
            }

            string extension = file.FileName.Substring(file.FileName.LastIndexOf('.'));

            string filename = CommonTools.ToUnixTime(System.DateTime.Now).ToString() + CommonTools.getRandomNumber() +
                              extension;


            file.SaveAs(phicyPath + filename);

            string fileuploadpath = "/" + UploadPath + "/" + folder + "/" + filename;

            return fileuploadpath;

        }


    }
}
