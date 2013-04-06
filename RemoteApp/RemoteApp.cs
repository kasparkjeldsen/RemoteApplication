using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.Collections;
using System.Threading;
using WindowScrape.Types;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;

namespace RemoteApp
{
    /// <summary>
    /// Remote object 
    /// </summary>
    public class RemoteApp : MarshalByRefObject
    {
        //loads of static windows methods.
        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hWnd, out Rectangle lpRect);
        [DllImport("user32.dll")]
        public static extern bool PrintWindow(IntPtr hWnd, IntPtr hdcBlt, int nFlags);
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        static extern uint keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        private Dictionary<int, HwndObject> images;
        private int pnumber = 0;
        private String lastImage;
        private Dictionary<int,List<byte>> diffbytes;
        private Dictionary<int, List<int>> diffvalues;
        public RemoteApp()
        {
            images = new Dictionary<int, HwndObject>();
            diffbytes = new Dictionary<int, List<byte>>();
            diffvalues = new Dictionary<int, List<int>>();
        }
        public List<HwndObject> getProcesses()
        {
            List<HwndObject> procs = new List<HwndObject>();
            List<HwndObject> p = HwndObject.GetWindows();
            foreach (HwndObject o in p)
            {
                if (o.Title.Length > 0)
                {
                    if (!o.Size.IsEmpty && o.Size.Height > 1 && o.Size.Width > 1)
                    {
                        procs.Add(o);
                    }
                }
            }
            return procs;
        }
        public ArrayList getNames()
        {
            ArrayList l = new ArrayList();
            var procs = getProcesses();
            foreach (HwndObject o in procs)
            {
                l.Add(o.Title);
            }
            return l;
        }
        public IntPtr getHandleFromName(String inp)
        {
            List<HwndObject> p = HwndObject.GetWindows();
            foreach (HwndObject o in p)
            {
                if (o.Title.Length > 0)
                {
                    if (o.Title.ToLower() == inp.ToLower()) return o.Hwnd;
                }
            }
            return IntPtr.Zero;
        }
        public int setProcess(IntPtr i)
        {
            HwndObject hw = new HwndObject(i);
            pnumber++;
            images.Add(pnumber, hw);
            return pnumber;
        }
        public void setSize(int number, Size s)
        {
            HwndObject h = images[number];
            h.Size = s;
        }
        //MOUSE
        public void MouseLeftDown(int number)
        {
            var o = images[number];
            SetForegroundWindow(o.Hwnd);
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown);
        }
        public void MouseRightDown(int number)
        {
            var o = images[number];
            SetForegroundWindow(o.Hwnd);
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.RightDown);
        }
        public void MouseLeftUp(int number)
        {
            var o = images[number];
            SetForegroundWindow(o.Hwnd);
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp);
        }
        public void MouseRightUp(int number)
        {
            var o = images[number];
            SetForegroundWindow(o.Hwnd);
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.RightUp);
        }
        public void MouseMove(int number, Point p)
        {
            var o = images[number];
            MouseOperations.SetCursorPosition(new MouseOperations.MousePoint(o.Location.X + p.X, o.Location.Y + p.Y));
        }
        //keyboard
        public void KeyPress(System.Windows.Forms.Keys key, int number)
        {
            var o = images[number];
            SetForegroundWindow(o.Hwnd);
            keybd_event((byte)key, 0, 0, 0);
            keybd_event((byte)key, 0, 0x7F, 0);
        }

        public void ShiftDown(System.Windows.Forms.Keys key, int number)
        {
            var o = images[number];
            SetForegroundWindow(o.Hwnd);
            keybd_event((byte)key, 0, 0, 0);
        }
        public void ShiftUp(System.Windows.Forms.Keys key, int number)
        {
            var o = images[number];
            SetForegroundWindow(o.Hwnd);
            keybd_event((byte)key, 0, 0, 0);
        }
        public String PrintWindow(int number)
        {
            HwndObject pr = images[number];
            Bitmap bmp = new Bitmap(pr.Size.Width, pr.Size.Height, PixelFormat.Format32bppArgb);
            Graphics gfxBmp = Graphics.FromImage(bmp);
            IntPtr hdcBitmap = gfxBmp.GetHdc();
            PrintWindow(pr.Hwnd, hdcBitmap, 0);
            gfxBmp.ReleaseHdc(hdcBitmap);
            gfxBmp.Dispose();

            MemoryStream ms = new MemoryStream();
            String r = Convert.ToBase64String(ms.ToArray());
            bmp.Save(ms, ImageFormat.Jpeg);
            if (lastImage == r) return "0"; //doesn't work... yet
            else
            {
                lastImage = r;
                return r;
            }
        }
    }
}
