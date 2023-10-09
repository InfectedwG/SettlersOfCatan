using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlersOfCatan
{
    internal class Vertice
    {
        private int id;
        private int counter = 0;
        private Dictionary<Hexagon, int> adjacentHexesAndVertices = new Dictionary<Hexagon, int>();

        public int Id { get; }

        public Dictionary<Hexagon, int> AdjacentHexes { get; }

        public Vertice(Hexagon hex, int verticeId, Dictionary<char, int[]> limits) 
        { 
            id = counter;
            counter++;
        }


    }
}
