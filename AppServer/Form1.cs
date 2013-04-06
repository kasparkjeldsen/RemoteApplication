using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RemoteApp;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

namespace AppServer
{
    /// <summary>
    /// server part
    /// </summary>
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// when big boring button is clicked start to listen on port 9000
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void start_Click(object sender, EventArgs e)
        {
            TcpServerChannel channel = new TcpServerChannel(9000);
            ChannelServices.RegisterChannel(channel, false);
            WellKnownServiceTypeEntry remObj = new WellKnownServiceTypeEntry
            (
            typeof(RemoteApp.RemoteApp),
            "remoteapp",
            WellKnownObjectMode.Singleton
            );
            RemotingConfiguration.RegisterWellKnownServiceType(remObj);
        }
    }
}
