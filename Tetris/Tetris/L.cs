using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tetris
{
    class L : Block
    {
        public L() : base()
        {
           color = Color.LightPink;
            
           Mod = new int[5, 5]
           {
                   { 0,0,0,0,0},
                   { 0,1,1,0,0},
                   { 0,0,1,0,0},
                   { 0,0,1,0,0},
                   { 0,0,0,0,0},
           };
        }
    }
}
