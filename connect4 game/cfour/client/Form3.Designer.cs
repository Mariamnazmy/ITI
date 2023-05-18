namespace client
{
    partial class Form3
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblWelcome = new System.Windows.Forms.Label();
            this.createbtn = new System.Windows.Forms.Button();
            this.listBox_rooms = new System.Windows.Forms.ListBox();
            this.listBox_players = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.roomname = new System.Windows.Forms.TextBox();
            this.watchbtn = new System.Windows.Forms.Button();
            this.joinbtn = new System.Windows.Forms.Button();
            this.colorbtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.ListBox_ID = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Comic Sans MS", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWelcome.Location = new System.Drawing.Point(14, 11);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(88, 25);
            this.lblWelcome.TabIndex = 2;
            this.lblWelcome.Text = "Welcome";
            // 
            // createbtn
            // 
            this.createbtn.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.createbtn.ForeColor = System.Drawing.Color.Blue;
            this.createbtn.Location = new System.Drawing.Point(651, 242);
            this.createbtn.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.createbtn.Name = "createbtn";
            this.createbtn.Size = new System.Drawing.Size(237, 65);
            this.createbtn.TabIndex = 5;
            this.createbtn.Text = "Create Room";
            this.createbtn.UseVisualStyleBackColor = true;
            this.createbtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // listBox_rooms
            // 
            this.listBox_rooms.FormattingEnabled = true;
            this.listBox_rooms.ItemHeight = 20;
            this.listBox_rooms.Location = new System.Drawing.Point(12, 83);
            this.listBox_rooms.Name = "listBox_rooms";
            this.listBox_rooms.Size = new System.Drawing.Size(214, 384);
            this.listBox_rooms.TabIndex = 6;
            this.listBox_rooms.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // listBox_players
            // 
            this.listBox_players.FormattingEnabled = true;
            this.listBox_players.ItemHeight = 20;
            this.listBox_players.Location = new System.Drawing.Point(414, 83);
            this.listBox_players.Name = "listBox_players";
            this.listBox_players.Size = new System.Drawing.Size(208, 384);
            this.listBox_players.TabIndex = 7;
            this.listBox_players.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Comic Sans MS", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Crimson;
            this.label1.Location = new System.Drawing.Point(12, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 31);
            this.label1.TabIndex = 8;
            this.label1.Text = "Rooms!";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Comic Sans MS", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Crimson;
            this.label2.Location = new System.Drawing.Point(409, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 29);
            this.label2.TabIndex = 9;
            this.label2.Text = "Players !";
            // 
            // roomname
            // 
            this.roomname.Location = new System.Drawing.Point(651, 197);
            this.roomname.Name = "roomname";
            this.roomname.Size = new System.Drawing.Size(237, 26);
            this.roomname.TabIndex = 10;
            this.roomname.TextChanged += new System.EventHandler(this.roomname_TextChanged);
            // 
            // watchbtn
            // 
            this.watchbtn.Font = new System.Drawing.Font("Comic Sans MS", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.watchbtn.ForeColor = System.Drawing.Color.Red;
            this.watchbtn.Location = new System.Drawing.Point(651, 411);
            this.watchbtn.Name = "watchbtn";
            this.watchbtn.Size = new System.Drawing.Size(237, 65);
            this.watchbtn.TabIndex = 11;
            this.watchbtn.Text = "Watch Room";
            this.watchbtn.UseVisualStyleBackColor = true;
            this.watchbtn.Click += new System.EventHandler(this.watchbtn_Click);
            // 
            // joinbtn
            // 
            this.joinbtn.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.joinbtn.Font = new System.Drawing.Font("Comic Sans MS", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.joinbtn.Location = new System.Drawing.Point(651, 322);
            this.joinbtn.Name = "joinbtn";
            this.joinbtn.Size = new System.Drawing.Size(237, 65);
            this.joinbtn.TabIndex = 12;
            this.joinbtn.Text = "Join Room";
            this.joinbtn.UseVisualStyleBackColor = false;
            this.joinbtn.Click += new System.EventHandler(this.joinbtn_Click);
            // 
            // colorbtn
            // 
            this.colorbtn.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colorbtn.ForeColor = System.Drawing.Color.RoyalBlue;
            this.colorbtn.Location = new System.Drawing.Point(651, 83);
            this.colorbtn.Name = "colorbtn";
            this.colorbtn.Size = new System.Drawing.Size(237, 65);
            this.colorbtn.TabIndex = 13;
            this.colorbtn.Text = "Choose Color";
            this.colorbtn.UseVisualStyleBackColor = true;
            this.colorbtn.Click += new System.EventHandler(this.colorbtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Comic Sans MS", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Crimson;
            this.label3.Location = new System.Drawing.Point(647, 161);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(148, 23);
            this.label3.TabIndex = 14;
            this.label3.Text = "Enter Room Name";
            // 
            // ListBox_ID
            // 
            this.ListBox_ID.FormattingEnabled = true;
            this.ListBox_ID.ItemHeight = 20;
            this.ListBox_ID.Location = new System.Drawing.Point(255, 83);
            this.ListBox_ID.Name = "ListBox_ID";
            this.ListBox_ID.Size = new System.Drawing.Size(134, 384);
            this.ListBox_ID.TabIndex = 15;
            this.ListBox_ID.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Comic Sans MS", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Crimson;
            this.label4.Location = new System.Drawing.Point(250, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 29);
            this.label4.TabIndex = 16;
            this.label4.Text = "Room ID !";
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(900, 519);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ListBox_ID);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.colorbtn);
            this.Controls.Add(this.joinbtn);
            this.Controls.Add(this.watchbtn);
            this.Controls.Add(this.roomname);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBox_players);
            this.Controls.Add(this.listBox_rooms);
            this.Controls.Add(this.createbtn);
            this.Controls.Add(this.lblWelcome);
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "Form3";
            this.Text = "Form3";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form3_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Button createbtn;
        private System.Windows.Forms.ListBox listBox_rooms;
        private System.Windows.Forms.ListBox listBox_players;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox roomname;
        private System.Windows.Forms.Button watchbtn;
        private System.Windows.Forms.Button joinbtn;
        private System.Windows.Forms.Button colorbtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox ListBox_ID;
        private System.Windows.Forms.Label label4;
    }
}