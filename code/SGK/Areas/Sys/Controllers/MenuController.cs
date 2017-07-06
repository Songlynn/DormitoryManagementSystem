using FineUIMvc;
using SGK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SGK.Areas.Sys.Controllers
{
    public class MenuController : Controller
    {
        private SGK_lynnEntities db = new SGK_lynnEntities();

        // GET: Sys/Menu
        public ActionResult Index()
        {
            BindDate_Tree();
            return View();
        }

        public void BindDate_Tree()
        {
            List<TreeNode> nodes = new List<TreeNode>();
            var father = from m in db.T_Menu where m.FatherID == null select m;
            foreach(var m in father)
            {
                TreeNode node = new TreeNode();
                node.Text = m.Name;
                node.NodeID = m.ID;
                if (m.Url != null)
                {
                    node.NavigateUrl = Url.Content(m.Url);
                }
                nodes.Add(node);
                BindDate_SubTree(m.ID, node);
            }
            ViewBag.treeMenu = nodes.ToArray();

        }
        public void BindDate_SubTree(string fatherid, TreeNode treeNode)
        {
            var menu = from m in db.T_Menu where m.FatherID == fatherid select m;
            foreach(var m in menu)
            {
                TreeNode node = new TreeNode();
                node.Text = m.Name;
                node.NodeID = m.ID;
                if (m.Url != null)
                {
                    node.NavigateUrl = Url.Content(m.Url);
                }
                treeNode.Nodes.Add(node);
            }
        }
    }
}