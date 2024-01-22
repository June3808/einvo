using EasyAbp.FileManagement;
using EasyAbp.FileManagement.Storage;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.FileManagement.DataExporting.NPOI
{
    public abstract class NpoiExcelExporterBase : FileManagementAppService, ITransientDependency
    {
        private readonly ITempFileCacheManager _tempFileCacheManager;

        protected NpoiExcelExporterBase(ITempFileCacheManager tempFileCacheManager)
        {
            _tempFileCacheManager = tempFileCacheManager;
        }

        protected FileDto CreateExcelPackage(string fileName, Action<XSSFWorkbook> creator)
        {
            var file = new FileDto(fileName, MimeTypeNames.ApplicationVndOpenxmlformatsOfficedocumentSpreadsheetmlSheet);
            var workbook = new XSSFWorkbook();

            creator(workbook);

            Save(workbook, file);

            return file;
        }

        protected FileDto CreateExcelPackageIntoPath(string fileName, string filePath, Action<XSSFWorkbook> creator)
        {
            var file = new FileDto(fileName, MimeTypeNames.ApplicationVndOpenxmlformatsOfficedocumentSpreadsheetmlSheet);
            var workbook = new XSSFWorkbook();
            creator(workbook);

            SaveExcelIntoPath(workbook, file, filePath);

            return file;
        }

        protected void AddHeader(ISheet sheet, params string[] headerTexts)
        {
            if (headerTexts.IsNullOrEmpty())
            {
                return;
            }

            sheet.CreateRow(0);

            for (var i = 0; i < headerTexts.Length; i++)
            {
                AddHeader(sheet, i, headerTexts[i]);
            }
        }

        protected void AddHeader(ISheet sheet, int columnIndex, string headerText)
        {
            var cell = sheet.GetRow(0).CreateCell(columnIndex);
            cell.SetCellValue(headerText);
            var cellStyle = sheet.Workbook.CreateCellStyle();
            var font = sheet.Workbook.CreateFont();
            font.IsBold = true;
            font.FontHeightInPoints = 12;
            cellStyle.SetFont(font);
            cell.CellStyle = cellStyle;
        }

        protected void AddObjects<T>(ISheet sheet, int startRowIndex, IList<T> items, params Func<T, object>[] propertySelectors)
        {
            if (items.IsNullOrEmpty() || propertySelectors.IsNullOrEmpty())
            {
                return;
            }

            for (var i = 1; i <= items.Count; i++)
            {
                var row = sheet.CreateRow(i);

                for (var j = 0; j < propertySelectors.Length; j++)
                {
                    var cell = row.CreateCell(j);
                    var value = propertySelectors[j](items[i - 1]);
                    if (value != null)
                    {
                        // hit error: The maximum length of cell contents (text) is 32,767 characters
                        var stringValue = value.ToString();
                        if (stringValue.Length > 32767)
                        {
                            stringValue = stringValue.Replace("	", "");
                            stringValue = stringValue.Replace(" - ", "-");
                            stringValue = stringValue.Replace(": ", ":");
                            stringValue = stringValue.Replace("Item number", "ItemNo");
                            stringValue = stringValue.Replace(" 0.00 ", "0.00");
                            stringValue = stringValue.Replace("Line number", "LineNo");
                            stringValue = stringValue.Replace("Batch number", "BatchNo");
                            if (stringValue.Length > 32767)
                                stringValue = stringValue.Substring(0, 32767);
                        }
                        cell.SetCellValue(stringValue);
                    }
                }
            }
        }

        protected void AddObjectsWithChildRows<T>(ISheet sheet, int startRowIndex, IList<T> items, int? childRowSeparationPosition = null, char listSeparateSign = ',', params Func<T, object>[] propertySelectors)
        {
            if (items.IsNullOrEmpty() || propertySelectors.IsNullOrEmpty())
            {
                return;
            }

            var count = 1;
            for (var i = 1; i <= items.Count; i++)
            {
                var row = sheet.CreateRow(count);

                for (var j = 0; j < propertySelectors.Length; j++)
                {
                    var cell = row.CreateCell(j);
                    var value = propertySelectors[j](items[i - 1]);
                    if (value != null)
                    {
                        if (j == childRowSeparationPosition)
                        {
                            var childValues = value.ToString();

                            if (!string.IsNullOrEmpty(childValues))
                            {
                                var childRows = childValues.Split(listSeparateSign);
                                cell.SetCellValue(childRows[0]);

                                for (var c = 1; c < childRows.Length; c++)
                                {
                                    count = count + 1;

                                    var Crow = sheet.CreateRow(count);
                                    var Ccell = Crow.CreateCell(j);
                                    Ccell.SetCellValue(childRows[c]);
                                }
                            }
                        }
                        else
                        {
                            cell.SetCellValue(value.ToString());
                        }
                    }
                }

                count = count + 1;
            }
        }

        protected void Save(XSSFWorkbook excelPackage, FileDto file)
        {
            using (var stream = new MemoryStream())
            {
                excelPackage.Write(stream);
                _tempFileCacheManager.SetFile(file.FileToken, stream.ToArray());
            }
        }

        protected void SaveExcelIntoPath(XSSFWorkbook excelPackage, FileDto file, string filePath)
        {
            using (var stream = new MemoryStream())
            {
                excelPackage.Write(stream);
                _tempFileCacheManager.SetFile(file.FileToken, stream.ToArray());
                excelPackage.Package.Save(path: filePath);
            }
        }

        protected void SetCellDataFormat(ICell cell, string dataFormat)
        {
            if (cell == null)
            {
                return;
            }

            var dateStyle = cell.Sheet.Workbook.CreateCellStyle();
            var format = cell.Sheet.Workbook.CreateDataFormat();
            dateStyle.DataFormat = format.GetFormat(dataFormat);
            cell.CellStyle = dateStyle;
            if (DateTime.TryParse(cell.StringCellValue, out var datetime))
            {
                cell.SetCellValue(datetime);
            }
        }

        protected void AddObjectsFromDataTable(ISheet sheet, DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }


            var headers = new List<string>();

            foreach (DataColumn dc in dt.Columns)
            {
                //if (dc.DataType == typeof(decimal))
                //    sheet.Column(i).Style.Numberformat.Format = "#0.00";
                //else if (dc.DataType == typeof(DateTime))
                //    sheet.Column(i).Style.Numberformat.Format = "dd/MM/yyyy HH:mm:ss";
                //i++;
                headers.Add(dc.ColumnName);
            }

            this.AddHeader(sheet, headers.ToArray());

            for (var i = 0; i < dt.Rows.Count; i++)
            {
                var row = sheet.CreateRow(i + 1);// current worksheet row index is 1, row index 0 is header 

                for (var j = 0; j < headers.Count; j++)
                {
                    var cell = row.CreateCell(j);
                    var value = dt.Rows[i][headers[j]];
                    if (value != null)
                    {
                        cell.SetCellValue(value.ToString());
                    }
                }
            }
        }
    }
}
