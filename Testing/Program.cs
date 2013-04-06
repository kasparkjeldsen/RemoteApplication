using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RemoteApp;
using System.Diagnostics;
using System.Drawing;
using WindowScrape.Types;
using System.IO;
using System.IO.Compression;

namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            RemoteApp.RemoteApp ra = new RemoteApp.RemoteApp();
            var t = ra.getProcesses();
            HwndObject pr = null;
            foreach(HwndObject p in t)
            {
                if (p.Title.ToLower().Contains("google chrome"))
                    pr = p;
            }
            ra.setProcess(pr);
            
            var b = ra.PrintWindow();
        }
    }
}
