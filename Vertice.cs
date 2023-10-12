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

            List<Hexagon> adjacentHexes = hex.AbsolutePositionOfAdjacentHexesToVertice(verticeId, limits);
            List<int> verticesId = new List<int>
            {
                verticeId
            };
            int hexVerticeId = AdjacentHex_sVerticeId(verticeId);


        }

        public static int AdjacentHex_sVerticeId(int verticeOrigin)
        {
            int verticeId = verticeOrigin + 3;

            if (verticeId > 5)
            {
                int rest = verticeId - 5;
                verticeId = rest - 1;
            }

            return verticeId;
        }


    }
}
