using MyRLGProject.Helper;
using MyRLGProject.Models;
using System.Web;
using System.Web.Mvc;

namespace MyRLGProject.Controllers
{
    public class HomeController : Controller
    {
        private IExcelData excelData;

        public HomeController()
        {
            excelData = new ExcelData();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Upoloaded file will be processed and returned to view as Json result in this action method
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadFiles()
        {
            HttpFileCollectionBase files = Request.Files;
            HttpPostedFileBase UploadFile = files[0];
            string JsonString = excelData.Convert(UploadFile);
            return Json(new { JsonString }, JsonRequestBehavior.AllowGet);
        }
    }
}