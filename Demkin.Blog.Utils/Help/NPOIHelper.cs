using Demkin.Blog.Utils.ClassExtension;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;

namespace Demkin.Blog.Utils.Help
{
    public class NPOIHelper
    {
        /// <summary>
        /// 读取excel 默认第一行为标头
        /// </summary>
        /// <param name="strFileName">excel文档路径</param>
        /// <returns></returns>
        public static DataTable XlsToDataTable(string strFileName)
        {
            DataTable dt = new DataTable();

            HSSFWorkbook hssfworkbook;
            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                file.Seek(0, SeekOrigin.Begin);
                hssfworkbook = new HSSFWorkbook(file);
            }
            ISheet sheet = hssfworkbook.GetSheetAt(0);
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

            IRow headerRow = sheet.GetRow(0);
            int cellCount = headerRow.LastCellNum;

            for (int j = 0; j < cellCount; j++)
            {
                ICell cell = headerRow.GetCell(j);
                dt.Columns.Add(cell.ToString());
            }

            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                DataRow dataRow = dt.NewRow();

                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    if (row.GetCell(j) != null)
                        dataRow[j] = row.GetCell(j).ToString();
                }

                dt.Rows.Add(dataRow);
            }
            return dt;
        }

        /// <summary>
        /// xls转换List集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static List<T> XlsToList<T>(string filePath) where T : class
        {
            DataTable dt = XlsToDataTable(filePath);
            if (dt == null)
                return null;

            // 获得T的类型
            List<T> tlist = Activator.CreateInstance<List<T>>();
            var dType = typeof(T);
            foreach (DataRow dr in dt.Rows)
            {
                T t = Activator.CreateInstance<T>();
                foreach (PropertyInfo pInfo in dType.GetProperties())
                {
                    string columnName = pInfo.Name;
                    //Xls中是否包含此列名
                    if (dt.Columns.Contains(columnName))
                    {
                        var val = dr[columnName];

                        if (pInfo.PropertyType.FullName.Contains("String"))
                        {
                            pInfo.SetValue(t, val.ObjToString());
                        }
                        else if (pInfo.PropertyType.FullName.Contains("Int32") || pInfo.PropertyType.FullName.Contains("Enum"))
                        {
                            pInfo.SetValue(t, val.ObjToInt());
                        }
                        else if (pInfo.PropertyType.FullName.Contains("Int64"))
                        {
                            pInfo.SetValue(t, val.ObjToLong());
                        }
                        else if (pInfo.PropertyType.FullName.Contains("DateTime"))
                        {
                            pInfo.SetValue(t, val.ObjToDate());
                        }
                        else if (pInfo.PropertyType.FullName.Contains("Boolean"))
                        {
                            pInfo.SetValue(t, val.ObjToBool());
                        }
                        else
                        {
                            pInfo.SetValue(t, val);
                        }
                    }
                }
                tlist.Add(t);
            }
            return tlist;
        }
    }
}