using System;
using System.IO;

namespace CellularAutomataTest
{
    public class DebugWriter
    {
        private const string log_path = "dbglog.txt";
        private StreamWriter stream;
        public DebugWriter()
        {
            stream = new StreamWriter(log_path);
            this.Write("Session started");
        }

        public void Write(string message)
        {
            string timestamp = "[" + DateTime.Now.ToString("dd/mm/yyyy HH:mm:ss:ff") + "]";
            stream.WriteLine("{timestamp} {message}");
        }

        public void Terminate()
        {
            this.Write("Session ended");
            stream.Close();
        }
    }
}
