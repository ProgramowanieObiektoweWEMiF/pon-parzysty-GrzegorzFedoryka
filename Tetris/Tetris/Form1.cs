using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    
    public partial class Form1 : Form
    {
        Timer fresh; 
        Game game;
        Game game2;
        private TimeSpan timee = new TimeSpan();


        int map_cell_width;
        int map_cell_height;

        public Form1()
        {
            InitializeComponent();
            
            
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.BackColor = Color.Black;

            pictureBox2.BorderStyle = BorderStyle.FixedSingle;
            pictureBox2.BackColor = Color.Black;

            game = new Game();
            game2 = new Game();

            map_cell_width = 30;
            map_cell_height = 30;

            //obsługa timera
            KeyDown += new KeyEventHandler(Form1_KeyDown);
            fresh = new Timer();
            fresh.Tick += new System.EventHandler(fresh_Tick);

            fresh.Interval = 100;
            fresh.Enabled = true;

            
        }
        public void NewGame()
        {
            game = new Game();
            game2 = new Game();
        }
        private void fresh_Tick(object sender, EventArgs e)
        {
            Draw();
            timee = timee.Add(TimeSpan.FromMilliseconds(100));

            time.Text = string.Format("{0}:{1}", timee.Minutes, timee.Seconds);

        }

        private void Draw() 
        {
            //liczenie punktów
            AlphaScore.Text = Convert.ToString(game.score);
            BetaScore.Text = Convert.ToString(game2.score);
          
            Font font = new Font("Arial", 16);
            SolidBrush drawBrush = new SolidBrush(Color.White);
            PointF drawPoint = new PointF(380.0F, 170.0F);
            string next = "Next:";

            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(bmp);
            SolidBrush s;

            Bitmap bmp2 = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            Graphics g2 = Graphics.FromImage(bmp2);
            SolidBrush s2;

            //narysuj obramowania planszy
            g.DrawRectangle(Pens.White, map_cell_width * 4 - 3 - 80, map_cell_height * 4 - 3 - 80, map_cell_width * game.board.Width + 3, map_cell_height * game.board.Height + 3);
            g2.DrawRectangle(Pens.White, map_cell_width * 4 - 3 - 80, map_cell_height * 4 - 3 - 80, map_cell_width * game2.board.Width + 3, map_cell_height * game2.board.Height + 3);

            //Napisz "Next"
            g.DrawString(next, font, drawBrush, drawPoint );
            g2.DrawString(next, font, drawBrush, drawPoint);



            for (int i = 4; i < game.board.Height + 4; i++) 
            {
                for (int j = 4; j < game.board.Width + 4; j++)
                {
                    if (game.board.Mod[i, j].p == 1)
                    {
                        s = new SolidBrush(game.board.Mod[i, j].c);
                        g.FillRectangle(s, map_cell_width * j - 80, map_cell_height * i - 80, map_cell_width - 2, map_cell_height - 2);
                    }
                }
            }
            s = new SolidBrush(game.block.color);
            
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (game.block.Mod[i, j] == 1)
                    {
                        if(i + game.block.Position.Y > 3)
                        {
                            
                            g.FillRectangle(s, map_cell_width * (j + game.block.Position.X) - 80, map_cell_height * (i + game.block.Position.Y) - 80, map_cell_width - 2, map_cell_height - 2);
                        }
                        
                    }
                }
            }

            //drugi gracz
            for (int i = 4; i < game2.board.Height + 4; i++)
            {
                for (int j = 4; j < game2.board.Width + 4; j++)
                {
                    if (game2.board.Mod[i, j].p == 1)
                    {
                        s = new SolidBrush(game2.board.Mod[i, j].c);
                        g2.FillRectangle(s, map_cell_width * j - 80, map_cell_height * i - 80, map_cell_width - 2, map_cell_height - 2);
                    }
                }
            }
            s = new SolidBrush(game2.block.color);

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (game2.block.Mod[i, j] == 1)
                    {
                        if (i + game2.block.Position.Y > 3)
                        {

                            g2.FillRectangle(s, map_cell_width * (j + game2.block.Position.X) - 80, map_cell_height * (i + game2.block.Position.Y) - 80, map_cell_width - 2, map_cell_height - 2);
                        }

                    }
                }
            }

            //wyświetlanie kolejnych klocków w obszarze picturebox

                s = new SolidBrush(game.nextBlock.color);
                  for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                     {
                        if (game.nextBlock.Mod[i, j] == 1)
                         {
                            g.FillRectangle(s, map_cell_width * j + 360, map_cell_height * i + 200, map_cell_width - 2, map_cell_height - 2);
                         }
                    }
                }

            s = new SolidBrush(game2.nextBlock.color);
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (game2.nextBlock.Mod[i, j] == 1)
                    {
                        g2.FillRectangle(s, map_cell_width * j + 360, map_cell_height * i + 200, map_cell_width - 2, map_cell_height - 2);
                    }
                }
            }


            pictureBox1.BackgroundImage = bmp;
            pictureBox1.Refresh();

            pictureBox2.BackgroundImage = bmp2;
            pictureBox2.Refresh();
        }
       
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (game.speed.Enabled == true)
            {
                return false;
            }
            return base.ProcessDialogKey(keyData);
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Keys key = e.KeyCode;

            //Gracz Alpha
            if (key == Keys.W)
            {
                pictureBox1.Refresh();
                game.Rotate();
                Draw();
            }
            if (key == Keys.A)
            {
                pictureBox1.Refresh();
                game.Move(Towards.LEFT);
                Draw();
            }
            if (key == Keys.D)
            {
                pictureBox1.Refresh();
                game.Move(Towards.RIGHT);
                Draw();
            }
            if (key == Keys.S)
            {
                pictureBox1.Refresh();
                game.Move(Towards.DOWN);
                Draw();
            }

            //Gracz Beta
            if (key == Keys.Up)
            {
                pictureBox2.Refresh();
                game2.Rotate();
                Draw();
            }
            if (key == Keys.Left)
            {
                pictureBox2.Refresh();
                game2.Move(Towards.LEFT);
                Draw();
            }
            if (key == Keys.Right)
            {
                pictureBox2.Refresh();
                game2.Move(Towards.RIGHT);
                Draw();
            }
            if (key == Keys.Down)
            {
                pictureBox2.Refresh();
                game2.Move(Towards.DOWN);
                Draw();
            }

        }
    }
}
