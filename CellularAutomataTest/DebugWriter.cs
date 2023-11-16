using System;
using System.Diagnostics;
using System.IO;

namespace CellularAutomataTest
{
    public class DebugWriter
    {
        public DebugWriter()
        {
            this.Write("Session started");
        }

        public void Write(string message)
        {
            string timestamp = "[" + DateTime.Now.ToString("dd/mm/yyyy HH:mm:ss:ff") + "]";
            Debug.WriteLine(timestamp + message);
        }

        public void Terminate()
        {
            this.Write("Session ended");
        }
    }
}
