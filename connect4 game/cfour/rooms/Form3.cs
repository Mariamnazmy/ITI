using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace rooms
{
    public partial  class Form3 : Form
    {
        public string username;

        public Form3(string username)
        {
            InitializeComponent();
            this.username = username;
            lblWelcome.Text = "Welcome to the Rooms, " + username + "!";
            //LoadRooms();
        }

        /* private void LoadRooms(string username)
         {
                 // TODO: Load list of rooms from a file and add to ListBox control
         }*/
    }

}

