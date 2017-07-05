using SGK.Models;
using SGK.Content;
using SGK.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SGK.Controllers;

namespace SGK.Areas.JC.Controllers
{
    public class StudentController : BaseController
    {
        private SGK_lynnEntities db = new SGK_lynnEntities();

        // GET: JC/Student
        public ActionResult Index()
        {
            BindData();
            return View();
        }

        public void BindData()
        {
            var stu = from s in db.vw_Student where 1 == 1 select s;
            DataTable dtSource = new DataTable();
            dtSource = stu.ToDataTable(rec => new object[] { stu });
            DataTable dt = new DataTable();
            dt = dtSource.Clone();
            foreach (DataRow row in dtSource.Rows)
            {
                DataRow r = dt.NewRow();
                r["ID"] = row["ID"];
                r["Name"] = row["Name"];
                r["ClassName"] = row["ClassName"];
                r["Sex"] =Exchange.NoToSex(row["Sex"].ToString());
                r["Tel"] = row["Tel"];
                r["Email"] = row["Email"];
                r["DeptID"] = row["DeptID"];
                r["College"] = row["College"];
                r["Major"] = row["Major"];
                r["Year"] = row["Year"];
                r["StuStatus"] = Exchange.NoToStuStatus(row["StuStatus"].ToString());
                r["Status"] = Exchange.NoToStatus(row["Status"].ToString());
                r["DormID"] = row["DormID"];
                r["Remark"] = row["Remark"];
                r["Campus"] = row["Campus"];
                r["Region"] = row["Region"];
                r["Building"] = row["Building"];
                r["FJH"] = row["Campus"] + "-" + row["Region"] + "-" + row["Building"] + "-" + row["FJH"];
                dt.Rows.Add(r);
            }
            ViewBag.gridStudent = dt;
        }

    }
}