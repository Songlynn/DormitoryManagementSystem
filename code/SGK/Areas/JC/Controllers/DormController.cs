using FineUIMvc;
using SGK.Content;
using SGK.Models;
using SGK.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SGK.Areas.JC.Controllers
{
    public class DormController : Controller
    {
        private SGK_lynnEntities db = new SGK_lynnEntities();

        // GET: JC/Dorm
        public ActionResult Index()
        {
            BindData_Tree();
            BindData_Grid();
            return View();
        }
        #region 绑定树
        public void BindData_Tree()
        {
            List<TreeNode> nodes = new List<TreeNode>();
            var campus = from c in db.T_Campus where 1 == 1 select c;
            foreach (var c in campus)
            {
                TreeNode node = new TreeNode();
                node.Text = c.Name;
                node.NodeID = c.ID;
                nodes.Add(node);
                SubRegion(c.ID, node);
            }
            ViewBag.treeDorm = nodes.ToArray();
        }

        private void SubRegion(string campusId,TreeNode treeNode)
        {
            var region = from r in db.T_Region where r.SZXQ == campusId orderby r.ID select r;
            if (region.Any())
            {
                treeNode.Expanded = true;
                foreach(var r in region)
                {
                    TreeNode node = new TreeNode();
                    node.Text = r.Name;
                    node.NodeID = r.ID;
                    treeNode.Nodes.Add(node);
                    SubBuilding(r.ID, node);
                }
            }
        }

        private void SubBuilding(string regionId,TreeNode treeNode)
        {
            var building = from b in db.T_Building where b.SZYQ == regionId orderby b.ID select b;
            if (building.Any())
            {
                treeNode.Expanded = true;
                foreach (var b in building)
                {
                    TreeNode node = new TreeNode();
                    node.Text = b.Name;
                    node.NodeID = b.ID;
                    treeNode.Nodes.Add(node);
                }
            }
        }
        #endregion

        #region 绑定表格
        public void BindData_Grid()
        {
            var dormlist = from d in db.vw_Dorm where 1 == 1 select d;
            DataTable dtSource = new DataTable();
            dtSource = dormlist.ToDataTable(rec => new object[] { dormlist });

            DataTable dt = new DataTable();
            dt = dtSource.Clone();
            foreach(DataRow row in dtSource.Rows)
            {
                DataRow r = dt.NewRow();
                r["DormID"]= row["DormID"];
                r["BuildingID"]= row["BuildingID"];
                r["SSLC"]= row["SSLC"];
                r["FJH"] = row["Building"] + "-" + row["FJH"];
                r["SSLX"] = Exchange.NoToSSLX(row["SSLX"].ToString());
                r["ZSFY"]= row["ZSFY"];
                r["CWS"]= row["CWS"];
                r["KYCWS"]= row["KYCWS"];
                r["Sex"] = Exchange.NoToSex(row["Sex"].ToString());
                r["DeptID"]= row["DeptID"];
                r["Remark"]= row["Remark"];
                r["Bed_01"]= row["Bed_01"];
                r["Bed_02"]= row["Bed_02"];
                r["Bed_03"]= row["Bed_03"];
                r["Bed_04"]= row["Bed_04"];
                r["Bed_05"]= row["Bed_05"];
                r["Bed_06"]= row["Bed_06"];
                r["Bed_07"]= row["Bed_07"];
                r["Bed_08"]= row["Bed_08"];
                r["Bed_09"]= row["Bed_09"];
                r["Bed_10"]= row["Bed_10"];
                r["Building"]= row["Building"];
                r["RegionID"]= row["RegionID"];
                r["Region"] = row["Campus"] + "-" + row["Region"];
                r["CampusID"]= row["CampusID"];
                r["Campus"]= row["Campus"];
                r["Stu_01"]= row["Stu_01"];
                r["Stu_02"]= row["Stu_02"];
                r["Stu_03"]= row["Stu_03"];
                r["Stu_04"]= row["Stu_04"];
                r["Stu_05"]= row["Stu_05"];
                r["Stu_06"]= row["Stu_06"];
                r["Stu_07"]= row["Stu_07"];
                r["Stu_08"]= row["Stu_08"];
                r["Stu_09"]= row["Stu_09"];
                r["Stu_10"]= row["Stu_10"];


                dt.Rows.Add(r);
            }
            ViewBag.gridDorm = dt;


        }


        #endregion
    }
}