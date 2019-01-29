using ExcelDataReader;
using Newtonsoft.Json;
using System;
using System.Data;
using System.IO;
using System.Web;

namespace MyRLGProject.Helper
{
    public class ExcelData : IExcelData
    {
        private Logger log = new Logger();
        private const string newFormat = ".xlsx";
        private const string oldFormat = ".xls";
        public string Convert(HttpPostedFileBase UploadFile)
        {
            string JsonString = "false";
            log.Log(DateTime.UtcNow.ToString());
            if (UploadFile.ContentLength > 0)
            {
                try
                {
                    string temppath = Path.GetTempPath();
                    var fileName = Path.GetFileName(UploadFile.FileName);
                    string FilePath = Path.Combine(temppath, fileName);
                    if (FilePath.EndsWith(newFormat) || FilePath.EndsWith(oldFormat))
                    {
                        UploadFile.SaveAs(FilePath); //storing uploaded file temporarily in client temporary directory
                        log.Log(LogMessage.Uploaded);

                        FileStream stream = System.IO.File.Open(FilePath, FileMode.Open, FileAccess.Read);
                        IExcelDataReader excelReader = null;

                        if (FilePath.EndsWith(oldFormat))
                            excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                        if (FilePath.EndsWith(newFormat))
                            excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

                        DataSet result = excelReader.AsDataSet();
                        excelReader.Close();
                        
                        DeleteFile(FilePath);

                        JsonString = JsonConvert.SerializeObject(result.Tables[0]);
                        log.Log(LogMessage.Returned);
                        return JsonString;
                    }
                    else
                    {
                        log.Log(LogMessage.NotExcelFile);
                        return JsonString;
                    }
                }
                catch (Exception e)
                {
                    log.Log("Exception: " + e.Message +LogMessage.Separator);
                    return JsonString;
                }
            }
            else
            {
                log.Log(LogMessage.FileError);
                return JsonString;
            }
        }

        public void DeleteFile(string file)
        {
            if (File.Exists(file))
            {
                File.Delete(file);
                log.Log(LogMessage.Deleted);
            }
        }
    }
}
