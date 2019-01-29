using System;
using System.IO;

namespace MyRLGProject.Helper
{
    public class Logger
    {
        public void Log(string log)
        {
            DateTime date = DateTime.Now;
            string fileName = "Logs\\"+"Log_" + date.Day + date.Month + date.Year+".txt";
            string filePath = System.Web.Hosting.HostingEnvironment.MapPath("~")+ fileName; 
            File.AppendAllText(filePath, log+"\n");
        }
    }
}
