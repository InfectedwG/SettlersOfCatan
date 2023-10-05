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
        private Vertice verticePosition;

        public Hexagon HexPosition { get; set; }
        public Vertice VerticePosition { get; set; }

        public Road(Hexagon hex, Vertice vertice, int victoryPoints) : base (victoryPoints)
        {
            HexPosition = hex;
            VerticePosition = vertice;
        }
    }
}
