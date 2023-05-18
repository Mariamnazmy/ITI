using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cfour
{
    public partial class Form1 : Form
    {
        //Intailizing The Variable That WIll Be Used.
        byte[] bt;
        IPAddress LocalAddress;
        TcpListener Server;
        NetworkStream NetStream;
        Socket Connection;
        BinaryReader binaryreader;
        BinaryWriter binarywriter;
        string threading;
        public Form1()
        {
            InitializeComponent();
            Thread thread = new Thread(() =>
            {
                while (true)
                {
                    if (NetStream != null)
                    {
                        binaryreader = new BinaryReader(NetStream);
                        threading = binaryreader.ReadString();
                        Invoke(new Action(() =>
                        {
                            listView1.Items.Add(threading);
                        }));
                        NetStream.Flush();
                    }
                }
            }
           );
            thread.Start();
        }
        private void button1_Click(object sender, EventArgs e)
        {     //start to create the server and make it starts 
            bt = new byte[] { 127, 0, 0, 1 };
            LocalAddress = new IPAddress(bt);
            Server = new TcpListener(LocalAddress, 1024);
            //Make The Server Ready to Any Signal
            Server.Start();
            //Stablish A connection 
            Connection = Server.AcceptSocket();
            NetStream = new NetworkStream(Connection);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            binaryreader.Close();
            NetStream.Close();
            Connection.Shutdown(SocketShutdown.Both);
            Connection.Close();
            binarywriter.Close();
        }

    }
}
