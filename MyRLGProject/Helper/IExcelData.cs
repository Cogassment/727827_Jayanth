using System.Web;

namespace MyRLGProject.Helper
{
    interface IExcelData
    {
        string Convert(HttpPostedFileBase UploadFile);
        void DeleteFile(string file);
    }
}
