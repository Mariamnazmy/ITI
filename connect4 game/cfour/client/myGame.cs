using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace client
{
    public partial class myGame : Form
    {
        
        private BinaryWriter bWriter;
        private int room_id;
        private int player_id;
       public string player_type;
        private Color mycolor;
        private Color mycolor1;
        private Color mycolor2;
        private string player_no;
        int wins = 0;
        int loses = 0;
        int x;
        int y;
        int c;
        bool flag = false;
        SolidBrush Ellipse_brush;
        Color Ellipse_Color;
        int[] ys;
        int[,] colors;
        int[] row;
        int noRow,noCol;
        int boardWidth;
        int boardHeight;
        List<saved_data> data;
        public myGame()
        {
            InitializeComponent();
        }

        public string Change_Label { set { current_player_txt.Text = value; } }
        public bool btn1 { set { button1.Enabled = value; } }
        public bool btn2 { set { button2.Enabled = value; } }
        public bool btn3 { set { button3.Enabled = value; } }
        public bool btn4 { set { button4.Enabled = value; } }
        public bool btn5 { set { button5.Enabled = value; } }
        public bool btn6 { set { button6.Enabled = value; } }
        public bool btn7 { set { button7.Enabled = value; } }
        public bool btn8 { set { button9.Enabled = value; } }
        public bool btn9 { set { button10.Enabled = value; } }
        public string other_player_name { set { other_player_txt.Text = value; } }

        public int x_cord
        {
            set
            {
                x = value;
             
            }
        }

        public int y_cord
        {
            set
            {
               
                y = value;

          
            }
        }


        public int c_cord
        {
            set
            {

                c = value;


            }
        }
        public Color color_
        {
            set
            {

                mycolor = value;

                draw_click(x, y, c,mycolor);
                Checklose();

            }
        }


        public myGame(int rows,int col,int Room_id, int Player_id, string Player_Type, BinaryWriter bWriter,Color mycolor)
        {
            InitializeComponent();

            this.room_id = Room_id;
            this.player_id = Player_id;
            this.bWriter = bWriter;
            this.noRow = rows;
            this.noCol= col;
            this.player_type = Player_Type;
            player1_txt.Text = player_type;
            Ellipse_Color = Color.White;

            if (noRow == 6 && noCol == 7)
            {
                row = new int[] { 0, 0, 0, 0, 0, 0, 0 };
                ys = new int[] { 460, 460, 460, 460, 460, 460, 460 };
                boardWidth = 550;
                boardHeight = 440;
                button9.Visible=false;
                button10.Visible=false;
            }
            else if (noRow == 7 && noCol == 8)
            {
                row = new int[] { 0, 0, 0, 0, 0, 0, 0, 0 };
                ys = new int[] { 490, 490, 490, 490, 490, 490, 490,490 };
                boardWidth = 640;
                boardHeight = 490;
                button10.Visible=false;
            }
            else
            {
                row = new int[] { 0, 0, 0, 0, 0, 0, 0 ,0 ,0};
                ys = new int[] { 500, 500, 500, 500, 500, 500, 500,500,500 };
                boardWidth = 720;
                boardHeight = 510;
            }


            data = new List<saved_data>();


            if (player_type.Contains("Player 1:"))
            {
                this.mycolor1 = mycolor;
            }
            if (player_type.Contains("Player 2:"))
            {
                mycolor2 = mycolor;
            }

            colors = new int[noRow, noCol];
            for (int i = 0; i < noRow; i++)
            {
                for (int j = 0; j < noCol; j++)
                {
                    colors[i, j] = 0;

                }

            }
          

            if (player_type.Contains("Watcher:"))
            {
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
                button6.Enabled = false;
                button7.Enabled = false;
                button9.Enabled = false;
                button10.Enabled = false;
                other_player_txt.Text = "";
                current_player_txt.Text = "";

                
            }
            
            
        }
        public void reset()
        {
            
            

            if (noRow == 6 && noCol == 7)
            {
                ys = new int[] { 450, 450, 450, 450, 450, 450, 450 };
                row = new int[] { 0, 0, 0, 0, 0, 0, 0 };
            }
            else if (noRow == 7 && noCol == 8)
            {
                row = new int[] { 0, 0, 0, 0, 0, 0, 0 , 0};
                ys = new int[] { 480, 480, 480, 480, 480, 480, 480,480 };
            }
            else
            {
                row = new int[] { 0, 0, 0, 0, 0, 0, 0 ,0,0 };
                ys = new int[] { 500, 500, 500, 500, 500, 500, 500 ,500,500};
            }
            for (int i = 0; i < noRow; i++)
            {
                for (int j = 0; j < noCol; j++)
                {
                    colors[i, j] = 0;

                }

            }
            Graphics g = this.CreateGraphics();
            Ellipse_brush = new SolidBrush(Color.White);
            redraw();
            if (noRow == 6 && noCol == 7)
            {
                ys = new int[] { 450, 450, 450, 450, 450, 450, 450 };
                row = new int[] { 0, 0, 0, 0, 0, 0, 0 };
            }
            else if (noRow == 7 && noCol == 8)
            {
                row = new int[] { 0, 0, 0, 0, 0, 0, 0, 0 };
                ys = new int[] { 480, 480, 480, 480, 480, 480, 480, 480 };
            }
            else
            {
                row = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                ys = new int[] { 500, 500, 500, 500, 500, 500, 500, 500, 500 };
            }
            flag = false;
        }
        public void redraw()
        {
            Graphics g = this.CreateGraphics();
            Ellipse_brush = new SolidBrush(Color.White);
            data.Clear();
            Ellipse_brush = new SolidBrush(Ellipse_Color);
            Pen blackPen = new Pen(Color.Black, 3);
            SolidBrush blueBrush = new SolidBrush(Color.DarkBlue);
            Ellipse_brush = new SolidBrush(Ellipse_Color);
            g.FillRectangle(blueBrush, 5, 75, boardWidth, boardHeight);
            int x = 10;
            int y = 90;
            int width = 50;
            int height = 50;
            for (int i = 0; i < noCol; i++)
            {
                for (int j = 0; j < noRow; j++)
                {

                    g.FillEllipse(new SolidBrush(Color.White), x, y, width, height);
                    y += width + 20;
                }
                y = 90;
                x += height + 30;

            }

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Ellipse_brush = new SolidBrush(Ellipse_Color);
            Pen blackPen = new Pen(Color.Black, 3);
            SolidBrush blueBrush = new SolidBrush(Color.DarkBlue);
            Ellipse_brush = new SolidBrush(Ellipse_Color);
            e.Graphics.FillRectangle(blueBrush, 5, 75, boardWidth, boardHeight);
            int x = 10;
            int y = 90;
            int width = 50;
            int height = 50;
            for (int i = 0; i < noCol; i++)
            {
                for (int j = 0; j < noRow; j++)
                {

                    e.Graphics.FillEllipse(new SolidBrush(Color.White), x, y, width, height);
                    y += width + 20;
                }
                y = 90;
                x += height + 30;

            }
            foreach (var item in data)
            {
                draw(item.xx, item.yy, item.color);
                

            }

        }
        //method is called whenever a player makes a move, and checks to see if the game has ended in a draw (if all the spaces on the board have been filled with moves). If this is the case, it displays a message box with the message "Draw" and calls the reset method, which resets the game board.
        public void draw_player()
        {
            int c = 0;
            for (int i = 0; i < noRow; i++)
            {
                for (int j = 0; j < noCol; j++)
                {
                    if (colors[i, j] == 0) { c++; }

                }

            }
            if (c == 0)
            {
                MessageBox.Show("Draw");
               reset();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Console.Beep();
            x = 0;
            y = 10;
            c = 0;
            player_no = player_type.Split(':')[0];
            if (player_type.Contains("Player 2:"))
            {

                draw_click(x, y, c, mycolor2);
                

                bWriter.Write("Button Clicked&" + room_id + "&" + player_type + "&" + x + "&" + y + "&" + c + "&" + mycolor2.ToArgb());
            }
            if (player_type.Contains("Player 1:"))
            {
                draw_click(x, y, c, mycolor1);


                bWriter.Write("Button Clicked&" + room_id + "&" + player_type + "&" + x + "&" + y + "&" + c + "&" + mycolor1.ToArgb());
            }
            disable();
            checK_win();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            Console.Beep();
            x = 1;
            y = 90;
            c = 1;
            disable();
            player_no = player_type.Split(':')[0];
            if (player_type.Contains("Player 2:"))
            {
                draw_click(x, y, c, mycolor2);                
                bWriter.Write("Button Clicked&" + room_id + "&" + player_type + "&" + x + "&" + y + "&" + c + "&" + mycolor2.ToArgb());
            }
            if (player_type.Contains("Player 1:"))
            {
                draw_click(x, y, c, mycolor1);                
                bWriter.Write("Button Clicked&" + room_id + "&" + player_type + "&" + x + "&" + y + "&" + c + "&" + mycolor1.ToArgb());
            }
            checK_win();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Console.Beep();
            x = 2;
            y = 170;
            c = 2;
            disable();
            player_no = player_type.Split(':')[0];
            if (player_type.Contains("Player 2:"))
            {


                
                draw_click(x, y, c, mycolor2);
               
                bWriter.Write("Button Clicked&" + room_id + "&" + player_type + "&" + x + "&" + y + "&" + c + "&" + mycolor2.ToArgb());
            }
            if (player_type.Contains("Player 1:"))
            {
                
                draw_click(x, y, c, mycolor1);

                bWriter.Write("Button Clicked&" + room_id + "&" + player_type + "&" + x + "&" + y + "&" + c + "&" + mycolor1.ToArgb());
            }
            checK_win();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Console.Beep();
            x = 3;
            y = 250;
            c = 3;
            disable();
            player_no = player_type.Split(':')[0];

            if (player_type.Contains("Player 2:"))
            {
                draw_click(x, y, c, mycolor2);
                bWriter.Write("Button Clicked&" + room_id + "&" + player_type + "&" + x + "&" + y + "&" + c + "&" + mycolor2.ToArgb());
            }
            if (player_type.Contains("Player 1:"))
            {
                draw_click(x, y, c, mycolor1);
                bWriter.Write("Button Clicked&" + room_id + "&" + player_type + "&" + x + "&" + y + "&" + c + "&" + mycolor1.ToArgb());
            }
            checK_win();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Console.Beep();
            x = 4;
            y = 330;
            c = 4;
            disable();
            player_no = player_type.Split(':')[0];

            if (player_type.Contains("Player 2:"))
            {
                draw_click(x, y, c, mycolor2);

                bWriter.Write("Button Clicked&" + room_id + "&" + player_type + "&" + x + "&" + y + "&" + c + "&" + mycolor2.ToArgb());
            }
            if (player_type.Contains("Player 1:"))
            {
                draw_click(x, y, c, mycolor1);

                bWriter.Write("Button Clicked&" + room_id + "&" + player_type + "&" + x + "&" + y + "&" + c + "&" + mycolor1.ToArgb());
            }
            checK_win();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Console.Beep();
            x = 5;
            y = 410;
            c = 5;
            disable();
            player_no = player_type.Split(':')[0];

            if (player_type.Contains("Player 2:"))
            {
                draw_click(x, y, c, mycolor2);
                bWriter.Write("Button Clicked&" + room_id + "&" + player_type + "&" + x + "&" + y + "&" + c + "&" + mycolor2.ToArgb());
            }
            if (player_type.Contains("Player 1:"))
            {
                draw_click(x, y, c, mycolor1);
                bWriter.Write("Button Clicked&" + room_id + "&" + player_type + "&" + x + "&" + y + "&" + c + "&" + mycolor1.ToArgb());
            }
            checK_win();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Console.Beep();
            x = 6;
            y = 490;
            c = 6;
            disable();
            player_no = player_type.Split(':')[0];

            
            if (player_type.Contains("Player 2:"))
            {
                draw_click(x, y, c, mycolor2);
                bWriter.Write("Button Clicked&" + room_id + "&" + player_type + "&" + x + "&" + y + "&" + c + "&" + mycolor2.ToArgb());
            }
            if (player_type.Contains("Player 1:"))
            {
                draw_click(x, y, c, mycolor1);
                bWriter.Write("Button Clicked&" + room_id + "&" + player_type + "&" + x + "&" + y + "&" + c + "&" + mycolor1.ToArgb());
            }
            checK_win();
        }
        //Redraw move on the game board, and a message is sent to the other player using the bWriter object to update their game board
        public void draw_click(int n, int x, int col ,Color mycolor_)
        {
           

            Graphics g = this.CreateGraphics();
            
            if (ys[n] >= 80)
            {
                if (flag == false)
                {
                    Ellipse_Color = mycolor_;
                    flag = true;
                    colors[row[col], col] = 1;
                    row[col]++;

                }
                else
                {
                    Ellipse_Color = mycolor_;
                    flag = false;
                    colors[row[col], col] = 2;
                    row[col]++;
                    Invalidate();
                }
                saved_data dd = new saved_data(){
                    xx = ys[n],
                    yy = x,
                    color = Ellipse_Color,
                };
                data.Add(dd);
                Invalidate();
                
              

                ys[n] = ys[n] - 70;
            }
            Invalidate();
            draw_player();


        }

        public void disable()
        {

            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;
            button9.Enabled = false;
            button10.Enabled = false;
        }

        public void checK_win()
        {
            int start = 0;

            //Horizontal Win
            for (int i = 0; i <noRow; i++)
            {
                for (start = 0; start <= noCol-4; start++)
                {
                    if ((colors[i, start] == colors[i, start + 1] && colors[i, start + 1] == colors[i, start + 2] && colors[i, start + 2] == colors[i, start + 3]) && (colors[i, start] == 1))
                    {

                        //bWriter.Write("Win Game&" + room_id + "&" + player_type);
                        wins++;
                        DialogResult ConfirmResult = MessageBox.Show("You win Congratulation,Do u want to play again !!!!", "Play Again!", MessageBoxButtons.YesNo);
                        if (ConfirmResult == DialogResult.Yes)
                        {
                            reset();
                            bWriter.Write("Play Again&" + player_id + "&" + room_id + "&" + "Yes");
                        }
                        else
                        {
                            reset();
                            bWriter.Write("Play Again&" + player_id + "&" + room_id + "&" + "No");
                        }

                        //disable();


                    }
                    else if ((colors[i, start] == colors[i, start + 1] && colors[i, start + 1] == colors[i, start + 2] && colors[i, start + 2] == colors[i, start + 3]) && (colors[i, start] == 2))
                    {
                        //bWriter.Write("Win Game&" + room_id + "&" + player_type);
                        wins++;
                        DialogResult ConfirmResult = MessageBox.Show("You win Congratulation,Do u want to play again !!!!", "Play Again!", MessageBoxButtons.YesNo);
                        if (ConfirmResult == DialogResult.Yes)
                        {
                            reset();
                            bWriter.Write("Play Again&" + player_id + "&" + room_id + "&" + "Yes");
                        }
                        else
                        {
                            reset();
                            bWriter.Write("Play Again&" + player_id + "&" + room_id + "&" + "No");
                        }
                        //disable();
                    }
                }
            }
            for (int i = 0; i < noRow+1; i++)
            {
                if (noCol == 9)
                {
                    for (start = 0; start <= noCol - 6; start++)
                    {
                        if ((colors[start, i] == colors[start + 1, i] && colors[start + 1, i] == colors[start + 2, i] && colors[start + 2, i] == colors[start + 3, i]) && (colors[start, i] == 1))
                        {
                            //bWriter.Write("Win Game&" + room_id + "&" + player_type);
                            wins++;
                            DialogResult ConfirmResult = MessageBox.Show("You win Congratulation,Do u want to play again !!!!", "Play Again!", MessageBoxButtons.YesNo);
                            if (ConfirmResult == DialogResult.Yes)
                            {
                                reset();
                                bWriter.Write("Play Again&" + player_id + "&" + room_id + "&" + "Yes");
                            }
                            else
                            {
                                reset();
                                bWriter.Write("Play Again&" + player_id + "&" + room_id + "&" + "No");
                            }
                            //disable();
                            //break;
                        }
                        else if ((colors[start, i] == colors[start + 1, i] && colors[start + 1, i] == colors[start + 2, i] && colors[start + 2, i] == colors[start + 3, i]) && (colors[start, i] == 2))
                        {
                            //bWriter.Write("Win Game&" + room_id + "&" + player_type);
                            wins++;
                            DialogResult ConfirmResult = MessageBox.Show("You win Congratulation,Do u want to play again !!!!", "Play Again!", MessageBoxButtons.YesNo);
                            if (ConfirmResult == DialogResult.Yes)
                            {
                                reset();
                                bWriter.Write("Play Again&" + player_id + "&" + room_id + "&" + "Yes");
                            }
                            else
                            {
                                reset();
                                bWriter.Write("Play Again&" + player_id + "&" + room_id + "&" + "No");
                            }
                            //disable();
                        }
                    }
                }
                else
                {
                    for (start = 0; start <= noCol - 5; start++)
                    {
                        if ((colors[start, i] == colors[start + 1, i] && colors[start + 1, i] == colors[start + 2, i] && colors[start + 2, i] == colors[start + 3, i]) && (colors[start, i] == 1))
                        {
                            //bWriter.Write("Win Game&" + room_id + "&" + player_type);
                            wins++;
                            DialogResult ConfirmResult = MessageBox.Show("You win Congratulation,Do u want to play again !!!!", "Play Again!", MessageBoxButtons.YesNo);
                            if (ConfirmResult == DialogResult.Yes)
                            {
                                reset();
                                bWriter.Write("Play Again&" + player_id + "&" + room_id + "&" + "Yes");
                            }
                            else
                            {
                                reset();
                                bWriter.Write("Play Again&" + player_id + "&" + room_id + "&" + "No");
                            }
                            //disable();
                            //break;
                        }
                        else if ((colors[start, i] == colors[start + 1, i] && colors[start + 1, i] == colors[start + 2, i] && colors[start + 2, i] == colors[start + 3, i]) && (colors[start, i] == 2))
                        {
                            //bWriter.Write("Win Game&" + room_id + "&" + player_type);
                            wins++;
                            DialogResult ConfirmResult = MessageBox.Show("You win Congratulation,Do u want to play again !!!!", "Play Again!", MessageBoxButtons.YesNo);
                            if (ConfirmResult == DialogResult.Yes)
                            {
                                reset();
                                bWriter.Write("Play Again&" + player_id + "&" + room_id + "&" + "Yes");
                            }
                            else
                            {
                                reset();
                                bWriter.Write("Play Again&" + player_id + "&" + room_id + "&" + "No");
                            }
                            //disable();
                        }
                    }
                }
                
            }

            //Diagonal Win - "/"
            for (int i = 3; i < noRow; i++)
            {
                for (start = 0; start <= noCol-4; start++)
                {
                    if ((colors[i, start] == colors[i - 1, start + 1] && colors[i - 1, start + 1] == colors[i - 2, start + 2] && colors[i - 2, start + 2] == colors[i - 3, start + 3]) && (colors[i, start] == 1))
                    {
                        //bWriter.Write("Win Game&" + room_id + "&" + player_type);
                        wins++;
                        DialogResult ConfirmResult = MessageBox.Show("You win Congratulation,Do u want to play again !!!!", "Play Again!", MessageBoxButtons.YesNo);
                        if (ConfirmResult == DialogResult.Yes)
                        {
                            reset();
                            bWriter.Write("Play Again&" + player_id + "&" + room_id + "&" + "Yes");
                        }
                        else
                        {
                            reset();
                            bWriter.Write("Play Again&" + player_id + "&" + room_id + "&" + "No");
                        }
                        disable();
                        //  break;
                    }
                    else if ((colors[i, start] == colors[i - 1, start + 1] && colors[i - 1, start + 1] == colors[i - 2, start + 2] && colors[i - 2, start + 2] == colors[i - 3, start + 3]) && (colors[i, start] == 2))
                    {
                        //bWriter.Write("Win Game&" + room_id + "&" + player_type);
                        wins++;
                        DialogResult ConfirmResult = MessageBox.Show("You win Congratulation,Do u want to play again !!!!", "Play Again!", MessageBoxButtons.YesNo);
                        if (ConfirmResult == DialogResult.Yes)
                        {
                            reset();
                            bWriter.Write("Play Again&" + player_id + "&" + room_id + "&" + "Yes");
                        }
                        else
                        {
                            reset();
                            bWriter.Write("Play Again&" + player_id + "&" + room_id + "&" + "No");
                        }
                        disable();
                    }
                }
            }

            //Diagonal Win -"\"
            for (int i = 0; i < noRow-3; i++)
            {
                for (start = 0; start < noCol-3; start++)
                {
                    if ((colors[i, start] == colors[i + 1, start + 1] && colors[i + 1, start + 1] == colors[i + 2, start + 2] && colors[i + 2, start + 2] == colors[i + 3, start + 3]) && (colors[i, start] == 1))
                    {
                        //bWriter.Write("Win Game&" + room_id + "&" + player_type);
                        wins++;
                        DialogResult ConfirmResult = MessageBox.Show("You win Congratulation,Do u want to play again !!!!", "Play Again!", MessageBoxButtons.YesNo);
                        if (ConfirmResult == DialogResult.Yes)
                        {
                            reset();
                            bWriter.Write("Play Again&" + player_id + "&" + room_id + "&" + "Yes");
                        }
                        else
                        {
                            reset();
                            bWriter.Write("Play Again&" + player_id + "&" + room_id + "&" + "No");
                        }
                        disable();
                        // break;
                    }
                    else if ((colors[i, start] == colors[i + 1, start + 1] && colors[i + 1, start + 1] == colors[i + 2, start + 2] && colors[i + 2, start + 2] == colors[i + 3, start + 3]) && (colors[i, start] == 2))
                    {
                        //bWriter.Write("Win Game&" + room_id + "&" + player_type);
                        wins++;
                        DialogResult ConfirmResult = MessageBox.Show("You win Congratulation,Do u want to play again !!!!", "Play Again!", MessageBoxButtons.YesNo);
                        if (ConfirmResult == DialogResult.Yes)
                        {
                            reset();
                            bWriter.Write("Play Again&" + player_id + "&" + room_id + "&" + "Yes");
                        }
                        else
                        {
                            reset();
                            bWriter.Write("Play Again&" + player_id + "&" + room_id + "&" + "No");
                        }
                        disable();
                    }
                }
            }
        }
        public void draw(int x, int y, Color col)
        {
            Graphics g = this.CreateGraphics();

            Ellipse_Color = col;
            Ellipse_brush = new SolidBrush(Ellipse_Color);
            if (noRow == 6 && noCol == 7)
            {
                g.FillEllipse(Ellipse_brush, y, x-20 , 50, 50);
            }
            else if(noRow==7&& noCol == 8) 
            {
                g.FillEllipse(Ellipse_brush, y, x + 20, 50, 50);
            }
            else
            {
                g.FillEllipse(Ellipse_brush, y, x + 10, 50, 50);
            }
            
            
        }

        public void Checklose()
        {
            
           
            int start = 0;

            //Horizontal Win
            for (int i = 0; i < noRow; i++)
            {
                for (start = 0; start <= noCol - 4; start++)
                {
                    if ((colors[i, start] == colors[i, start + 1] && colors[i, start + 1] == colors[i, start + 2] && colors[i, start + 2] == colors[i, start + 3]) && (colors[i, start] == 1))
                    {

                        //bWriter.Write("Win Game&" + room_id + "&" + player_type);
                        wins++;
                        DialogResult ConfirmResult = MessageBox.Show("You lose, Do you want to play again !!!!", "You lose !!!!", MessageBoxButtons.YesNo);
                        if (ConfirmResult == DialogResult.Yes)
                        {
                            reset();
                            bWriter.Write("Play Again&" + player_id + "&" + room_id + "&" + "Yes");
                        }
                        else
                        {
                            reset();
                            bWriter.Write("Play Again&" + player_id + "&" + room_id + "&" + "No");
                        }

                        //disable();


                    }
                    else if ((colors[i, start] == colors[i, start + 1] && colors[i, start + 1] == colors[i, start + 2] && colors[i, start + 2] == colors[i, start + 3]) && (colors[i, start] == 2))
                    {
                        //bWriter.Write("Win Game&" + room_id + "&" + player_type);
                        wins++;
                        DialogResult ConfirmResult = MessageBox.Show("You lose, Do you want to play again !!!!", "You lose !!!!", MessageBoxButtons.YesNo);
                        if (ConfirmResult == DialogResult.Yes)
                        {
                            reset();
                            bWriter.Write("Play Again&" + player_id + "&" + room_id + "&" + "Yes");
                        }
                        else
                        {
                            reset();
                            bWriter.Write("Play Again&" + player_id + "&" + room_id + "&" + "No");
                        }
                        //disable();
                    }
                }
            }
            for (int i = 0; i < noRow + 1; i++)
            {
                if (noCol == 9)
                {
                    for (start = 0; start <= noCol - 6; start++)
                    {
                        if ((colors[start, i] == colors[start + 1, i] && colors[start + 1, i] == colors[start + 2, i] && colors[start + 2, i] == colors[start + 3, i]) && (colors[start, i] == 1))
                        {
                            //bWriter.Write("Win Game&" + room_id + "&" + player_type);
                            wins++;
                            DialogResult ConfirmResult = MessageBox.Show("You lose, Do you want to play again !!!!", "You lose !!!!", MessageBoxButtons.YesNo);
                            if (ConfirmResult == DialogResult.Yes)
                            {
                                reset();
                                bWriter.Write("Play Again&" + player_id + "&" + room_id + "&" + "Yes");
                            }
                            else
                            {
                                reset();
                                bWriter.Write("Play Again&" + player_id + "&" + room_id + "&" + "No");
                            }
                            //disable();
                            //break;
                        }
                        else if ((colors[start, i] == colors[start + 1, i] && colors[start + 1, i] == colors[start + 2, i] && colors[start + 2, i] == colors[start + 3, i]) && (colors[start, i] == 2))
                        {
                            //bWriter.Write("Win Game&" + room_id + "&" + player_type);
                            wins++;
                            DialogResult ConfirmResult = MessageBox.Show("You lose, Do you want to play again !!!!", "You lose !!!!", MessageBoxButtons.YesNo);
                            if (ConfirmResult == DialogResult.Yes)
                            {
                                reset();
                                bWriter.Write("Play Again&" + player_id + "&" + room_id + "&" + "Yes");
                            }
                            else
                            {
                                reset();
                                bWriter.Write("Play Again&" + player_id + "&" + room_id + "&" + "No");
                            }
                            //disable();
                        }
                    }
                }
                else
                {
                    for (start = 0; start <= noCol - 5; start++)
                    {
                        if ((colors[start, i] == colors[start + 1, i] && colors[start + 1, i] == colors[start + 2, i] && colors[start + 2, i] == colors[start + 3, i]) && (colors[start, i] == 1))
                        {
                            //bWriter.Write("Win Game&" + room_id + "&" + player_type);
                            wins++;
                            DialogResult ConfirmResult = MessageBox.Show("You lose, Do you want to play again !!!!", "You lose !!!!", MessageBoxButtons.YesNo);
                            if (ConfirmResult == DialogResult.Yes)
                            {
                                reset();
                                bWriter.Write("Play Again&" + player_id + "&" + room_id + "&" + "Yes");
                            }
                            else
                            {
                                reset();
                                bWriter.Write("Play Again&" + player_id + "&" + room_id + "&" + "No");
                            }
                            //disable();
                            //break;
                        }
                        else if ((colors[start, i] == colors[start + 1, i] && colors[start + 1, i] == colors[start + 2, i] && colors[start + 2, i] == colors[start + 3, i]) && (colors[start, i] == 2))
                        {
                            //bWriter.Write("Win Game&" + room_id + "&" + player_type);
                            wins++;
                            DialogResult ConfirmResult = MessageBox.Show("You lose, Do you want to play again !!!!", "You lose !!!!", MessageBoxButtons.YesNo);
                            if (ConfirmResult == DialogResult.Yes)
                            {
                                reset();
                                bWriter.Write("Play Again&" + player_id + "&" + room_id + "&" + "Yes");
                            }
                            else
                            {
                                reset();
                                bWriter.Write("Play Again&" + player_id + "&" + room_id + "&" + "No");
                            }
                            //disable();
                        }
                    }
                }

            }

            //Diagonal Win - "/"
            for (int i = 3; i < noRow; i++)
            {
                for (start = 0; start <= noCol - 4; start++)
                {
                    if ((colors[i, start] == colors[i - 1, start + 1] && colors[i - 1, start + 1] == colors[i - 2, start + 2] && colors[i - 2, start + 2] == colors[i - 3, start + 3]) && (colors[i, start] == 1))
                    {
                        //bWriter.Write("Win Game&" + room_id + "&" + player_type);
                        wins++;
                        DialogResult ConfirmResult = MessageBox.Show("You lose, Do you want to play again !!!!", "You lose !!!!", MessageBoxButtons.YesNo);
                        if (ConfirmResult == DialogResult.Yes)
                        {
                            reset();
                            bWriter.Write("Play Again&" + player_id + "&" + room_id + "&" + "Yes");
                        }
                        else
                        {
                            reset();
                            bWriter.Write("Play Again&" + player_id + "&" + room_id + "&" + "No");
                        }
                        disable();
                        //  break;
                    }
                    else if ((colors[i, start] == colors[i - 1, start + 1] && colors[i - 1, start + 1] == colors[i - 2, start + 2] && colors[i - 2, start + 2] == colors[i - 3, start + 3]) && (colors[i, start] == 2))
                    {
                        //bWriter.Write("Win Game&" + room_id + "&" + player_type);
                        wins++;
                        DialogResult ConfirmResult = MessageBox.Show("You lose, Do you want to play again !!!!", "You lose !!!!", MessageBoxButtons.YesNo);
                        if (ConfirmResult == DialogResult.Yes)
                        {
                            reset();
                            bWriter.Write("Play Again&" + player_id + "&" + room_id + "&" + "Yes");
                        }
                        else
                        {
                            reset();
                            bWriter.Write("Play Again&" + player_id + "&" + room_id + "&" + "No");
                        }
                        disable();
                    }
                }
            }

            //Diagonal Win -"\"
            for (int i = 0; i < noRow - 3; i++)
            {
                for (start = 0; start < noCol - 3; start++)
                {
                    if ((colors[i, start] == colors[i + 1, start + 1] && colors[i + 1, start + 1] == colors[i + 2, start + 2] && colors[i + 2, start + 2] == colors[i + 3, start + 3]) && (colors[i, start] == 1))
                    {
                        //bWriter.Write("Win Game&" + room_id + "&" + player_type);
                        wins++;
                        DialogResult ConfirmResult = MessageBox.Show("You lose, Do you want to play again !!!!", "You lose !!!!", MessageBoxButtons.YesNo);
                        if (ConfirmResult == DialogResult.Yes)
                        {
                            reset();
                            bWriter.Write("Play Again&" + player_id + "&" + room_id + "&" + "Yes");
                        }
                        else
                        {
                            reset();
                            bWriter.Write("Play Again&" + player_id + "&" + room_id + "&" + "No");
                        }
                        disable();
                        // break;
                    }
                    else if ((colors[i, start] == colors[i + 1, start + 1] && colors[i + 1, start + 1] == colors[i + 2, start + 2] && colors[i + 2, start + 2] == colors[i + 3, start + 3]) && (colors[i, start] == 2))
                    {
                        //bWriter.Write("Win Game&" + room_id + "&" + player_type);
                        wins++;
                        DialogResult ConfirmResult = MessageBox.Show("You lose, Do you want to play again !!!!", "You lose !!!!", MessageBoxButtons.YesNo);
                        if (ConfirmResult == DialogResult.Yes)  
                        {
                            reset();
                            bWriter.Write("Play Again&" + player_id + "&" + room_id + "&" + "Yes");
                        }
                        else
                        {
                            reset();
                            bWriter.Write("Play Again&" + player_id + "&" + room_id + "&" + "No");
                        }
                        disable();
                    }
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Console.Beep();
            x = 7;
            y = 570;
            c = 7;
            disable();
            player_no = player_type.Split(':')[0];


            if (player_type.Contains("Player 2:"))
            {
                draw_click(x, y, c, mycolor2);
                bWriter.Write("Button Clicked&" + room_id + "&" + player_type + "&" + x + "&" + y + "&" + c + "&" + mycolor2.ToArgb());
            }
            if (player_type.Contains("Player 1:"))
            {
                draw_click(x, y, c, mycolor1);
                bWriter.Write("Button Clicked&" + room_id + "&" + player_type + "&" + x + "&" + y + "&" + c + "&" + mycolor1.ToArgb());
            }
            checK_win();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Console.Beep();
            x = 8;
            y = 650;
            c = 8;
            disable();
            player_no = player_type.Split(':')[0];


            if (player_type.Contains("Player 2:"))
            {
                draw_click(x, y, c, mycolor2);
                bWriter.Write("Button Clicked&" + room_id + "&" + player_type + "&" + x + "&" + y + "&" + c + "&" + mycolor2.ToArgb());
            }
            if (player_type.Contains("Player 1:"))
            {
                draw_click(x, y, c, mycolor1);
                bWriter.Write("Button Clicked&" + room_id + "&" + player_type + "&" + x + "&" + y + "&" + c + "&" + mycolor1.ToArgb());
            }
            checK_win();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string[] condition = player_type.Split(':');

            bWriter.Write("chat&"+textBox1.Text + "&" + player_id + "&" + room_id );
            textBox2.Text += $"{condition[1]} : {textBox1.Text}  {Environment.NewLine} ";
            textBox1.Clear();

        }

       

        private void myGame_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            bWriter.Write("score&" + loses + "&" + wins + "&" + other_player_txt.Text + "&" + player1_txt.Text+"&"+room_id);
  
        }

       
    }
    public class saved_data
    {
        public int xx;
        public int yy;
        public Color color;
       
    }
}
