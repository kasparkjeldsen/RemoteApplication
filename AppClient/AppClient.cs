using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RemoteApp;
using WindowScrape;
using System.Collections;

namespace AppClient
{
    /// <summary>
    /// Client part of server/client application.
    /// Handles connecting to the server, which is hopefully running, since we at this point don't have a fallback
    /// </summary>
    public partial class AppClient : Form
    {
        private RemoteApp.RemoteApp ra;
        public AppClient()
        {
            TcpChannel chan = new TcpChannel();
            ChannelServices.RegisterChannel(chan, false);
            ra = (RemoteApp.RemoteApp)Activator.GetObject(
                typeof(RemoteApp.RemoteApp),
                "tcp://localhost:9000/remoteapp");
            if (ra == null)
                Console.WriteLine("cannot locate server");
            else
            {
                InitializeComponent();
                ArrayList l = ra.getNames();
                foreach (String s in l)
                    procs.Items.Add(s);
            }
            
        }
        /// <summary>
        /// Pass on remoteApp object
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void open_Click(object sender, EventArgs e)
        {
            var i = ra.getHandleFromName(procs.Text);
            int b = ra.setProcess(i);
            new ShowApp(ra,b).Show();
        }
    }
}
