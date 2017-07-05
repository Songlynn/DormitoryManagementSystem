using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGK.Common
{
    public class Exchange
    {
        /// <summary>
        /// 转换为宿舍类型
        /// </summary>
        /// <param name="no"></param>
        /// <returns></returns>
        public static string NoToSSLX(string no)
        {
            string type = "";
            switch (no)
            {
                case "0":
                    type = "学生宿舍";
                    break;
                case "1":
                    type = "非住宿用房";
                    break;
                default:
                    break;
            }
            return type;
        }

        public static string SSLXToNo(string name)
        {
            string type = "";
            switch (name)
            {
                case "学生宿舍":
                    type = "0";
                    break;
                case "非住宿用房":
                    type = "1";
                    break;
                default:
                    break;
            }
            return type;
        }

        /// <summary>
        /// 转换为性别
        /// </summary>
        /// <param name="no"></param>
        /// <returns></returns>
        public static string NoToSex(string no)
        {
            string type = "";
            switch (no)
            {
                case "0":
                    type = "男";
                    break;
                case "1":
                    type = "女";
                    break;
                default:
                    break;
            }
            return type;
        }

        /// <summary>
        /// 转换为学生状态
        /// </summary>
        /// <param name="no"></param>
        /// <returns></returns>
        public static string NoToStuStatus(string no)
        {
            string type = "";
            switch (no)
            {
                case "0":
                    type = "在读";
                    break;
                case "1":
                    type = "休学";
                    break;
                case "2":
                    type = "退学";
                    break;
                case "3":
                    type = "转学";
                    break;
                case "4":
                    type = "延期毕业";
                    break;
                case "5":
                    type = "毕业";
                    break;
                case "6":
                    type = "其它";
                    break;
                default:
                    break;
            }
            return type;
        }

        /// <summary>
        /// 转换为住宿状态
        /// </summary>
        /// <param name="no"></param>
        /// <returns></returns>
        public static string NoToStatus(string no)
        {
            string type = "";
            switch (no)
            {
                case "0":
                    type = "非入住";
                    break;
                case "1":
                    type = "入住";
                    break;
                case "2":
                    type = "入住（走读）";
                    break;
                default:
                    break;
            }
            return type;
        }

        /// <summary>
        /// 转换为申请类型
        /// </summary>
        /// <param name="no"></param>
        /// <returns></returns>
        public static string NoToReportType(string no)
        {
            string type = "";
            switch (no)
            {
                case "0":
                    type = "宿舍调整";
                    break;
                case "1":
                    type = "宿舍交换";
                    break;
                case "2":
                    type = "入住";
                    break;
                case "3":
                    type = "退宿";
                    break;
                default:
                    break;
            }
            return type;
        }

        /// <summary>
        /// 转换为审核状态
        /// </summary>
        /// <param name="no"></param>
        /// <returns></returns>
        public static string NoToAuditStatus(string no)
        {
            string type = "";
            switch (no)
            {
                case "0":
                    type = "待审核";
                    break;
                case "1":
                    type = "待归档";
                    break;
                case "2":
                    type = "已归档";
                    break;
                case "3":
                    type = "驳回";
                    break;
                default:
                    break;
            }
            return type;
        }

        /// <summary>
        /// 转换为住宿费用变动
        /// </summary>
        /// <param name="no"></param>
        /// <returns></returns>
        public static string NoToPriceChanged(string no)
        {
            string type = "";
            switch (no)
            {
                case "0":
                    type = "无变动";
                    break;
                case "1":
                    type = "有变动";
                    break;
                default:
                    break;
            }
            return type;
        }
    }
}