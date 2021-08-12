using System;
using System.IO;
using System.Reflection;


namespace CheckOut.Promotion.Engine.Infrastructure
{

    public static class LogWriter
    {
        private static string _mExePath = string.Empty;

        public static void LogWrite(string logMessage)
        {
            _mExePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            try
            {
                using var w = File.AppendText(_mExePath + "\\" + Constants.LogFileName);
                Log(logMessage, w);
            }
            catch (Exception)
            {
            }
        }

        public static void Log(string logMessage, TextWriter txtWriter)
        {
            try
            {
                txtWriter.Write("\r\nLog Entry : ");
                txtWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToLongDateString());
                txtWriter.WriteLine("  :");
                txtWriter.WriteLine("  :{0}", logMessage);
                txtWriter.WriteLine("-------------------------------");
            }
            catch (Exception)
            {
            }
        }
    }
}
