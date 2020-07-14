using System;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;

namespace DesignPatterns
{
    internal class TraceLine
    {
        private string _filePath;

        [Obsolete("Old Trace Initialization",true)]
        private void _Initialize(string filename = "Mylog.log")
        {
            var directoryInfo = Directory.GetParent(Environment.CurrentDirectory).Parent;
            if (directoryInfo != null)
                Trace.Listeners.Add(new TextWriterTraceListener(
                    $"{Path.Combine(directoryInfo.FullName, filename)}"));
        }

        private void _InitializeToFile(string fileName = "Mylog.log")
        {
            var directoryInfo = Directory.GetParent(Environment.CurrentDirectory).Parent;
            _filePath = $"{Path.Combine(directoryInfo.FullName, fileName)}";
           

        }
        public TraceLine()
        {
            _InitializeToFile();
        }

        public TraceLine(string filePath= "MyLog.log")
        {
            _InitializeToFile(filePath);
        }

        [Obsolete("Discontinued Trace",true)]
        public void WriteTrace(string msg, bool omitDate = false)
        {


            if (!omitDate)
                msg = DateTime.Now + ":" + msg;
            Trace.WriteLine(msg);
        }

        public void WriteLine(string msg, bool omitDate = false)
        {
            if (!omitDate)
                msg = DateTime.Now + ":" + msg;
            using (FileStream fs = new FileStream(_filePath, FileMode.Append, FileAccess.Write))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.WriteLine(msg);
            }
        }
    }
}