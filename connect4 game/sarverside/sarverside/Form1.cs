using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sarverside
{
    public partial class Form1 : Form
    {
        private Dictionary<int, Clients> clients;
        private Thread thread1;
        private Thread thread2;
        TcpListener listener;
        private Dictionary<int, _ٌRoom> rooms;
        private int roomid = 100;

        ~Form1()
        {
            thread1.Abort();
            thread2.Abort();
        }

        public Form1()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            listener = new TcpListener(ip, 5000);
            clients = new Dictionary<int, Clients>();
            rooms = new Dictionary<int, _ٌRoom>();

        }

        private void Start()
        {
            listener.Start();
            while (true)
            {
                   Socket socket = listener.AcceptSocket();
                if (socket.Connected) {
                    Clients client = new Clients(socket);
                    clients.Add(client.Endpoint, client);
                    clients_ports.Items.Add(client.Endpoint);
                    client_name.Items.Add(client.Name);
                    foreach (int index in rooms.Keys.ToList())
                    {
                        client.bWriter = "room&" + index + "&" + rooms[index].Name + "&" + rooms[index].num_of_players(); //request to server roomid & name & num of players
                    }

                } 
                   

            }


        } 

        private void button1_Click(object sender, EventArgs e)
        {
            thread1 = new Thread(Start);
            thread1.Priority = ThreadPriority.BelowNormal;
            thread2 = new Thread(RecieveData);
            thread2.Priority = ThreadPriority.Highest;
            thread1.IsBackground = true;
            thread2.IsBackground = true;
            thread1.Start();
            thread2.Start();
        }

        public void RecieveData()
        {
            while (true)
            {
                try
                {
                  foreach(int port in clients.Keys.ToList())
                    {
                        foreach(string msg in clients[port].Msgs.ToList()) 
                        {
                            string[] response = msg.Split('&');
                            string type = response[0];
                            clients[port].RemoveMsg(msg);
                            switch(type)
                            {
                                case "new room":
                                    int creatorport = int.Parse(response[1]);
                                    string roomname = response[2];
                                    int rows = int.Parse(response[3]);
                                    int cols = int.Parse(response[4]);
                                    clients[creatorport].bWriter= "room token&" + roomid; //request to server to tell room is token
                                    _ٌRoom roomobj=new _ٌRoom(creatorport,roomname,rows,cols);
                                    rooms.Add(roomid, roomobj);
                                    foreach (int index in clients.Keys.ToList())
                                    {
                                        clients[index].bWriter = "room&" + roomid + "&" + roomobj.Name + "&" + roomobj.num_of_players(); //request to server roomid & name & num of players
                                    }
                                    roomid++;
                                    break;
                                case "join room confirm":
                                    int _roomid = int.Parse(response[1]);
                                    int player2_id = int.Parse(response[2]);
                                    int owner_id = rooms[_roomid].Owner;
                                    clients[owner_id].bWriter = "new player&" + _roomid + "&" + player2_id + "&" + clients[player2_id].Name;
                                    break;

                                    case "join room reply":
                                    int _room_id = int.Parse(response[1]);
                                    int player2= int.Parse(response[2]);
                                    rooms[_room_id].AddPlayer(player2);
                                    clients[player2].bWriter = "join room accepted&" + _room_id + "&" + clients[rooms[_room_id].Player1].Name + "&" + rooms[_room_id].Row + "&" + rooms[_room_id].Col;
                                    foreach (int index in clients.Keys.ToList())// send to all client thats room is full 2/2
                                    {
                                        clients[index].bWriter = "Change Room Capacity&" + _room_id;//room id
                                    }
                                    break;
                                case "Button Clicked":
                                    int room_id_btn_clck = int.Parse(response[1]);
                                    string player_clck_type = response[2];
                                    int x = int.Parse(response[3]);
                                    int y = int.Parse(response[4]);

                                    int c = int.Parse(response[5]);
                                    string mycolor = response[6];
                                    if (player_clck_type.Contains("Player 1"))
                                    {
                                        clients[rooms[room_id_btn_clck].Player2].bWriter = "BButton&" + x + "&" + y + "&" + c + "&true" + "&" + mycolor;
                                        rooms[room_id_btn_clck].Addmove("BButton&" + x + "&" + y + "&" + c + "&false" + "&" + mycolor);
                                    }
                                    else if (player_clck_type.Contains("Player 2"))
                                    {
                                        clients[rooms[room_id_btn_clck].Player1].bWriter = "BButton&" + x + "&" + y + "&" + c + "&true" + "&" + mycolor;
                                        rooms[room_id_btn_clck].Addmove("BButton&" + x + "&" + y + "&" + c + "&false" + "&" + mycolor);
                                    }
                                    foreach (int index in rooms[room_id_btn_clck].Watchers)
                                    {
                                        clients[index].bWriter = "BButton&" + x + "&" + y + "&" + c + "&false" + "&" + mycolor;
                                    }


                                    break;

                                case "watch room":
                                    int watcherid = int.Parse(response[1]);
                                    int watchroomid = int.Parse(response[2]);
                                    rooms[watchroomid].AddWatcher(watcherid);

                                    clients[watcherid].bWriter = "watch room token&" + "##" + "&" + rooms[watcherid].Row + "&" + rooms[watcherid].Col;
                                    foreach (var item in rooms[watchroomid].Moves)
                                    {
                                        clients[watcherid].bWriter = item;
                                    }
                                    break;
                                case "Win Game":
                                    int winroomid = int.Parse(response[1]);
                                    string winplayer = response[2];

                                    break;
                                case "Play Again":
                                    int sender = int.Parse(response[1]);
                                    int senderRoomID = int.Parse(response[2]);
                                    string choice = response[3];
                                    int otherPlayer;
                                    if (sender == rooms[senderRoomID].Owner)
                                    {
                                        otherPlayer = rooms[senderRoomID].Player2;
                                        if (choice == "No")
                                        {
                                            clients[otherPlayer].bWriter = "Play Again&";
                                            foreach (int index in clients.Keys.ToList())// send to all client thats room is  1/2 again
                                            {
                                                clients[index].bWriter = "Reduce Room Capacity&" + senderRoomID;//room id
                                            }

                                        }
                                    }
                                    else
                                    {
                                        otherPlayer = rooms[senderRoomID].Owner;
                                        if (choice == "No")
                                        {
                                            clients[sender].bWriter = "Play Again&";
                                            foreach (int index in clients.Keys.ToList())// send to all client thats room is  1/2 again
                                            {
                                                clients[index].bWriter = "Reduce Room Capacity&" + senderRoomID;//room id
                                            }

                                        }

                                    }
                                    break;


                                case "chat":

                                    string message = response[1];
                                    int player = int.Parse(response[2]);
                                    int room = int.Parse(response[3]);

                                    if (rooms[room].Owner==player)

                                    {
                                        int otherplayer = rooms[room].Player2;
                                        clients[otherplayer].bWriter = "chat&" + message + "&" + clients[player].Name;
                                    }
                                    else
                                    {
                                        int otherplayer = rooms[room].Owner;
                                        clients[otherplayer].bWriter = "chat&"+message + "&" + clients[player].Name;
                                    }
                                    break;

                                case "score":
                                    int roomName = int.Parse(response[5]);
                                    StreamWriter writer = File.CreateText(rooms[roomName].Name+".txt");
                                    writer.WriteLine(response[3]);
                                    writer.WriteLine(response[1]);
                                    writer.Write(writer.NewLine);
                                    writer.WriteLine(response[4]);
                                    writer.WriteLine(response[2]);
                                    writer.Write(writer.NewLine);
                                    writer.Write("the date of the game");
                                    writer.Write(DateTime.Now.ToString());
                                    writer.Close();
                                    break;

                                default:
                                    MessageBox.Show(response[0]);
                                    break;
                            }

                        }
                    }
                }
                catch (Exception ex) {
                    MessageBox.Show("There is error happened while reading data.\n" + ex.Message, "Error msg!",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.ExitThread();
                }
            }
        }


    }
}
