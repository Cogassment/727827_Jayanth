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

        public string Convert(HttpPostedFileBase UploadFile)
        {
            string JsonString = string.Empty;
            log.Log("\n" + DateTime.UtcNow.ToString() + "\n");
            if (UploadFile.ContentLength > 0)
            {
                try
                {
                    string temppath = Path.GetTempPath();
                    var fileName = Path.GetFileName(UploadFile.FileName);
                    string FilePath = Path.Combine(temppath, fileName);
                    if (FilePath.EndsWith(".xlsx") || FilePath.EndsWith(".xls"))
                    {
                        UploadFile.SaveAs(FilePath); //storing uploaded file temporarily in client temporary directory
                        log.Log(LogMessage.Uploaded);

                        FileStream stream = System.IO.File.Open(FilePath, FileMode.Open, FileAccess.Read);
                        IExcelDataReader excelReader = null;

                        if (FilePath.EndsWith(".xls"))
                            excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                        if (FilePath.EndsWith(".xlsx"))
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
                        JsonString = "false";
                        return JsonString;
                    }
                }
                catch (Exception e)
                {
                    log.Log("Exception: " + e.Message + "\n" + LogMessage.Separator);
                    JsonString = "false";
                    return JsonString;
                }
            }
            else
            {
                log.Log(LogMessage.FileError);
                JsonString = "false";
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