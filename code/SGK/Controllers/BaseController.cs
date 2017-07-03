using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FineUIMvc;

namespace SGK.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// 显示通知对话框
        /// </summary>
        /// <param name="message"></param>
        public virtual void ShowNotify(string message)
        {
            ShowNotify(message, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 显示通知对话框
        /// </summary>
        /// <param name="message"></param>
        /// <param name="messageIcon"></param>
        public virtual void ShowNotify(string message, MessageBoxIcon messageIcon)
        {
            ShowNotify(message, messageIcon, Target.Top);
        }

        /// <summary>
        /// 显示通知对话框
        /// </summary>
        /// <param name="message"></param>
        /// <param name="messageIcon"></param>
        /// <param name="target"></param>
        public virtual void ShowNotify(string message, MessageBoxIcon messageIcon, Target target)
        {
            Notify n = new Notify();
            n.Target = target;
            n.Message = message;
            n.MessageBoxIcon = messageIcon;
            n.PositionX = Position.Center;
            n.PositionY = Position.Top;
            n.DisplayMilliseconds = 3000;
            n.ShowHeader = false;

            n.Show();
        }

        #region 通知方法

        /// <summary>
        /// Alert.MessageBoxIcon可设置提示框图标样式,可选样式：None无 Information消息 Warning警告 Question问题 Error错误 Success成功,Alert.Target可设置显示提示框的位置,可选样式：Self当前页面 Parent父页面 Top顶层页面
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">信息</param>
        /// <param name="icon">Icon类型</param>
        public void alertInfo(string title, string message, string icon)
        {
            Alert alert = new Alert();
            alert.Title = title;
            alert.Message = message;
            alert.MessageBoxIcon = (MessageBoxIcon)Enum.Parse(typeof(MessageBoxIcon), icon, true);
            alert.Target = (Target)Enum.Parse(typeof(Target), "Self", true);
            alert.Show();
        }

        #endregion
    }
}