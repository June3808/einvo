using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Content;

namespace EasyAbp.FileManagement.DataExporting.NPOI
{
    public abstract class NpoiExcelImporterBase<TEntity>
    {
        protected List<TEntity> ProcessExcelFile(byte[] fileBytes, Func<ISheet, int, TEntity> processExcelRow)
        {
            var entities = new List<TEntity>();

            using (var stream = new MemoryStream(fileBytes))
            {
                var workbook = new XSSFWorkbook(stream);
                for (var i = 0; i < workbook.NumberOfSheets; i++)
                {
                    var entitiesInWorksheet = ProcessWorksheet(workbook.GetSheetAt(i), processExcelRow);
                    entities.AddRange(entitiesInWorksheet);
                }
            }

            return entities;
        }

        private List<TEntity> ProcessWorksheet(ISheet worksheet, Func<ISheet, int, TEntity> processExcelRow)
        {
            var entities = new List<TEntity>();

            var rowEnumerator = worksheet.GetRowEnumerator();
            rowEnumerator.Reset();

            var i = 0;
            while (rowEnumerator.MoveNext())
            {
                if (i == 0)
                {
                    //Skip header
                    i++;
                    continue;
                }
                try
                {
                    var entity = processExcelRow(worksheet, i++);
                    if (entity != null)
                    {
                        entities.Add(entity);
                    }
                }
                catch (Exception)
                {
                    //ignore
                }
            }

            return entities;
        }

        public List<TEntity> ImportEntityFromExcel(string fullFilePath)
        {
            if (!File.Exists(fullFilePath))
                throw new Exception($"File not exists!{fullFilePath}");
            var sFileExtension = NpoiExcelImporterBase.CalculateFileExtension(fullFilePath);
            using (var stream = new FileStream(fullFilePath, FileMode.Open))
            {

                stream.Position = 0;
                ISheet worksheet;
                if (sFileExtension == ".xls")//This will read the Excel 97-2000 formats    
                {
                    HSSFWorkbook hssfwb = new HSSFWorkbook(stream);
                    worksheet = hssfwb.GetSheetAt(0);
                }
                else //This will read 2007 Excel format    
                {
                    XSSFWorkbook hssfwb = new XSSFWorkbook(stream);
                    worksheet = hssfwb.GetSheetAt(0);
                }
                var entities = new List<TEntity>();

                var rowEnumerator = worksheet.GetRowEnumerator();
                rowEnumerator.Reset();

                var rowIndex = 0;
                TEntity entity = default(TEntity);
                Dictionary<string, int> headerColumns = new Dictionary<string, int>();

                while (rowEnumerator.MoveNext())
                {

                    try
                    {
                        if (rowIndex == 0)
                        {
                            //header
                            var columnNameList = worksheet.GetRow(rowIndex).Cells;
                            for (int j = 0; j < columnNameList.Count; j++)
                            {
                                var cell = columnNameList[j];
                                var headerName = cell.StringCellValue.ToUpper().Replace(" ", "");

                                //remove all whitespace
                                headerName = headerName.Replace(" ", "");
                                headerColumns.Add(headerName, j);
                            }
                            rowIndex++;
                            continue;
                        }
                        entity = Activator.CreateInstance<TEntity>();
                        var exceptionMessage = new StringBuilder();
                        //find out the type
                        Type type = entity.GetType();
                        //loop all properties information based on the type
                        foreach (PropertyInfo prop in type.GetProperties())
                        {
                            try
                            {
                                var columnIndex = headerColumns[prop.Name.ToUpper()];

                                //get each property information based on property name
                                System.Reflection.PropertyInfo propertyInfo = type.GetProperty(prop.Name);

                                //find the property type
                                Type propertyType = propertyInfo.PropertyType;

                                var columnValue = GetRequiredValueFromRowOrNull(worksheet, rowIndex, columnIndex, prop.Name, exceptionMessage, propertyType);
                                if (!object.Equals(columnValue, DBNull.Value))
                                {
                                    //prop.SetValue(entity, columnValue, null);
                                    this.SetValue(entity, prop.Name, columnValue, propertyInfo);
                                }
                            }
                            catch (Exception e)
                            {
                                continue;
                            }
                        }
                        rowIndex++;
                        entities.Add(entity);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        //ignore
                    }
                }

                return entities;
            }
        }

