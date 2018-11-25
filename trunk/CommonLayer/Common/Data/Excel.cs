using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.IO;
using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;  


namespace Common.Data
{
    public class Excel
    {
        public DataTable ToDataTable(string strExcelFileName, string strSheetName)
        {
            try
            {
                string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + strExcelFileName + ";" + "Extended Properties=Excel 5.0;";
                string strExcel = string.Format("select * from [{0}$]", strSheetName);
                DataSet ds = new DataSet();
                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();
                    OleDbDataAdapter adapter = new OleDbDataAdapter(strExcel, strConn);
                    adapter.Fill(ds, strSheetName);
                    conn.Close();
                }
                return ds.Tables[strSheetName];
            }
            catch { }
            return new DataTable();
        }

        /// <summary>读取excel     
        /// 默认第一行为表头，导入第一个工作表  
        /// </summary>     
        /// <param name="strFileName">excel文档路径</param>     
        /// <returns></returns>     
        public DataTable Import(string strFileName)
        {
            DataTable dt = new DataTable();
            string value = "";
            XSSFWorkbook hssfworkbook;
            try
            {
                using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
                {
                    hssfworkbook = new XSSFWorkbook(file);
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
                        {
                            if (string.IsNullOrEmpty(row.GetCell(j).ToString()))
                            {
                                dataRow[j] = value;
                            }
                            else
                            {
                                dataRow[j] = row.GetCell(j).ToString();
                            }

                        }
                    }
                    dt.Rows.Add(dataRow);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }


        /// <summary>
        /// 根据模板生成Excel
        /// </summary>
        /// <param name="OldFileName"></param>
        /// <param name="NewFileName"></param>
        /// <param name="col"></param>
        /// <param name="scol"></param>
        /// <param name="dt"></param>
        public string GenerationExcel(string OldFileName, string NewFileName, int[] col, string[] scol, DataTable dt)
        {
            XSSFWorkbook workbook;
            using (FileStream file = new FileStream(OldFileName, FileMode.Open, FileAccess.Read))
            {
                workbook = new XSSFWorkbook(file);
            }
            ISheet sheet = workbook.GetSheetAt(0);

            IRow row;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                row = sheet.CreateRow(i + 1);
                for (int j = 0; j < col.Length; j++)
                {
                    row.CreateCell(col[j], CellType.STRING).SetCellValue(dt.Rows[i][scol[j]].ToString());
                }
            }

            using (FileStream stream = new FileStream(NewFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                workbook.Write(stream);
            }
            return NewFileName;
        }

        /// <summary>
        /// 导出EXCEL(文件存到服务器)
        /// </summary>
        /// <param name="OldFileName"></param>
        /// <param name="NewFileName"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public string GenerationExcel(string OldFileName, string NewFileName, DataTable dt)
        {
            try
            {
                XSSFWorkbook workbook;
                using (FileStream file = new FileStream(OldFileName, FileMode.Open, FileAccess.Read))
                {
                    workbook = new XSSFWorkbook(file);
                }
                ISheet sheet = workbook.GetSheetAt(0);

                IRow row;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    row = sheet.CreateRow(i + 1);
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        row.CreateCell(j, CellType.STRING).SetCellValue(dt.Rows[i][j].ToString());
                    }
                }

                using (FileStream stream = new FileStream(NewFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    workbook.Write(stream);
                }
                return NewFileName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


            /// <summary>
        /// 导出EXCEL(文件存到服务器)(不需要模板)
            /// </summary>
            /// <param name="NewFileName"></param>
            /// <param name="dt"></param>
            /// <returns></returns>
        public string GenerationExcelByServer(string NewFileName, DataTable dt)
        {
            try
            {
                XSSFWorkbook workbook = new XSSFWorkbook();
                ISheet sheet = workbook.CreateSheet();

                IRow row;
                row = sheet.CreateRow(0);
                for (int a = 0; a < dt.Columns.Count; a++)
                {
                    row.CreateCell(a, CellType.STRING).SetCellValue(dt.Columns[a].ColumnName);
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    row = sheet.CreateRow(i + 1);
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        row.CreateCell(j, CellType.STRING).SetCellValue(dt.Rows[i][j].ToString());
                    }
                }
                using (FileStream stream = new FileStream(NewFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    workbook.Write(stream);
                }
                return NewFileName;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 导出EXCEL(文件存到内存中)
        /// </summary>
        /// <param name="OldFileName"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public Stream GenerationExcel(string OldFileName, DataTable dt)
        {
            MemoryStream ms = new MemoryStream();
            XSSFWorkbook workbook;
            using (FileStream file = new FileStream(OldFileName, FileMode.Open, FileAccess.Read))
            {
                workbook = new XSSFWorkbook(file);
            }
            ISheet sheet = workbook.GetSheetAt(0);

            IRow row;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                row = sheet.CreateRow(i + 1);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    row.CreateCell(j, CellType.STRING).SetCellValue(dt.Rows[i][j].ToString());
                }
            }

            workbook.Write(ms);
            ms.Flush();
            return ms;
        }

        /// <summary>
        /// 导出EXCEL(文件存到内存中)(可以选择相应的列绑定)
        /// </summary>
        /// <param name="OldFileName">模板路径</param>
        /// <param name="col">EXCEL中列的文职</param>
        /// <param name="scol">DT中的列名</param>
        /// <param name="dt">数据DT</param>
        public Stream GenerationExcel(string OldFileName, int[] col, string[] scol, DataTable dt)
        {
            MemoryStream ms = new MemoryStream();
            XSSFWorkbook workbook;
            using (FileStream file = new FileStream(OldFileName, FileMode.Open, FileAccess.Read))
            {
                workbook = new XSSFWorkbook(file);
            }
            ISheet sheet = workbook.GetSheetAt(0);

            IRow row;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                row = sheet.CreateRow(i + 1);
                for (int j = 0; j < col.Length; j++)
                {
                    row.CreateCell(col[j], CellType.STRING).SetCellValue(dt.Rows[i][scol[j]].ToString());
                }
            }

            workbook.Write(ms);
            ms.Flush();
            return ms;
        }


        /// <summary>
        /// 导出EXCEL(文件存到内存中)(可以选择相应的列绑定)
        /// </summary>
        /// <param name="OldFileName">模板路径</param>
        /// <param name="col">EXCEL中列的文职</param>
        /// <param name="scol">DT中的列名</param>
        /// <param name="dt">数据DT</param>
        public Stream GenerationExcel(string OldFileName,Dictionary<string, string> obj, DataTable dt)
        {
            MemoryStream ms = new MemoryStream();
            XSSFWorkbook workbook;
            using (FileStream file = new FileStream(OldFileName, FileMode.Open, FileAccess.Read))
            {
                workbook = new XSSFWorkbook(file);
            }
            ISheet sheet = workbook.GetSheetAt(0);

            IRow row;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                row = sheet.CreateRow(i + 1);
                foreach (var item in obj)
                {
                    row.CreateCell(Convert.ToInt32(item.Key), CellType.STRING).SetCellValue(dt.Rows[i][item.Value].ToString());
                }
            }

            workbook.Write(ms);
            ms.Flush();
            return ms;
        }



