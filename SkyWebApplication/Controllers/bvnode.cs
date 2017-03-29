using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyWebApplication.Controllers
{
    //这是一个用于bootstraptreeview的node类
    public class bvnode
    {
        public string text;
        public int pid;
        public int id;
        public List<bvnode> nodes;
    }
}