using FineUIMvc;
using SGK.Common;
using SGK.Content;
using SGK.Controllers;
using SGK.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SGK.Areas.TB.Controllers
{
    public class JsonDataModel
    {
        public string id { get; set; }
        public string text { get; set; }
    }

    public class ReportController : Controller
    {
        private SGK_lynnEntities db = new SGK_lynnEntities();

        // GET: TB/Report
        public ActionResult Index()
        {
            BindData_Report();
            BindData_SbuReport("-1");
            return View();
        }

        public void BindData_Report()
        {
            var report = from r in db.T_Report where 1 == 1 select r;
            DataTable dtSource = new DataTable();
            dtSource = report.ToDataTable(rec => new object[] { report });

            DataTable dt = new DataTable();
            dt = dtSource.Clone();
            foreach (DataRow row in dtSource.Rows)
            {
                DataRow r = dt.NewRow();
                r["ID"] = row["ID"];
                r["AccountID"] = row["AccountID"];
                r["Type"] = Exchange.NoToReportType(row["Type"].ToString());
                r["Time"] = row["Time"];
                r["Num"] = row["Num"];
                r["Reason"] = row["Reason"];
                r["AuditID"] = row["AuditID"];
                r["RejectReason"] = row["RejectReason"];
                r["Status"] = Exchange.NoToAuditStatus(row["Status"].ToString());
                r["Remark"] = row["Remark"];
                r["Name"] = row["Name"];
                r["AuditName"] = row["AuditName"];
                dt.Rows.Add(r);
            }
            ViewBag.gridReport = dt;
        }
        public void BindData_SbuReport(string reportid)
        {
            var subreport = from s in db.vw_SubReport where s.ReportID == reportid select s;
            DataTable dtSource = new DataTable();
            dtSource = subreport.ToDataTable(rec => new object[] { subreport });

            DataTable dt = new DataTable();
            dt = dtSource.Clone();
            foreach (DataRow row in dtSource.Rows)
            {
                DataRow r = dt.NewRow();
                r["ID"] = row["ID"];
                r["ReportID"] = row["ReportID"];
                r["StudentID"] = row["StudentID"];
                r["OutDormID"] = row["OutDormID"];
                r["InDormID"] = row["InDormID"];
                r["PriceChanged"] = Exchange.NoToPriceChanged(row["PriceChanged"].ToString());
                r["StuName"] = row["StuName"];
                r["OutCampus"] = row["OutCampus"];
                r["OutRegion"] = row["OutRegion"];
                r["OutBuilding"] = row["OutBuilding"];
                r["OutFJH"] = row["OutCampus"] + "-" + row["OutRegion"] + "-" + row["OutBuilding"] + "-" + row["OutFJH"];
                r["InCampus"] = row["InCampus"];
                r["InRegion"] = row["InRegion"];
                r["InBuilding"] = row["InBuilding"];
                r["InFJH"] = row["InCampus"] + "-" + row["InRegion"] + "-" + row["InBuilding"] + "-" + row["InFJH"];
                dt.Rows.Add(r);
            }
            ViewBag.gridSubReport = dt;
        }

        public ActionResult Add()
        {
            ViewBag.girdSubReport = null;
            return View();
        }

        public ActionResult btnSubmit_Click(FormCollection formInfo)
        {
            return UIHelper.Result();
        }

        public ActionResult Modify()
        {
            return View();
        }

        public ActionResult SelectStudent()
        {
            var stu = from s in db.T_Student where 1 == 1 select s;
            ViewBag.gridStudent = stu.ToList();
            return View();
        }

        public ActionResult SelectDorm()
        {
            return View();
        }

        public ActionResult postBack()
        {
            //获取Json 数据
            //var sr = new StreamReader(Request.InputStream);
            //var stream = sr.ReadToEnd();
            //JavaScriptSerializer js = new JavaScriptSerializer();
            //var list = js.Deserialize<List<JsonDataModel>>(stream);
            return UIHelper.Result();
        }
    }
}