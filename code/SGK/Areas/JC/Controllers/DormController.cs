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
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Formula.Eval;
using SGK.Controllers;

namespace SGK.Areas.JC.Controllers
{
    public class DormController : BaseController
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
            var campus = from c in db.T_Campus where 1 == 1 orderby c.ID select c;
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
            var dormlist = from d in db.vw_Dorm where 1 == 1 orderby d.DormID select d;
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

        #region 下载模板
        public ActionResult DownloadEmptyDormitory()
        {
            DownloadFile("~/Document/download/Dormitory.xlsx", "空白宿舍信息表.xlsx");
            return UIHelper.Result();
        }

        private void DownloadFile(string strFilePath, string strFileName)
        {
            Response.ContentType = "application/x-zip-compressed";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + strFileName);
            string filename = Server.MapPath(strFilePath);
            //指定编码 防止中文文件名乱码  
            Response.HeaderEncoding = System.Text.Encoding.GetEncoding("gb2312");
            Response.TransmitFile(filename);
        }
        #endregion

        #region 导入excel
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetExcel(HttpPostedFileBase fileExcel, FormCollection values)
        {
            if (fileExcel != null)
            {
                string fileName = fileExcel.FileName;

                if (fileName.EndsWith(".xls") || fileName.EndsWith(".xlsx"))
                {
                    fileName = fileName.Replace(":", "_").Replace(" ", "_").Replace("\\", "_").Replace("/", "_");
                    fileName = DateTime.Now.Ticks.ToString() + "_" + fileName;

                    fileExcel.SaveAs(Server.MapPath("~/Areas/JC/Import/" + fileName));
                    string strPath = Server.MapPath("~/Areas/JC/Import/") + fileName;
                    //DataTable dt = FileAction.XlSToDataTable(strPath, "", 0);
                    DataTable dt = FileAction.ExcelToDataTable(strPath, null, true);
                    if (dt.Rows.Count > 0)
                    {
                        foreach(DataRow row in dt.Rows)
                        {
                            string id = "";
                            T_Dorm dorm = null;
                            string campusid = getCampusID(row[0].ToString());
                            string regionid = getRegionID(row[1].ToString());
                            string buildingid = getBuildingID(row[2].ToString());
                            string ceil = String.Format("{0:d2}", Convert.ToInt16(row[3]));
                            string num = String.Format("{0:d4}", Convert.ToInt16(row[4]));
                            if (buildingid != "" && regionid != "" && campusid != "")
                            {
                                id = buildingid + ceil + num;
                                dorm = db.T_Dorm.Find(id);
                                if (dorm != null)
                                {
                                    //已存在
                                }
                                else
                                {
                                    dorm = null;
                                    dorm.ID = id;
                                    dorm.SSLD = buildingid;
                                    dorm.SSLC = Convert.ToInt16(row[3]);
                                    dorm.FJH = row[4].ToString();
                                    dorm.SSLX = Exchange.SSLXToNo(row[5].ToString());
                                    dorm.ZSFY = Convert.ToInt16(row[6].ToString());
                                    dorm.CWS = Convert.ToInt16(row[7].ToString());
                                    dorm.Remark = row[8].ToString();
                                }
                            }
                            else if (regionid != "" && campusid != "")
                            {

                            }
                        }
                    }
                }
                else
                {
                    ShowNotify("文件格式错误，请选择Excel文件");
                }
            }
            return UIHelper.Result();
        }

        private string getBuildingID(string name)
        {
            string id = "";
            var building = from b in db.T_Building where b.Name == name select b;
            if (building.Any())
            {
                id = building.First().ID;
            }
            return id;
        }
        private string getRegionID(string name)
        {
            string id = "";
            var region = from r in db.T_Region where r.Name == name select r;
            if (region.Any())
            {
                id = region.First().ID;
            }
            return id;
        }
        private string getCampusID(string name)
        {
            string id = "";
            var campus = from c in db.T_Campus where c.Name == name select c;
            if (campus.Any())
            {
                id = campus.First().ID;
            }
            return id;
        }

        private int countDorm(string id)
        {
            var dorm = from d in db.T_Dorm where d.SSLD == id select d;
            return dorm.Count();
        }
        private int countBuilding(string id)
        {
            var building = from b in db.T_Building where b.SZYQ == id select b;
            return building.Count();
        }
        private int countRegion(string id)
        {
            var region = from r in db.T_Region where r.SZXQ == id select r;
            return region.Count();
        }
        #endregion


    }
}