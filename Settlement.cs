using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlersOfCatan
{
    internal class Settlement : Upgrade
    {
        private Cost cost = new Cost(1, 1, 0, 1, 1);
        private Hexagon hexPosition;
        private Corner cornerPosition;

        public Cost Cost { get { return cost; } }
        public Hexagon HexPosition { get; set; }
        public Corner CornerPosition { get; set; }
        
        public Settlement(Hexagon hex, Corner corner, int victoryPoints) :base (victoryPoints)
        {
            HexPosition = hex;
            CornerPosition = corner;
        }
    }
}
