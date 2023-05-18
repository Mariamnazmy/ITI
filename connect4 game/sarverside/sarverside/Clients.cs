using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sarverside
{
    public class Clients
    {
        private Socket socket;
        private NetworkStream nStream;
        private BinaryReader breader;
        private BinaryWriter bwriter;
        private List<string> msgs;
        private Thread thread;
       
        private int endpoint;
        private string name;
        public int ID { get; set; }


        TcpClient _tcpClient;
        StreamWriter _streamWriter;
        StreamReader _streamReader;
        NetworkStream networkStream;

       



        //this is the actual constarctor we use for clients
        public Clients(TcpClient tcpClient)
        {
            _tcpClient = tcpClient;

            networkStream = tcpClient.GetStream();

            _streamReader = new StreamReader(networkStream);

            _streamWriter = new StreamWriter(networkStream);

            _streamWriter.AutoFlush = true;
            /*_streamWriter.WriteLine("welcome from server");*/
            Reeadmsgs();
        }

        public async void Reeadmsgs()
        {
            while (true)
            {
                string msg = await _streamReader.ReadLineAsync();

                MessageBox.Show(msg);
            }

        }

        public string bWriter
        {
            set
            {
                try
                { bwriter.Write(value); }
                catch (Exception)
                {
                    Disconnect();
                }
            }
        }

        public List<string> Msgs { get { return msgs; } }
        public int Endpoint { get { return endpoint; } }
        public string Name { get { return name; } }

        //this is old test code
        public Clients(Socket socket)
        {
            this.socket = socket;
            endpoint = int.Parse(socket.RemoteEndPoint.ToString().Split(':')[1]);
            nStream = new NetworkStream(socket);
            breader = new BinaryReader(nStream);
            bwriter = new BinaryWriter(nStream);
            msgs = new List<string>();
            name = breader.ReadString();
            bWriter = endpoint.ToString();

            thread = new Thread(new ThreadStart(DataRead));
            thread.Start();
            
        }

        private void DataRead()
        {
            while (true)
            {
                try
                {
                    msgs.Add(breader.ReadString());
                }
                catch (Exception) { Disconnect(); }
            }
        }

        public void RemoveMsg(string msg)
        {
            msgs.Remove(msg);
        }
        public void Disconnect()
        {
            thread.Abort();
            breader.Close();
            bwriter.Close();
            nStream.Close();
        }

        
    }
}
