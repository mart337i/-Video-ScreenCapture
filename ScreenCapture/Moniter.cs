using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ScreenCapture
{
    public class Moniter
    {
        public IEnumerable<Process> WindowProcesses()
        {
            foreach(var proc in Process.GetProcesses())
            {
                if(proc.MainWindowHandle != IntPtr.Zero)
                {
                    yield return proc;
                }
                Console.WriteLine(proc);
            }
        }
    }
}