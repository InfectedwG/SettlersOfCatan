using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlersOfCatan
{
    internal class Hexagon
    {
        private int[] position = new int[3];
        public int[] Position { get { return position; } }
        public Hexagon(int col, int row, int slice)
        {
            position[0] = col;
            position[1] = row;
            position[2] = slice;
        }

        public Hexagon adjacent(int corner)
        {
            Hexagon adjacent = this;


            switch (corner)
            {
                case 0:
                    new[] { 1, -1, 0 };
                    
            }

            position = new int[3];

            for (int i = 0; i<position.Length; i++)
            {
                position[i] = this.Position[i] + ;
            }
        }
    }
}