        /// <summary>
        /// 导出EXCEL(文件存到内存中)(不需要模板)
        /// </summary>
        /// <param name="OldFileName"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public Stream GenerationExcel(DataTable dt)
        {
            MemoryStream ms = new MemoryStream();
            XSSFWorkbook workbook = new XSSFWorkbook();
            ISheet sheet = workbook.CreateSheet();

            IRow row;
            row = sheet.CreateRow(0);
            for (int a = 0; a < dt.Columns.Count; a++)
            {
                row.CreateCell(a, CellType.STRING).SetCellValue(dt.Columns[a].ColumnName);
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                row = sheet.CreateRow(i + 1);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    row.CreateCell(j, CellType.STRING).SetCellValue(dt.Rows[i][j].ToString());
                }
            }

            workbook.Write(ms);
            ms.Flush();
            return ms;
        }

        /// <summary>
        /// 导出EXCEL(读取带样式的模板存到内存中，并且是否从第一行写起)
        /// BY TX 2013-6-20
        /// </summary>
        /// <param name="OldFileName"></param>
        /// <param name="dt"></param>
        /// <param name="FistRow">是否从第一行写起</param>
        /// <returns></returns>
        public Stream GenerationExcel(string OldFileName, DataTable dt,bool first)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                XSSFWorkbook workbook;
                using (FileStream file = new FileStream(OldFileName, FileMode.Open, FileAccess.Read))
                {
                    workbook = new XSSFWorkbook(file);
                }
                ISheet sheet = workbook.GetSheetAt(0);

                IRow row;
                if (first)
                {
                    row = sheet.GetRow(0);
                }
                else
                {
                    row = sheet.GetRow(1);
                }
                for (int a = 0; a < dt.Columns.Count; a++)
                {
                    try
                    {
                        row.GetCell(a).SetCellValue(dt.Columns[a].ColumnName);
                    }
                    catch
                    {
                        row.CreateCell(a, CellType.STRING).SetCellValue(dt.Columns[a].ColumnName);
                    }
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    row = sheet.CreateRow(i + 1);
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        try
                        {
                            row.GetCell(j).SetCellValue(dt.Rows[i][j].ToString());
                        }
                        catch
                        {
                            row.CreateCell(j, CellType.STRING).SetCellValue(dt.Rows[i][j].ToString());
                        }
                    }
                }
                workbook.Write(ms);
                ms.Flush();
                return ms;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
       
    }
}
