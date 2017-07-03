using FineUIMvc;
using Newtonsoft.Json.Linq;
using SGK.Controllers;
using SGK.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SGK.Areas.JC.Controllers
{
    public class DeptController : BaseController
    {
        private SGK_lynnEntities db = new SGK_lynnEntities();

        #region list
        // GET: JC/Dept
        public ActionResult Index()
        {
            var dept = from d in db.T_Dept where 1 == 1 select d;
            ViewBag.gridDept = dept.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult btnDelete_Click(JArray selectedRows, JArray fields)
        {
            foreach (string id in selectedRows)
            {
                T_Dept t_dept = db.T_Dept.Find(id);
                db.T_Dept.Remove(t_dept);
            }
            db.SaveChanges();
            Alert.Show("删除成功！");
            var dept = from d in db.T_Dept where 1 == 1 select d;
            ViewBag.gridDept = dept.ToList();
            UIHelper.Grid("gridDeptList").DataSource(dept.ToList(), fields);
            return UIHelper.Result();
        }
        #endregion

        #region add
        public ActionResult Add()
        {
            return View();
        }

        public ActionResult btnSubmit_Click(FormCollection formInfo)
        {
            string deptid = formInfo["tbDeptID"].ToString();
            string name = formInfo["tbDeptName"].ToString();

            var exist = from d in db.T_Dept where d.ID == deptid select d;
            if (exist.Any())
            {
                alertInfo("错误提示", "该单位编号已存在，请重新输入编号", "Error");
            }
            else
            {
                exist = from d in db.T_Dept where d.ID == deptid && d.Name == name select d;
                if (exist.Any())
                {
                    alertInfo("警告", "该单位已存在，请重新添加其它单位", "Warning");
                }
                else
                {
                    T_Dept dept = new T_Dept();
                    dept.ID = deptid;
                    dept.Name = name;
                    db.T_Dept.Add(dept);
                    int result;
                    try
                    {
                        result = db.SaveChanges();
                    }
                    catch (DbEntityValidationException dbEx)
                    {
                        result = 0;
                    }
                    if (result == 1)
                    {
                        string script = String.Format("alert('新增成功！');");
                        PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference() +
                            script);
                    }
                    else
                    {
                        alertInfo("提交失败", "数据库提交失败，请重新尝试!", "Error");
                    }
                }
            }
            return UIHelper.Result();
        }

        #endregion

        #region modify
        public ActionResult Modify()
        {
            string id = Request.QueryString["id"].ToString();
            T_Dept dept = (from d in db.T_Dept where d.ID == id select d).First();
            ViewBag.tbDeptID = dept.ID.ToString();
            ViewBag.tbDeptName = dept.Name.ToString();
            return View();
        }

        #endregion
    }
}