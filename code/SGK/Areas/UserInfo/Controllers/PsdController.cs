using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FineUIMvc;
using SGK.Controllers;
using SGK.Models;

namespace SGK.Areas.UserInfo.Controllers
{
    public class PsdController : BaseController
    {
        private SGK_lynnEntities db = new SGK_lynnEntities();

        // GET: UserInfo/Psd
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult btnSubmit_Click(string tbPsdOld,string tbPsdNew,string tbPsdNew2)
        {
            string id = Session["AccountID"].ToString();
            var user = from a in db.T_Account where a.ID == id select a;
            if (tbPsdOld.Equals(user.First().Psd))
            {
                if (tbPsdNew.Equals(tbPsdNew2))
                {
                    T_Account a = db.T_Account.Find(id);
                    a.Psd = tbPsdNew;
                    db.SaveChanges();
                    ShowNotify("修改成功！");
                    UIHelper.TextBox("tbPsdOld").Text("");
                    UIHelper.TextBox("tbPsdNew").Text("");
                    UIHelper.TextBox("tbPsdNew2").Text("");
                }
                else
                {
                    ShowNotify("第二次输入的新密码与第一次不一致，请重新输入");
                    UIHelper.TextBox("tbPsdNew").Text("");
                    UIHelper.TextBox("tbPsdNew2").Text("");
                }
            }
            else
            {
                ShowNotify("旧密码输入有误，请重新输入");
                UIHelper.TextBox("tbPsdOld").Text("");
                UIHelper.TextBox("tbPsdNew").Text("");
                UIHelper.TextBox("tbPsdNew2").Text("");
            }
            return UIHelper.Result();
        }

    }
}