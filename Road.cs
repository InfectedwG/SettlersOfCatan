using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlersOfCatan
{
    internal class Road : Upgrade
    {
        public Cost cost = new Cost(0, 0, 0, 1, 1);
        private Hexagon hexPosition;
        private Edge verticePosition;

        public Hexagon HexPosition { get; set; }
        public Edge VerticePosition { get; set; }

        public Road(Hexagon hex, Edge vertice, int victoryPoints) : base (victoryPoints)
        {
            HexPosition = hex;
            VerticePosition = vertice;
        }
    }
}
