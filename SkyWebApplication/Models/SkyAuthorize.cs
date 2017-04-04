using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyWebApplication.Models
{
    public class SkyAuthorize
    {
        public int ID { get; set; }
        public string SkyControllerName { get; set; }
        public string SkyActionName { get; set; }
        public string SkyLinkTitle { get; set; }
        public string SkyRolesID { get; set; }

    }
}