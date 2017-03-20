using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SkyWebApplication.Models;

namespace SkyWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private SkyWebContext db = new SkyWebContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult FileUpload()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Xiangqing()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult List()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Datatable()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Add()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Charts()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        /// <summary>
        /// 返回Json格式数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetTreeJson(int id)
        {
            //var nodeA = new List<Node>();
            //nodeA.Add(new Node(4, "A01", null));
            //nodeA.Add(new Node(5, "A02", null));
            //nodeA.Add(new Node(6, "A03", null));

            //var nodeB = new List<Node>();
            //nodeB.Add(new Node(7, "B07", null));
            //nodeB.Add(new Node(8, "B08", null));
            //nodeB.Add(new Node(9, "B09", null));

            //var nodes = new List<Node>();
            //nodes.Add(new Node(1, "A01", nodeA));
            //nodes.Add(new Node(2, "B02", nodeB));
            //nodes.Add(new Node(3, "A03", null));

            var nodes = new List<Node>();

    
            List<Category> categorys = Getcategorys(id);
           
            for (int i = 0; i < categorys.Count; i++)
            {
                if (Getcategorys(categorys[i].CategoryParentID).Count > 0)
                { 
                
                }
                else
                {
                    nodes.Add(new Node(categorys[i].ID, categorys[i].CategoryName, null));
                }
               // nodes.Add(new Node(categorys[i].ID, categorys[i].CategoryName, List < Node > node));
            }

            return Json(nodes, JsonRequestBehavior.AllowGet);
        }

        public List<Category> Getcategorys(int id)
        {
            var categorys = from s in db.Category
                            orderby s.ID descending
                            where s.CategoryParentID == id
                            select s;
            return categorys.ToList();

        }
        public ActionResult Dictionary()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }

    /// <summary>
    /// Tree类
    /// </summary>
    public class Node
    {
        public Node() { }
        public Node(int id, string str, List<Node> node)
        {
            nodeId = id;
            text = str;
            nodes = node;
        }
        public int nodeId;    //树的节点Id，区别于数据库中保存的数据Id。若要存储数据库数据的Id，添加新的Id属性；若想为节点设置路径，类中添加Path属性
        public string text;   //节点名称
        public List<Node> nodes;    //子节点，可以用递归的方法读取，方法在下一章会总结
    }
}