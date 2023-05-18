using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace client
{
    public delegate void AddToListBoxDelegate(string item1, string item2);
    public partial class Form3 : Form
    {

        private Color change_col;
        public string username;
        string color;
        NetworkStream nStream;
        ColorDialog colorDialog1 = new ColorDialog();
        int rows, columns;
        private int endpoint;
        private BinaryReader Reader;
        private BinaryWriter Writer;
        private Thread thread;
        private string palyer1Name;
        private string w_room_token = null;
        private int temp_room_id=-1;
        bool flag;
        private myGame game2;
        private string palyer1_name;




        public Form3(string username , NetworkStream nStream)
        {
            //CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            this.username = username;
                this.nStream = nStream;
                lblWelcome.Text = "Welcome to Connect4 Game, " + username + " !";
                this.Text = username;
                Writer = new BinaryWriter(nStream);
                Writer.Write(username);
                Reader = new BinaryReader(nStream);
                endpoint = int.Parse(Reader.ReadString());
                thread = new Thread(Data_Transferred);
                //thread.Priority = ThreadPriority.Highest;
                thread.IsBackground = true;
                thread.Start();
                createbtn.Enabled = false;
                joinbtn.Enabled = false;
                watchbtn.Enabled = false;
                color = null;
               // temp_room_id = -1;
        }
        ~Form3() { thread.Abort(); }



        private void button1_Click(object sender, EventArgs e)//create room
        {
            Boardsize boardsize = new Boardsize();

            DialogResult res;
            res = boardsize.ShowDialog();
            if (res == DialogResult.OK)
            {
                columns = boardsize.Column;
                rows = boardsize.Row;
            }
            Writer.Write("new room&" + endpoint + "&" + roomname.Text+"&"+rows+"&"+columns);//send to server create room and client port num and room name
            while (temp_room_id == -1) ;

            game2 = new client.myGame(rows,columns,temp_room_id, endpoint, "Player 1: " + username, Writer, change_col);
            temp_room_id = -1;
            game2.btn1 = false;
            game2.btn2 = false;
            game2.btn3 = false;
            game2.btn4 = false;
            game2.btn5 = false;
            game2.btn6 = false;
            game2.btn7 = false;    
            game2.btn8 = false;
            game2.btn9 = false;
            DialogResult DR1 = game2.ShowDialog();
        }

        private void colorbtn_Click(object sender, EventArgs e)
        {

            colorDialog1.ShowDialog();
            change_col = colorDialog1.Color;
            color = change_col.Name;


            if (roomname.Text != string.Empty)
            {
                createbtn.Enabled = true;
            }
            joinbtn.Enabled = true;
            watchbtn.Enabled = true;
        }

        private void roomname_TextChanged(object sender, EventArgs e)
        {
            if (color == null)
            {
                createbtn.Enabled = false;
            }
            else
            {
                createbtn.Enabled = true;
            }

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = ((ListBox)sender).SelectedIndex;
            if (index != -1)
            {

                listBox_rooms.SelectedIndex = index;
                listBox_players.SelectedIndex = index;
                ListBox_ID.SelectedIndex = index;
                watchbtn.Enabled = true;
                if (listBox_players.SelectedItem.ToString() == "2/2")
                    joinbtn.Enabled = false;
                else
                    joinbtn.Enabled = true;
            }
            else
            {
                watchbtn.Enabled = false;
                joinbtn.Enabled = false;
            }
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            thread.Abort();
            Reader.Close();
            Writer.Close();
            nStream.Close();
            Application.ExitThread();
        }

        private void joinbtn_Click(object sender, EventArgs e)
        {
            if (ListBox_ID.SelectedIndex != -1)
            {
                Writer.Write("join room confirm&" + ListBox_ID.SelectedItem.ToString() + "&" + endpoint);//send to server room id and player 2 id (prot number)
                while (temp_room_id == -1) ;
                game2 = new myGame(rows,columns,temp_room_id, endpoint, "Player 2: " + username, Writer, change_col);
                temp_room_id = -1;
                game2.other_player_name = "player 1 : " + palyer1_name;
                game2.Change_Label = "";
                game2.ShowDialog();
                DialogResult DR2 = game2.ShowDialog();
            }
           
        }

        private void watchbtn_Click(object sender, EventArgs e)
        {
            if(ListBox_ID.SelectedIndex != -1)
            {
                int room_id = int.Parse(ListBox_ID.SelectedItem.ToString());
                Writer.Write("watch room&" + endpoint + "&" + room_id);

                while (w_room_token == null) ;
                game2 = new client.myGame(rows, columns, room_id, endpoint, "Watcher: " + username, Writer, change_col);
                w_room_token = null;
                game2.ShowDialog();
            }
        }

        private void Data_Transferred()
        {
            while (true)
            {
                string[] response = Reader.ReadString().Split('&');
                string type = response[0];
                // MessageBox.Show(type.ToString());
                switch (type)
                {
                    case "room": // add room id ,name, num of player per room to listboxs of all clients except room creator
                                 // MessageBox.Show("entered");

                        ListBox_ID.Items.Add(response[1]);

                        listBox_rooms.Items.Add(response[2]);
                        listBox_players.Items.Add(response[3] + "/2");

                        break;
                    case "room token": // room id from server to make creator to open mygame form 

                        temp_room_id = int.Parse(response[1]);
                        break;
                    case "new player": // server send room id and player 2 id and name 

                        DialogResult result = MessageBox.Show(response[3] + " wants to join your room", "Join Confirmation",
                             MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                        if (result == DialogResult.OK)
                        {
                            game2.Change_Label = "";
                            game2.btn1 = false;
                            game2.btn2 = false;
                            game2.btn3 = false;
                            game2.btn4 = false;
                            game2.btn5 = false;
                            game2.btn6 = false;
                            game2.btn7 = false;
                            game2.btn8 = false;
                            game2.btn9 = false;
                            game2.other_player_name = "Player 2 : " + response[3];
                            Writer.Write("join room reply&" + response[1] + "&" + response[2]);// send to server player2 id and room id 

                        }
                        break;

                    case "join room accepted":

                        palyer1_name = response[2];
                        rows = int.Parse(response[3]);
                        columns= int.Parse(response[4]);
                        temp_room_id = int.Parse(response[1]);
                        //MessageBox.Show(response[3] + " , " + response[4]);

                        break;


                    case "BButton":
                        while (game2 == null) ;
                        game2.x_cord = int.Parse(response[1]);
                        game2.y_cord = int.Parse(response[2]);
                        game2.c_cord = int.Parse(response[3]);
                        flag = bool.Parse(response[4]);
                        game2.color_ = Color.FromArgb(int.Parse(response[5]));

                        game2.btn1 = flag;
                        game2.btn2 = flag;
                        game2.btn3 = flag;
                        game2.btn4 = flag;
                        game2.btn5 = flag;
                        game2.btn6 = flag;
                        game2.btn7 = flag;
                        game2.btn8 = flag;
                        game2.btn9 = flag;



                        break;

                    case "Change Room Capacity":
                        int index = ListBox_ID.FindStringExact(response[1]);
                        listBox_players.Items[index] = "2/2";
                        break;
                    case "watch room token":
                       rows= int.Parse(response[2]);
                        columns = int.Parse(response[3]);
                        w_room_token = response[1];

                        break;
                    case "Play Again":
                        game2.Close();
                        MessageBox.Show("Both of you didn't match to play again with each others");
                        game2.Close();
                        break;
                    case "Reduce Room Capacity":
                        int index2 = ListBox_ID.FindStringExact(response[1]);
                        listBox_players.Items[index2] = "1/2";
                        break;

                    case "chat":
                        game2.textBox2.Text += $"{response[2]}  :   {response[1]}  { Environment.NewLine}";
                        break;
                    default:
                        MessageBox.Show(response[0]);
                        break;

                }

            }
        }


    }
}
