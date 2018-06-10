﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tetris
{
    public class Board
        //klasa tworząca tablice, czy raczej " siatkę" na planszy, dzięki której klocki będą przesuwały i obracały się w sposób bardziej efektywny
    {
        //deklaracja "lekkiego" obiektu Cell (dlatego struktura)
        public struct Cell
        {
            public int p;
            public Color c;
        };
        public Cell[,] Mod;
        public int Height;
        public int Width;
        public Board(int width, int height)
        {
            Width = width;
            Height = height;
            Mod = new Cell[height + 8, width + 8];
            for (int i = 0; i < height + 8; i++)
            {
                for (int j = 0; j < width + 8; j++)
                {
                    if (j < 4 || j > width + 3 || i > height + 3)
                    {
                        Mod[i, j].p = 1;
                    }
                }
            }
        }
    }
}