        public bool ValidateExcelHeader(string fullFilePath)
        {
            if (!File.Exists(fullFilePath))
                throw new Exception($"File not exists!{fullFilePath}");
            var sFileExtension = NpoiExcelImporterBase.CalculateFileExtension(fullFilePath);
            using (var stream = new FileStream(fullFilePath, FileMode.Open))
            {

                stream.Position = 0;
                ISheet worksheet;
                if (sFileExtension == ".xls")//This will read the Excel 97-2000 formats    
                {
                    HSSFWorkbook hssfwb = new HSSFWorkbook(stream);
                    worksheet = hssfwb.GetSheetAt(0);
                }
                else //This will read 2007 Excel format    
                {
                    XSSFWorkbook hssfwb = new XSSFWorkbook(stream);
                    worksheet = hssfwb.GetSheetAt(0);
                }

                var rowEnumerator = worksheet.GetRowEnumerator();
                rowEnumerator.Reset();

                var rowIndex = 0;
                var TotalHeaderValidated = 0;
                var Headers = new List<string>();

                TEntity entity = default(TEntity);
                entity = Activator.CreateInstance<TEntity>();
                var exceptionMessage = new StringBuilder();

                try
                {
                    //get all headers
                    var columnNameList = worksheet.GetRow(rowIndex).Cells;
                    for (int j = 0; j < columnNameList.Count; j++)
                    {
                        var cell = columnNameList[j];
                        var headerName = cell.StringCellValue.ToUpper().Replace(" ", "");

                        //remove all whitespace
                        headerName = headerName.Replace(" ", "");
                        Headers.Add(headerName);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    //ignore
                }

                //find out the type
                Type type = entity.GetType();
                //loop all properties information based on the type
                //check if header is present

                if (Headers == null || Headers.Count == 0)
                    return false;

                var TotalDtoProperties = 0;
                foreach (PropertyInfo prop in type.GetProperties())
                {
                    try
                    {
                        if (Headers.Contains(prop.Name.ToUpper()))
                        {
                            TotalHeaderValidated++;
                        };

                        TotalDtoProperties++;

                    }
                    catch (Exception e)
                    {
                        continue;
                    }
                }

                return TotalHeaderValidated == TotalDtoProperties;
            }
        }
        private void SetValue(object inputObject, string propertyName, object propertyVal, System.Reflection.PropertyInfo propertyInfo)
        {


            //find the property type
            Type propertyType = propertyInfo.PropertyType;

            //Convert.ChangeType does not handle conversion to nullable types
            //if the property type is nullable, we need to get the underlying type of the property
            var targetType = IsNullableType(propertyType) ? Nullable.GetUnderlyingType(propertyType) : propertyType;

            if (propertyType.IsEnum)
            {
                propertyVal = Convert.ToInt32(propertyVal);
                object newEnumValue = Enum.ToObject(propertyType, propertyVal);
                //Set the value of the property
                propertyInfo.SetValue(inputObject, newEnumValue, null);
            }
            else
            {
                //Returns an System.Object with the specified System.Type and whose value is
                //equivalent to the specified object.
                if (propertyType == typeof(Guid))
                {
                    propertyVal = new Guid(propertyVal.ToString());
                }
                else
                {
                    propertyVal = Convert.ChangeType(propertyVal, targetType);
                }

                //Set the value of the property
                propertyInfo.SetValue(inputObject, propertyVal, null);
            }

        }
        private static bool IsNullableType(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
        }

        private string GetRequiredValueFromRowOrNull(
                                                    ISheet worksheet,
                                                    int row,
                                                    int column,
                                                    string columnName,
                                                    StringBuilder exceptionMessage,
                                                    Type propertyType)
        {
            var cell = worksheet.GetRow(row).GetCell(column);
            if (cell == null)
            {
                if (propertyType == typeof(string))
                    return "";

                exceptionMessage.Append($"Column Name:{columnName} in column:{column} Is Invalid");
                return null;
            }

            var cellValue = "";
            var cellType = cell.CellType;

            //if (cellType.HasValue)
            //{
            //    cell.SetCellType(cellType.Value);
            //}
            //else
            //{
            //    cell.SetCellType(CellType.String);
            //}
            //var cellValue = cell.StringCellValue;

            //switch (cellType)
            //{
            //    case CellType.Blank:
            //    case CellType.Error:
            //        // ignore all blank or error cells
            //        break;
            //    case CellType.Numeric:
            //        break;
            //    case CellType.Boolean:
            //        text = cell.BooleanCellValue.ToString();
            //        break;
            //    case CellType.String:
            //    default:
            //        text = cell.StringCellValue;
            //        break;
            //}

            try
            {
                //if the property type is nullable, we need to get the underlying type of the property
                propertyType = IsNullableType(propertyType) ? Nullable.GetUnderlyingType(propertyType) : propertyType;
                TypeCode typeCode = Type.GetTypeCode(propertyType);

                switch (typeCode)
                {

                    case TypeCode.Byte:
                    case TypeCode.SByte:
                    case TypeCode.Decimal:
                    case TypeCode.Double:
                    case TypeCode.Int16:
                    case TypeCode.Int32:
                    case TypeCode.Int64:
                    case TypeCode.Single:
                    case TypeCode.UInt16:
                    case TypeCode.UInt32:
                    case TypeCode.UInt64:
                        cellValue = cell.NumericCellValue.ToString();
                        break;
                    case TypeCode.Boolean:
                        if (cellType == CellType.Numeric)
                        {
                            cellValue = cell.NumericCellValue.ToString();
                            if (cellValue == "1")
                                cellValue = "true";
                            else
                                cellValue = "false";
                        }
                        else
                        {
                            cellValue = cell.BooleanCellValue.ToString();
                        }
                        break;
                    case TypeCode.DateTime:
                        cellValue = cell.DateCellValue.ToString();
                        break;
                    case TypeCode.String:
                    case TypeCode.Char:
                        if (cellType == CellType.Numeric)
                        {
                            cellValue = cell.NumericCellValue.ToString();
                        }
                        else
                        {
                            cellValue = cell.StringCellValue.ToString();
                        }
                        break;
                    case TypeCode.Object:
                    default:
                        cellValue = cell.StringCellValue.ToString();
                        break;
                }//switch
            }
            catch (Exception ex)
            {
                exceptionMessage.Append($"Column Name:{columnName} in column:{column} Is error,{ex.Message}");
            }

            if (cellValue != null && cellValue != "NULL")
            {
                return cellValue;
            }

            exceptionMessage.Append($"Column Name:{columnName} in column:{column} Is Invalid");
            return null;
        }
    }

    public abstract class NpoiExcelImporterBase
    {
        public ISheet ImportEntityFromExcel(string filename, Stream stream)
        {
            //var stream = file.GetStream();

            var sFileExtension = CalculateFileExtension(filename);
            stream.Position = 0;
            ISheet worksheet;
            if (sFileExtension == ".xls")//This will read the Excel 97-2000 formats    
            {
                HSSFWorkbook hssfwb = new HSSFWorkbook(stream);
                worksheet = hssfwb.GetSheetAt(0);
            }
            else //This will read 2007 Excel format    
            {
                XSSFWorkbook hssfwb = new XSSFWorkbook(stream);
                worksheet = hssfwb.GetSheetAt(0);
            }

            return worksheet;
        }

        public static string CalculateFileExtension(string fileName)
        {
            if (!fileName.Contains("."))
            {
                return null;
            }

            return fileName.Substring(fileName.LastIndexOf(".", StringComparison.Ordinal) + 1);
        }
    }
}
