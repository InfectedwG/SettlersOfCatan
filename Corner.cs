using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlersOfCatan
{
    internal class Corner
    {
        private int counter = 0;
        private int id;
        private Dictionary<Hexagon, int> adjacentHexesAndCorners = new Dictionary<Hexagon, int>();

        public int Id { get; }

        public Dictionary<Hexagon, int> AdjacentHexesAndCorners { get; }

        public Corner(Hexagon hex, int cornerId, Dictionary<char, int[]> limits)
        { 
            id = counter;
            counter++;

            List<Hexagon> adjacentHexes = hex.AbsolutePositionOfAdjacentHexesWithCorner(cornerId, limits);
            List<int> cornerIDs = new List<int>
            {
                cornerId
            };
            int hex1CornerId = AdjacentHex_sCornerId(cornerId);
            cornerIDs.Add(hex1CornerId);
            cornerIDs.Add(AdjacentHex_sCornerId(hex1CornerId));

            for (int i = 0; i < adjacentHexes.Count && i < cornerIDs.Count; i++)
                adjacentHexesAndCorners.Add(adjacentHexes[i], cornerIDs[i]);
        }
        /*
         the corner of the adjacent hex is the origin+2 if origin+2 <= 5, 
        if not your restart from of however more than 5 you go over 
        (so the rest more than 5)
         */
        public static int AdjacentHex_sCornerId(int cornerOrigin)
        {
            int cornerId = cornerOrigin + 2;

            if(cornerId > 5)
            {
                int rest = cornerId - 5;
                cornerId = rest - 1;
            }

            return cornerId;
        }

        public bool AreEquals(Corner corner)
        {
            bool result = false;

            for (int i = 0; i < this.adjacentHexesAndCorners.Count || i < corner.adjacentHexesAndCorners.Count; i++)
            {
                Hexagon hex1 = this.adjacentHexesAndCorners.ElementAt(i).Key;
                int cornerId1 = this.adjacentHexesAndCorners[hex1];

                for (int j = 0; j < corner.adjacentHexesAndCorners.Count; j++)
                {
                    Hexagon hex2 = corner.adjacentHexesAndCorners.ElementAt(j).Key;
                    int cornerId2 = corner.adjacentHexesAndCorners[hex2];
                    if (hex1.AreEquals(hex2) && cornerId1 == cornerId2)
                    {
                        result = true;
                        break;
                    }
                }
                if (result) break;
            }

            return result;
        }
        /*
        public bool AreAdjacent(Corner corner)
        {

        }
        */
    }
}
