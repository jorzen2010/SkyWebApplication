using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SkyWebApplication.Models;
using SkyWebApplication.DAL;

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
        ///// <summary>
        ///// 返回Json格式数据
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //public JsonResult GetTreeJson(int id)
        //{
        //    //var nodeA = new List<Node>();
        //    //nodeA.Add(new Node(4, "A01", null));
        //    //nodeA.Add(new Node(5, "A02", null));
        //    //nodeA.Add(new Node(6, "A03", null));

        //    //var nodeB = new List<Node>();
        //    //nodeB.Add(new Node(7, "B07", null));
        //    //nodeB.Add(new Node(8, "B08", null));
        //    //nodeB.Add(new Node(9, "B09", null));

        //    //var nodes = new List<Node>();
        //    //nodes.Add(new Node(1, "A01", nodeA));
        //    //nodes.Add(new Node(2, "B02", nodeB));
        //    //nodes.Add(new Node(3, "A03", null));

        //    var nodes = new List<Node>();


        //    List<Node> categorys = Getcategorys(0);

        //    for (int i = 0; i < categorys.Count; i++)
        //    {

        //        nodes.Add(new Node(categorys[i].nodeId, categorys[i].text, Getcategorys(categorys[i].nodeId)));

        //       // nodes.Add(new Node(categorys[i].ID, categorys[i].CategoryName, List < Node > node));
        //    }

        //    return Json(nodes, JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult AddTree(int ParentID, Node pNode)
        //{

        //    List<Category> categorys = Getcategorys(ParentID);

        //    if (categorys.Count == 0)
        //    {
                
        //        return Json(pNode, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        foreach (Category category in categorys)
        //        {
        //            //声明节点
        //            Node node = new Node();

        //            //开始递归
        //            if (ParentID == 0)
        //            {
        //                node.nodeId = category.ID;
        //                node.text = category.CategoryName;
        //                node.nodes =

        //             //  node.Add(node);

        //                AddTree(category.ID, node);    //再次递归
        //            }
        //            else
        //            {
        //                node.nodeId = category.ID;
        //                node.text = category.CategoryName;

        //                pNode.nodes.Add(node);

        //                AddTree(category.ID, node);    //再次递归
        //            }
        //        }

        //    }

        //    return Json(categorysNode, JsonRequestBehavior.AllowGet);
        //}
        
        public JsonResult GetTreeJson()
        {
            List<Node> categorysNodes = new List<Node>();
            List<Category> data = Getcategorys(0);
            foreach (var tree in data)
            {
                List<Category> ChildCategoryList = Getcategorys(tree.ID);
                List<Node> ChildCategorysNodes = new List<Node>();
                if (ChildCategoryList.Count > 0)
                {
                    foreach (var childTree in ChildCategoryList)
                    {
                       // ChildCategorysNodes.Add(new Node(ChildCategoryList[i].ID,ChildCategoryList[i].ID,null));
                        categorysNodes.Add(new Node(tree.CategoryParentID, tree.CategoryName, ChildCategorysNodes));
                    
                    }
                
                }
            }
            return Json(categorysNodes, JsonRequestBehavior.AllowGet);
        }

        public void AddChildNode(int id)
        {
            List<Node> cNodes = new List<Node>();
            List<Category> data = Getcategorys(id);
            if (data.Count > 0)
            { 
                foreach (var tree in data)
            {
                Console.WriteLine(tree.CategoryName);
                AddChildNode(tree.ID);
            }
            
            }
           
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

        //public List<Node> Getcategorys(int? id)
        //{
        //    int parentid = id ??0;
        //    var categorys = from s in db.Categorys
        //                    orderby s.ID descending
        //                    where s.CategoryParentID == parentid
        //                    select s;
        //    var nodes = new List<Node>();
        //    if (categorys.Count() == 0)
        //    {

        //    }
        //    else
        //    { 
        //     for (int i = 0; i < categorys.Count(); i++)
        //    {

        //        nodes.Add(new Node(categorys.ToList()[i].CategoryParentID, categorys.ToList()[i].CategoryName, Getcategorys(categorys.ToList()[i].CategoryParentID)));
        //    }
        //    }


        //    return nodes;

        //}
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