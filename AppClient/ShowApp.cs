using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RemoteApp;
using System.Threading;
using System.IO;

namespace AppClient
{
    /// <summary>
    /// Handles communication between this client and server.
    /// </summary>
    public partial class ShowApp : Form
    {
        private Image im;
        private bool sizesSynced = false;
        private RemoteApp.RemoteApp ra;
        private int pnum;
        public ShowApp(RemoteApp.RemoteApp ra, int pnum)
        {
            this.ra = ra;
            this.pnum = pnum;
            InitializeComponent();
            Thread t = new Thread(new ThreadStart(work));
            t.IsBackground = true;
            t.Start();
        }
        /// <summary>
        /// Worker thread trying to update picture
        /// </summary>
        public void work()
        {
            while (!this.Visible) ;//wait until window is visible.
            while (true)
            {
                try
                {
                    String base64 = this.ra.PrintWindow(pnum); //get windowframe in base64
                    if (base64 != "0") //at some point remoteApp will send "0" if the picture is the same as the one sent before.
                    {
                        byte[] n = Convert.FromBase64String(base64); 
                        MemoryStream ms = new MemoryStream(n);
                        im = Image.FromStream(ms); //make pretty picture and invoke imageframe.
                        image.Invoke(new Action(() => 
                        {
                            if (im != null) image.Image = im;
                            image.Size = im.Size;
                        }));
                        //if remote window isn't the same size as our window, resize.
                        if (!sizesSynced) 
                        {
                            this.Invoke(new Action(() =>
                            {
                                Size s = im.Size;
                                s.Height = s.Height + 20;
                                this.ClientSize = s;
                            }));
                            sizesSynced = true;
                        }
                    }
                    else
                    {
                        //here we can handle if the image we recieved is the same as the one before. Not implemented yet.
                    }
                    
                }
                catch 
                {
                    //presummably we want some kind of error-handling. Not now though!
                }
            }

        }
        /// <summary>
        /// Change remote application if this application changes size
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void image_SizeChanged(object sender, EventArgs e)
        {
            ra.setSize(pnum,image.Size);
        }
        /// <summary>
        /// Send mouse down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void image_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button.ToString().ToLower() == "left")
                ra.MouseLeftDown(pnum);
            if (e.Button.ToString().ToLower() == "right")
                ra.MouseRightDown(pnum);
        }
        /// <summary>
        /// Send mouse move
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void image_MouseMove(object sender, MouseEventArgs e)
        {
            ra.MouseMove(pnum, e.Location);
        }
        /// <summary>
        /// send mouse up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void image_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button.ToString().ToLower() == "left")
                ra.MouseLeftUp(pnum);
            if (e.Button.ToString().ToLower() == "right")
                ra.MouseRightUp(pnum);
        }
        
        //!!!this part isn't fully implemented yet and buggy as hell
        bool shift = false;
        /// <summary>
        /// Send keydown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowApp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 16) ra.ShiftDown(e.KeyCode, pnum);
            else
                ra.KeyPress(e.KeyCode, pnum);
        }
        /// <summary>
        /// Send key up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowApp_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 16) ra.ShiftDown(e.KeyCode, pnum);
        }
    }
}
