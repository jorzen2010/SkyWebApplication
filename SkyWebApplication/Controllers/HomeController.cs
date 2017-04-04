using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SkyWebApplication.Models;
using SkyWebApplication.DAL;
using SkyWebApplication.Attributes;
using Common;
using Microsoft.Owin.Security;

namespace SkyWebApplication.Controllers
{
    
    public class HomeController : Controller
    {
        private SkyWebContext db = new SkyWebContext();
        private List<Node> categorysNode = new List<Node>();
        
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
       
     

        [HttpPost]
        public JsonResult GetTreeJsond(int id)
        {
            var nodes = new List<Node>();


            List<Category> categorys = Getcategorys(id);

            for (int i = 0; i < categorys.Count; i++)
            {

                nodes.Add(new Node(categorys[i].ID, categorys[i].CategoryName, null));

                // nodes.Add(new Node(categorys[i].ID, categorys[i].CategoryName, List < Node > node));
            }

            return Json(nodes, JsonRequestBehavior.AllowGet);
        }

        public List<Category> Getcategorys(int ParentID)
        {
            var categorys = from s in db.Categorys
                            orderby s.ID descending
                            where s.CategoryParentID == ParentID
                            select s;

            return categorys.ToList();

        }
        public void LoopToAppendChildren(List<Category> all, Category curItem)
        {
           // var subItems = all.Where(ee => ee.ParentId == curItem.Id).ToList();
            var subItems = Getcategorys(curItem.ID);
            curItem.ChildCategory = new List<Category>();
            curItem.ChildCategory.AddRange(subItems);
            foreach (var subItem in subItems)
            {
                LoopToAppendChildren(all, subItem);//新闻1.1
            }
        }
        public ContentResult testjson()
        {
            var categorys = from s in db.Categorys
                            orderby s.ID descending
                            select s;
           return Content(JsonHelper.ListToJson(categorys.ToList()));

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

    public class NewsType
    {
        public int Id;
        public int ParentId;
        public string Name;
        public List<NewsType> children;

       
    
    }


}