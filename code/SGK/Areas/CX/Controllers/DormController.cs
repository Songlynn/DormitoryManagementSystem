using FineUIMvc;
using NPOI.HSSF.UserModel;
using NPOI.SS.Util;
using SGK.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SGK.Areas.CX.Controllers
{
    public class DormController : Controller
    {
        private SGK_lynnEntities db = new SGK_lynnEntities();

        // GET: CX/Dorm
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult getDormInfo()
        {
            string campus = "";
            string region = "";

            return getExcel();
        }

        private ActionResult getExcel()
        {
            HSSFWorkbook newExcel = new HSSFWorkbook();
            HSSFSheet sheet = (HSSFSheet)newExcel.CreateSheet();
            int index = 0;
            #region  设置标题
            HSSFRow row_title = (HSSFRow)sheet.CreateRow(index++);
            HSSFCell cell_title = (HSSFCell)row_title.CreateCell(0);
            cell_title.SetCellValue("园区住宿情况");
            sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, 6));   //合并单元格(起始行，结束行，起始列，结束列)
            #endregion

            List<T_Building> building = (from b in db.T_Building where b.SZYQ == "0104" select b).ToList();
            foreach (T_Building b in building)
            {
                #region  设置列名
                HSSFRow row_name = (HSSFRow)sheet.CreateRow(index++);

                HSSFCell cell_name_1 = (HSSFCell)row_name.CreateCell(0);
                cell_name_1.SetCellValue("楼栋");

                HSSFCell cell_name_2 = (HSSFCell)row_name.CreateCell(1);
                cell_name_2.SetCellValue("层数");

                HSSFCell cell_name_3 = (HSSFCell)row_name.CreateCell(2);
                cell_name_3.SetCellValue("房间总数");

                HSSFCell cell_name_4 = (HSSFCell)row_name.CreateCell(3);
                cell_name_4.SetCellValue("可住房间数");

                HSSFCell cell_name_5 = (HSSFCell)row_name.CreateCell(4);
                cell_name_5.SetCellValue("可住床位数");

                HSSFCell cell_name_6 = (HSSFCell)row_name.CreateCell(5);
                cell_name_6.SetCellValue("占用床位数");

                HSSFCell cell_name_7 = (HSSFCell)row_name.CreateCell(6);
                cell_name_7.SetCellValue("剩余床位数");

                int Amount = 0, Able = 0, Total = 0, Used = 0, Last = 0;
                #endregion

                #region 设置数据
                var ceil = (from d in db.T_Dorm where d.SSLD == b.ID select d.SSLC).Distinct();
                foreach(var c in ceil)
                {
                    int num = Convert.ToInt16(c);
                    var dorm = from d in db.T_Dorm where d.SSLD == b.ID && d.SSLC == num select d;
                    int dorm_amount = dorm.Count();
                    var able_dorm = from d in dorm where d.SSLX == "0" select d;
                    int able_amount = able_dorm.Count();
                    int total = 0;
                    int last = 0;
                    foreach(var d in able_dorm)
                    {
                        total = total + (int)d.CWS;
                        last = last + (int)d.KYCWS;
                    }
                    int used = total - last;
                    HSSFRow row = (HSSFRow)sheet.CreateRow(index++);//写入行  
                    row.CreateCell(0).SetCellValue(b.Name);
                    row.CreateCell(1).SetCellValue(num);
                    row.CreateCell(2).SetCellValue(dorm_amount);
                    row.CreateCell(3).SetCellValue(able_amount);
                    row.CreateCell(4).SetCellValue(total);
                    row.CreateCell(5).SetCellValue(used);
                    row.CreateCell(6).SetCellValue(last);

                    Amount += dorm_amount;
                    Able += able_amount;
                    Total += total;
                    Used += used;
                    Last += last;
                }
                HSSFRow row_total = (HSSFRow)sheet.CreateRow(index++);
                row_total.CreateCell(0).SetCellValue("");
                row_total.CreateCell(1).SetCellValue("合计");
                row_total.CreateCell(2).SetCellValue(Amount);
                row_total.CreateCell(3).SetCellValue(Able);
                row_total.CreateCell(4).SetCellValue(Total);
                row_total.CreateCell(5).SetCellValue(Used);
                row_total.CreateCell(6).SetCellValue(Last);

                #endregion
            }
            // 写入到客户端
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            newExcel.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return File(ms, "application/vnd.ms-excel", "园区住宿情况.xls");


        }

        


    }
}