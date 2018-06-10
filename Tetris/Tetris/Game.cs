using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Tetris
    //Klasa gry, zbierająca zachowanie klocków na planszy
{
    //typ wyliczeniowy 
    public enum Towards { UP = 0, RIGHT, DOWN, LEFT };
    class Game
    {
        public Board board;
        public Board board2;

        public Block block;
        public Block nextBlock;

        public Block[] blockArray = new Block[7];


        public Timer speed; 
        public static Random random = new Random(); 
        public int score = 0;
        public Game()
        {
            //tworzymy tablice dla różnych obiektów klocków
            blockArray = new Block[7]
            {
                blockArray[0] = new I(),
                blockArray[1] = new J(),
                blockArray[2] = new T(),
                blockArray[3] = new S(),
                blockArray[4] = new Z(),
                blockArray[5] = new O(),
                blockArray[6] = new L(),

             };

            board = new Board(10, 15);
            board2 = new Board(10, 15);

            CreateBrick();
            nextBlock = blockArray[random.Next(blockArray.Length)]; //losowanie obiektu z tablicy

            speed = new Timer();
            speed.Tick += new EventHandler(speed_Tick);
            speed.Interval = 1000;
            speed.Enabled = true;

            
        }
        private void speed_Tick(object sender, EventArgs e)
        {
            if (!Drop())
            {
                PutBrick2Map();
                ClearLine();
                if (!NewFall())
                {
                    speed.Enabled = false;
                    MessageBox.Show("GAME OVER");
                    //Application.Exit();
                }
            }
        }
        
      
     
        public bool Intersect() 
        {
            bool flag = false;
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                    if (block.nMod[i, j] + board.Mod[block.nPosition.Y + i, block.nPosition.X + j].p == 2)
                        flag = true;
            return flag;
        }
        public void PutBrick2Map()
        {
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                    if (block.Mod[i, j] == 1)
                    {
                        board.Mod[i + block.Position.Y, j + block.Position.X].p = block.Mod[i, j];
                        board.Mod[i + block.Position.Y, j + block.Position.X].c = block.color;
                    }
        }
        public void Rotate()
        {
            block.RotateBlock();
            if(!Intersect())
            {
                block.ChangeMod();
            }
        }
        public void CreateBrick()
        {
            block = blockArray[random.Next(blockArray.Length)]; //losowanie klocka z tablicy
            block.Position.X = (board.Width + 8 / 2) - 8;
            block.Position.Y = 2;
        }
        public bool NewFall()
        {
            block = nextBlock;
            block.Position.X = (board.Width + 8 / 2) - 8;
            block.Position.Y = 1;
            nextBlock = blockArray[random.Next(blockArray.Length)];
            block.MoveBlock();
            return !Intersect();
        }
        public bool Drop()
        {
            bool flag = false;
            block.MoveBlock();
            block.MoveDown();
            if (!Intersect())
            {
                flag = true;
                block.ChangePos();
            }
            else
            {
                block.InitialtPos();
            }
            return flag;
        }
        public void Move(Towards dir)
        {
            block.MoveBlock();
            if (dir == Towards.LEFT)
                block.MoveLeft();
            if (dir == Towards.RIGHT)
                block.MoveRight();
            if (dir == Towards.UP)
                block.MoveUp();
            if (dir == Towards.DOWN)
                block.MoveDown();
            if (!Intersect())
            {
                block.ChangePos();
            }
            else
            {
                block.InitialtPos();
            }
        }

        //algorytm, który pozwala na "wyczyszczenie oststaniej linii
        public void ClearLine()
        {
            int i, dx, dy;
            bool fullflag;

            for (i = 4; i < board.Height + 4; i++)//Ostatnia linia utrzymuje linię
            {
                fullflag = true;//zaklada, ze jest pełnny
                for (int j = 4; j < board.Width + 4; j++)//zarezerwowana kolumna
                { 
                    if (board.Mod[i,j].p == 0)
                    {
                        fullflag = false;
                        break;
                    }
                }//znajdz pełne
                if (fullflag)
                {  //ustawienie, żeby dodawało punkty
                    score += 10;
                    for (dy = i; dy > 0; dy--)
                        for (dx = 4; dx < board.Width + 4; dx++)
                            board.Mod[dy, dx] = board.Mod[dy - 1, dx];//przenieś w dół o jedną linie
                    for (dx = 4; dx < board.Width + 4; dx++)
                        board.Mod[0, dx].p = 0;//i wyczyść pierwsza linie
                }
            }
        }
    }
}
