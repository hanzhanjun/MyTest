using Autofac;
using SqlHelpCore;
using SqlHelpCore.Application.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class AccountController : BaseController
    {

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var administratorservice = Container.Instance.Resolve<IAdministratorService>();
            try
            {
                var admin = administratorservice.Login(username, password);
                if (admin != null)
                {
                    CurrentUser = admin;
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (HelpException ex)
            {
                ViewBag.Content = ex.Message;
            }
            return View();
        }

        public static MemoryStream ExportDataTableToExcel(DataTable sourceTable)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            MemoryStream ms = new MemoryStream();
            int dtRowsCount = sourceTable.Rows.Count;
            int SheetCount = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(dtRowsCount) / 65536));
            int SheetNum = 1;
            int rowIndex = 1;
            int tempIndex = 1; //标示 
            ISheet sheet = workbook.CreateSheet("sheet1" + SheetNum);
            for (int i = 0; i < dtRowsCount; i++)
            {
                if (i == 0 || tempIndex == 1)
                {
                    IRow headerRow = sheet.CreateRow(0);
                    foreach (DataColumn column in sourceTable.Columns)
                        headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                }
                HSSFRow dataRow = (HSSFRow)sheet.CreateRow(tempIndex);
                foreach (DataColumn column in sourceTable.Columns)
                {
                    dataRow.CreateCell(column.Ordinal).SetCellValue(sourceTable.Rows[i][column].ToString());
                }
                if (tempIndex == 65535)
                {
                    SheetNum++;
                    sheet = workbook.CreateSheet("sheet" + SheetNum);//
                    tempIndex = 0;
                }
                rowIndex++;
                tempIndex++;
                //AutoSizeColumns(sheet);
            }
            workbook.Write(ms);
            ms.Flush();
            ms.Position = 0;
            sheet = null;
            // headerRow = null;
            workbook = null;
            return ms;
        }
    }
}