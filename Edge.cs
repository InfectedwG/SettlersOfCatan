using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlersOfCatan
{
    internal class Edge
    {
        private int id;
        private int counter = 0;
        private Dictionary<Hexagon, int> adjacentHexesAndEdges = new Dictionary<Hexagon, int>();

        public int Id { get; }

        public Dictionary<Hexagon, int> AdjacentHexes { get; }

        public Edge(Hexagon hex, int edgeId, Dictionary<char, int[]> limits) 
        { 
            id = counter;
            counter++;

            List<Hexagon> adjacentHexes = hex.AbsolutePositionOfAdjacentHexesToEdge(edgeId, limits);
            List<int> edgeIDs = new List<int>
            {
                edgeId
            };
            int hexEdgeId = AdjacentHex_sEdgeId(edgeId);
            edgeIDs.Add(hexEdgeId);
            for (int i = 0; i < adjacentHexes.Count && i < edgeIDs.Count; i++)
                adjacentHexesAndEdges.Add(adjacentHexes[i], edgeIDs[i]);

        }

        public static int AdjacentHex_sEdgeId(int edgeOrigin)
        {
            int edgeId = edgeOrigin + 3;

            if (edgeId > 5)
            {
                int rest = edgeId - 5;
                edgeId = rest - 1;
            }

            return edgeId;
        }


    }
}
