using System.IO;

namespace MyRLGProject.Helper
{
    public class Logger
    {
        public void Log(string log)
        {
            string filePath = System.Web.Hosting.HostingEnvironment.MapPath("~")+"Logs\\log.txt"; ;
            File.AppendAllText(filePath, log+"\n");            
        }
    }
}