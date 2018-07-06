using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace WebApplication1.Nopi
{
    public class ExcelNpoi
    {
        /// <summary>
        /// NOPI导出execl
        /// </summary>
        /// <param name="cellHeard"></param>
        /// <param name="enList"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public void NOPIEntityListToExcel(Dictionary<string, string> cellHeard, IList enList, string sheetName, string title)
        {         
            MemoryStream ms = new System.IO.MemoryStream();
            try
            {
                    // 2.解析单元格头部，设置单元头的中文名称
                    HSSFWorkbook workbook = new HSSFWorkbook(); // 工作簿
                    ISheet sheet = workbook.CreateSheet(sheetName); // 工作表
                    List<string> keys = cellHeard.Keys.ToList();


                    IRow row0 = sheet.CreateRow(0);
                    row0.CreateCell(0).SetCellValue(title);//设置标题
                    row0.GetCell(0).CellStyle = Getcellstyle(workbook, "大标题");
                    SetCellRangeAddress(sheet, 0, 1, 0, keys.Count - 1);

                    IRow row = sheet.CreateRow(2);
                    for (int i = 0; i < keys.Count; i++)
                    {
                        row.CreateCell(i).SetCellValue(cellHeard[keys[i]]); // 列名为Key的值
                        row.GetCell(i).CellStyle = Getcellstyle(workbook, "子项边框");
                    }

                    // 3.List对象的值赋值到Excel的单元格里
                    int rowIndex = 3;
                    foreach (var en in enList)
                    {
                        IRow rowTmp = sheet.CreateRow(rowIndex);
                        for (int i = 0; i < keys.Count; i++)
                        {
                            string cellValue = ""; // 单元格的值
                            object properotyValue = null; // 属性的值
                            System.Reflection.PropertyInfo properotyInfo = null; // 属性的信息                      
                                                                                 //根据属性名称获取对象对应的属性
                            properotyInfo = en.GetType().GetProperty(keys[i]);
                            if (properotyInfo != null)
                            {
                                properotyValue = properotyInfo.GetValue(en, null);
                            }
                            //}

                            // 3.3 属性值经过转换赋值给单元格值
                            if (properotyValue != null)
                            {
                                cellValue = properotyValue.ToString();
                                // 3.3.1 对时间初始值赋值为空
                                if (cellValue.Trim() == "0001/1/1 0:00:00" || cellValue.Trim() == "0001/1/1 23:59:59" || cellValue.Contains("00:00:00"))
                                {
                                    cellValue = cellValue.Substring(0, cellValue.Length - 8);
                                }
                            }

                            if (cellValue == "true" || cellValue == "True")
                            {
                                cellValue = "是";
                            }
                            else if (cellValue == "false" || cellValue == "False")
                            {
                                cellValue = "否";
                            }
                            // 3.4 填充到Excel的单元格里
                            rowTmp.CreateCell(i).SetCellValue(cellValue);
                            rowTmp.GetCell(i).CellStyle = Getcellstyle(workbook, "子项边框");
                        }
                        rowIndex++;
                    }
                string excelName = DateTime.Now.ToString("yyyyMMddhhmmss") + ".xls";//获取当前时间
                string userAgent = HttpContext.Current.Request.ServerVariables["http_user_agent"].ToLower();   
                // 写入到客户端        
                workbook.Write(ms);
                ms.Seek(0, SeekOrigin.Begin);
                HttpResponse response = System.Web.HttpContext.Current.Response;
                response.Clear();
                response.Charset = "UTF-8";
                response.ContentType = "application/vnd-excel";
                //判断浏览器版本
                if (userAgent.IndexOf("firefox") == -1)
                {
                    //非火狐浏览器                
                    excelName = System.Web.HttpUtility.UrlEncode(excelName, System.Text.Encoding.UTF8);
                }
                System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment; filename=" + excelName));
                System.Web.HttpContext.Current.Response.BinaryWrite(ms.ToArray());
                if (sheet != null)
                    sheet = null;

            }
                catch (Exception ex)
                {
                    throw(ex);
                }
            finally
            {
                ms.Close();
            }
        }

        public ICellStyle Getcellstyle(HSSFWorkbook workbook, string str)
        {
            ICellStyle style = workbook.CreateCellStyle();
            switch (str)
            {
                case "大标题":
                    style.Alignment = HorizontalAlignment.Center;
                    style.WrapText = true;
                    IFont font = workbook.CreateFont();
                    font.FontHeightInPoints = 16;
                    font.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;
                    font.FontName = "宋体";
                    font.Color = HSSFColor.Black.Index;
                    style.SetFont(font);
                    break;
                case "副标题":
                    style.Alignment = HorizontalAlignment.Center;
                    style.WrapText = true;
                    IFont font1 = workbook.CreateFont();
                    font1.FontHeightInPoints = 12;
                    font1.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;
                    font1.FontName = "宋体";
                    font1.Color = HSSFColor.Black.Index;
                    style.SetFont(font1);
                    break;
                case "内容页":
                    style.Alignment = HorizontalAlignment.Center;
                    style.WrapText = true;
                    IFont font2 = workbook.CreateFont();
                    font2.FontHeightInPoints = 10;
                    font2.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;
                    font2.FontName = "宋体";
                    font2.Color = HSSFColor.Black.Index;
                    style.SetFont(font2);
                    break;
                case "子项边框":
                    style.Alignment = HorizontalAlignment.Center;
                    style.VerticalAlignment = VerticalAlignment.Center;
                    style.WrapText = true;
                    IFont font3 = workbook.CreateFont();
                    font3.FontHeightInPoints = 10;
                    font3.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;
                    font3.FontName = "宋体";
                    font3.Color = HSSFColor.Black.Index;
                    style.SetFont(font3);

                    style.BorderBottom = BorderStyle.Medium;
                    style.BorderLeft = BorderStyle.Medium;
                    style.BorderRight = BorderStyle.Medium;
                    style.BorderTop = BorderStyle.Medium;
                    break;
                default:
                    break;
            }
            return style;
        }

        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="sheet">工作表</param>
        /// <param name="rowstart">行开始</param>
        /// <param name="rowend">行结束</param>
        /// <param name="colstart">列开始</param>
        /// <param name="colend">列结束</param>
        public void SetCellRangeAddress(ISheet sheet, int rowstart, int rowend, int colstart, int colend)
        {
            CellRangeAddress cellRangeAddress = new CellRangeAddress(rowstart, rowend, colstart, colend);
            sheet.AddMergedRegion(cellRangeAddress);
        }


        private void DownloadTheFile(string fileFullPath)
        {
            try
            {
                if (!File.Exists(fileFullPath))
                {                 
                    return;
                }
                string newFileName = DateTime.Now.ToString("yyyyMMddHHmmss") ;
                /* 判断浏览器，对于Firefox浏览器不进行编码*/
                string userAgent = HttpContext.Current.Request.ServerVariables["http_user_agent"].ToLower();
                if (userAgent.IndexOf("firefox") == -1)
                    newFileName = HttpUtility.UrlEncode(newFileName, Encoding.UTF8);
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.ContentEncoding = Encoding.UTF8;
                HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + newFileName + ".xls");
                HttpContext.Current.Response.WriteFile(fileFullPath);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
                HttpContext.Current.Response.Close();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}