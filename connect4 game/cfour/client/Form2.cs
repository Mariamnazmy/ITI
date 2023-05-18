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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Runtime.InteropServices.ComTypes;
using WMPLib;

namespace client
{
    public partial class Form2 : Form
    {
        byte[] Bytes;
        IPAddress IpForClient;
        TcpClient client;
        NetworkStream Networkstream;
        BinaryReader Reader;
        BinaryWriter Writer;
        string username;
        string Threading;
        Thread thread1;
        WindowsMediaPlayer player = new WindowsMediaPlayer();
        public Form2()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            player.URL = "music.mp3";
            player.controls.stop();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bytes = new byte[] {127,0, 0,1};
            IpForClient = new IPAddress(Bytes);
            client = new TcpClient();
            username = textBox1.Text;
            try
            {


                if (string.IsNullOrWhiteSpace(username))
                {
                    MessageBox.Show("Please enter a valid username.");
                }
                else
                {
                    client.Connect(IpForClient, 5000);
                    Networkstream = client.GetStream();
                    //Writer = new BinaryWriter(Networkstream);
                    //Writer.Write(textBox1.Text);
                    DialogResult = DialogResult.OK;
                }



            }
            catch
            {
                 MessageBox.Show("Server isn't avilable please check server connection");
                Application.Restart();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Reader.Close();
            Writer.Close();
            Networkstream.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            if (textBox1.Text == " ")
            {
                MessageBox.Show("Should Enter a UserName to Play. ");

               
            }
            else if (textBox1.Text == string.Empty)
            {
                MessageBox.Show("Should Enter a UserName to Play. ");

               

            }
            


        }

        public NetworkStream NetworkStream
        {
            get
            {
                return Networkstream;
            }
        }

        public string Username
        {
            get
            {
                return textBox1.Text;
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
         
             username = textBox1.Text;

            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Please enter a valid username.");
                return;
            }

           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (button3.Text == "Play Music")
            {
                player.controls.play();
                button3.Text = "stop";
            }
            else if (button3.Text == "stop")
            {
                player.controls.stop();
                button3.Text = "Play Music";
            }
        }
    }
}
