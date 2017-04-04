using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SkyWebApplication.Models
{
    public class ImgandFile
    {
        public int ID { get; set; }
        [Display(Name = "文件类型")]
        public int FileCategroy { get; set; }
        [Display(Name = "文件标题")]
        public string  FileTitle { get; set; }
        [Display(Name = "文件备注")]
        public string FileInfo { get; set; }
        [Display(Name = "文件地址")]
        public string FilePath { get; set; }
        [Display(Name = "文件链接")]
        public string HrefUrl { get; set; }
    }
}