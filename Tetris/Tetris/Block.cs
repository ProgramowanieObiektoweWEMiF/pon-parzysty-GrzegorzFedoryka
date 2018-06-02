using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tetris
{
    //Każdy klocek ma pewne wspólne cechy i "umiejętnosci", różnią się międzysobą tylko kształtem (Mod) i kolorem.
    public class Block
    {
        //zmienne które będą używane, czyli kształt klocka - Mod, oraz jego pozycja - Position
        public int[,] Mod; //kształt klocka jako tablica x na x
        public Point Position;
        public int[,] nMod; //nowy kształt (niezbędne przy obracaniu klocka)
        public Point nPosition;
        public Color color;

        //konstruktor klasy klocek
        public Block()
        {
            color = Color.Gray;
            Mod = new int[5, 5]; //tablica 5x5 ułatwia obracanie klockiem
            Position = new Point();
            nMod = new int[5, 5];
            nPosition = new Point();
            MoveBlock();
        }
    
        public void RotateBlock()
        {
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                    nMod[j, 5 - i - 1] = Mod[i, j];
        }
        public void MoveBlock()
        {
            InitialtPos();
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                    nMod[i, j] = Mod[i, j];
        }
        public void ChangePos()
        {
            Position.X = nPosition.X;
            Position.Y = nPosition.Y;
        }
        public void InitialtPos()
        {
            nPosition.X = Position.X;
            nPosition.Y = Position.Y;
        }
        public void ChangeMod()
        {
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                    Mod[i, j] = nMod[i, j];
        }
        public void MoveLeft()
        {
            nPosition.Offset(-1, 0);
        }
        public void MoveRight()
        {
            nPosition.Offset(1, 0);
        }
        public void MoveUp()
        {
            nPosition.Offset(0, -1);
        }
        public void MoveDown()
        {
            nPosition.Offset(0, 1);
        }
    }
}
