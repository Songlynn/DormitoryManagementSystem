using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FineUIMvc;
using SGK.Models;

namespace SGK.Controllers
{
    public class HomeController : BaseController
    {
        private SGK_lynnEntities db = new SGK_lynnEntities();

        public ActionResult Index()
        {
            if (Session["AccountID"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            BindDate_Tree(Session["RoleID"].ToString());
            return View();
        }

        public void BindDate_Tree(string roleid)
        {
            List<TreeNode> nodes = new List<TreeNode>();
            var menu = from m in db.vw_RoleMenu where m.RoleID == roleid select m;
            var father = from m in menu where m.FatherID == null select m;
            foreach (var m in father)
            {
                TreeNode node = new TreeNode();
                node.Text = m.Name;
                node.NodeID = m.MenuID;
                if (m.Url != null)
                {
                    node.NavigateUrl = Url.Content(m.Url);
                }
                nodes.Add(node);
                BindDate_SubTree(menu, m.MenuID, node);
            }
            ViewBag.treeMenu = nodes.ToArray();

        }
        public void BindDate_SubTree(IQueryable<vw_RoleMenu> menu, string fatherid, TreeNode treeNode)
        {
            var list = from m in menu where m.FatherID == fatherid select m;
            foreach (var m in list)
            {
                TreeNode node = new TreeNode();
                node.Text = m.Name;
                node.NodeID = m.MenuID;
                if (m.Url != null)
                {
                    node.NavigateUrl = Url.Content(m.Url);
                }
                treeNode.Nodes.Add(node);
            }
        }

        public ActionResult Hello()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult btnHello_Click()
        {
            Alert.Show("你好 FineUI！", MessageBoxIcon.Warning);

            return UIHelper.Result();
        }



        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult btnLogin_Click(string tbUserName, string tbPassword)
        {
            var user = from a in db.T_Account where a.ID == tbUserName && a.Psd == tbPassword select a;
            if (user.Any() && user.Count() == 1)
            {
                Session["AccountID"] = tbUserName;
                Session["RoleID"] = user.First().RoleID;
                Session["DeptID"] = user.First().DeptID;
                Session["AccountName"] = user.First().Name;
                ViewBag.AccountName = user.First().Name;
                ShowNotify("成功登录！", MessageBoxIcon.Success);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ShowNotify("用户名或密码错误！", MessageBoxIcon.Error);
                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult Exit()
        {
            Session["AccountID"] = null;
            Session["RoleID"] = null;
            Session["DeptID"] = null;
            Session["AccountName"] = null;
            return RedirectToAction("Login", "Home");
        }

        // GET: Themes
        public ActionResult Themes()
        {
            return View();
        }
    }
}