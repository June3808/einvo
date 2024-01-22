using EasyAbp.FileManagement.DataExporting.NPOI;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Content;

namespace EasyAbp.FileManagement.Application
{
    public static class CustomDtoExcelImporter
    {
        public static List<T> ImportEntityFromExcel<T>(string fullFilePath)
        {
            var importer = new CustomDtoExcelImporterBase<T>();
            return importer.ImportEntityFromExcel(fullFilePath);
        }

        public static ISheet ImportEntityFromStream(string filename, Stream stream)
        {
            var importer = new CustomDtoExcelImporterBase();
            return importer.ImportEntityFromExcel(filename, stream);
        }

        public static bool ValidateExcelHeader<T>(string fullFilePath)
        {
            var importer = new CustomDtoExcelImporterBase<T>();
            return importer.ValidateExcelHeader(fullFilePath);
        }

        public class CustomDtoExcelImporterBase<T> : NpoiExcelImporterBase<T>
        {
        }

        public class CustomDtoExcelImporterBase : NpoiExcelImporterBase
        {
        }
    }
}
