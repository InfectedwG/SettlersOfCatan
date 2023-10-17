using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlersOfCatan
{
    internal class GameBoard
    {
        private readonly Dictionary<char, int[]> limits = new Dictionary<char, int[]>();
        private readonly List<Hexagon> hexList = new List<Hexagon>();
        private readonly List<Corner> cornerList = new List<Corner>();
        private readonly List<Edge> edgeList = new List<Edge>();

        public Dictionary<char, int[]> Limits { get { return limits; } }
        public List<Hexagon> HexList { get { return hexList; } }
        public List<Corner> CornerList { get {  return cornerList; } }
        public List<Edge> EdgeList { get {  return edgeList; } }

        public GameBoard(Dictionary<char, int[]> limits)
        {
            //hexList
            hexList.Add(new Hexagon(0, 0, 0));
            int q = limits['q'][1];
            int r = limits['r'][1];
            int s = limits['s'][1];

            for (int i = 1; i <= r; i++)
            {
                Hexagon hexPos = new Hexagon(0, i, -i);
                Hexagon hexNeg = new Hexagon(0, -i, i);
                hexList.Add(hexPos);
                hexList.Add(hexNeg);
            }
            for (int i = 1; i <= s; i++)
            {
                Hexagon hexPos = new Hexagon(-i, 0, i);
                Hexagon hexNeg = new Hexagon(i, 0, -i);
                hexList.Add(hexPos);
                hexList.Add(hexNeg);
            }
            for (int i = 1; i <= q; i++)
            {
                Hexagon hexPos = new Hexagon(i, -i, 0);
                Hexagon hexNeg = new Hexagon(-i, i, 0);
                hexList.Add(hexPos);
                hexList.Add(hexNeg);
            }

            this.quadrantDefinition(0, true, q);
            this.quadrantDefinition(0, false, q);
            this.quadrantDefinition(1, true, r);
            this.quadrantDefinition(1, false, r);
            this.quadrantDefinition(2, true, s);
            this.quadrantDefinition(2, false, s);

            //cornerList
            for (int i = 0; i < hexList.Count; i+=2)
            {
                Corner corner1 = new Corner(hexList[i], 0, limits);
                Corner corner2 = new Corner(hexList[i], 1, limits);
                Corner corner3 = new Corner(hexList[i], 2, limits);
                Corner corner4 = new Corner(hexList[i], 3, limits);
                Corner corner5 = new Corner(hexList[i], 4, limits);
                Corner corner6 = new Corner(hexList[i], 5, limits);
                Corner[] tempList = {corner1, corner2, corner3, corner4, corner5, corner6};

                for (int j = 0; j < tempList.Length; j++)
                {
                    bool canAdd = true;
                    for (int k = 0; k < cornerList.Count; k++)
                    {
                        if (tempList[j].AreEquals(cornerList[k]))
                        {
                            canAdd = false;
                            break;
                        }
                    }
                    if (canAdd) cornerList.Add(tempList[j]);
                }
            }

            //edgeList
            for (int i = 0; i < hexList.Count; i += 2)
            {
                Edge edge1 = new Edge(hexList[i], 0, limits);
                Edge edge2 = new Edge(hexList[i], 1, limits);
                Edge edge3 = new Edge(hexList[i], 2, limits);
                Edge edge4 = new Edge(hexList[i], 3, limits);
                Edge edge5 = new Edge(hexList[i], 4, limits);
                Edge edge6 = new Edge(hexList[i], 5, limits);
                Edge[] tempList = {edge1, edge2, edge3, edge4, edge5, edge6};

                for (int j = 0; j < tempList.Length; j++)
                {
                    bool canAdd = true;
                    for (int k = 0; k < edgeList.Count; k++)
                    {
                        if (tempList[j].AreEquals(edgeList[k]))
                        {
                            canAdd = false;
                            break;
                        }
                    }
                    if (canAdd) edgeList.Add(tempList[j]);
                }
            }

            //attribution of resources and numberToken
            int[] resourcePool = availableResources(limits);
            int[] numberTokenPool = availableNumberTokens(limits);

            Random random = new Random();

            foreach (var hex in hexList)
            {
                int roll;
                //attribution of resources
                do
                {
                    roll = random.Next(0, 5);
                } while (resourcePool[roll] == 0);

                switch (roll)
                {
                    case 0: hex.Resource = "desert"; break;
                    case 1: hex.Resource = "Wool"; break;
                    case 2: hex.Resource = "Ore"; break;
                    case 3: hex.Resource = "clay"; break;
                    case 4: hex.Resource = "wood"; break;
                    default: hex.Resource = "wheat"; break;
                }
                resourcePool[roll] -= 1;

                //attribution of numberTokens
                do
                {
                    roll = random.Next(0, 9);
                } while (numberTokenPool[roll] == 0);

                switch (roll)
                {
                    case 1: hex.NumberToken = 5; break;
                    case 2: hex.NumberToken = 6; break;
                    case 3: hex.NumberToken = 8; break;
                    case 4: hex.NumberToken = 9; break;
                    case 5: hex.NumberToken = 10; break;
                    case 6: hex.NumberToken = 11; break;
                    case 7: hex.NumberToken = 3; break;
                    case 8: hex.NumberToken = 12; break;
                    case 9: hex.NumberToken = 2; break;
                    default: hex.NumberToken = 4; break;
                }
                numberTokenPool[roll] -= 1;
            }

            
            
            

        }

        public static int numberOfHexes(Dictionary<char, int[]> limits)
        {
            int q = limits['q'][1];
            int r = limits['r'][1];
            int length = q * 2 + 1;
            int total = length;

            for (int i = 1; i <= r; i++)
            {
                total += 2 * (length - 1);
                length -= 1;
            }
            return total;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="axis">quadrant parallel to said axis</param>
        /// <param name="direction">positive or negative</param>
        /// <param name="limit">axis limit</param>
        public void quadrantDefinition(int axis, bool direction, int limit)
        {
            int[] positions = new int[3];
            int pos0 = 0;
            int pos1 = -1;
            int pos2 = -1;
            int lineCounter = 1;
            int directionFactor;
            switch (axis)
            {
                case 1:
                    positions[0] = pos1;
                    positions[1] = pos2;
                    positions[2] = pos0;
                    break;
                case 2:
                    positions[0] = pos2;
                    positions[1] = pos0;
                    positions[2] = pos1;
                    break;
                default:
                    positions[0] = pos0;
                    positions[1] = pos1;
                    positions[2] = pos2;
                    break;
            }
            if (!direction) directionFactor = -1;
            else directionFactor = 1;

            
            for (int i = 1; i <= limit; i++)
            {
                pos0 = i + 1;
                for (int j = 0; j < lineCounter; j++)
                {
                    Hexagon hex = new Hexagon(
                        positions[0] * directionFactor, 
                        positions[1] * directionFactor, 
                        positions[2] * directionFactor);
                    this.hexList.Add(hex);
                    pos1++;
                    pos2--;
                }
                lineCounter++;
                pos1 = -i;
                pos2 = -1;
            }
        }

        public static int[] availableResources(Dictionary<char, int[]> limits)
        {
            /* attribution of resources
             * desert = 1 every 19
             * wool, wheat and wood always equal and more than ore and clay
             * ore and clay always equal
            */

            //array structure : [desert, wheat, wool, ore, clay, wood]

            int numberOfHex = numberOfHexes(limits);
            int desert;
            int wheat = 4;
            int wool = 4;
            int ore = 3;
            int clay = 3;
            int wood = 4;

            
            desert = numberOfHex / 19;

            if(numberOfHex > 19) 
            {
                int numberToAdd = (numberOfHex - 18 - desert) / 5;
                int rest = (numberOfHex - 18 - desert) % 5;
                wheat += numberToAdd;
                wood += numberToAdd;
                wool += numberToAdd;
                ore += numberToAdd;
                clay += numberToAdd;

                switch (rest)
                {
                    case 1: wheat += rest; break;
                    case 2:
                        wheat += 1;
                        wool += 1;
                        break;
                    case 3:
                        wheat += 1;
                        wool += 1;
                        wood += 1;
                        break;
                    case 4:
                        wheat += 1;
                        wool += 1;
                        wood += 1;
                        clay += 1;
                        break;
                    default: return new int[] { desert, wheat, wool, ore, clay, wood };
                }
            }

            return new int[] { desert, wheat, wool, ore, clay, wood};
        }

        public static int[] availableNumberTokens(Dictionary<char, int[]> limits)
        {
            //array structure : [2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12]

            int numberOfHex = numberOfHexes(limits);
            int _2 = 1;
            int _3 = 2;
            int _4 = 2;
            int _5 = 2;
            int _6 = 2;
            int _8 = 2;
            int _9 = 2;
            int _10 = 2;
            int _11 = 1;
            int _12 = 1;
            int[] result = new int[] { _4, _5, _6, _8, _9, _10, _11, _3, _12, _2 };

            if (numberOfHex > 19)
            {
                int numberToAdd = (numberOfHex - 19) / 10;
                int rest = (numberOfHex - 19) % 10;

                for (int i = 0; i < result.Length; i++)
                {
                    result[i] += numberToAdd;
                }

                for (int i = 0; i <= rest; i++)
                {
                    result[i] += 1;
                }

                
            }

            return result;
        }

        public List<Edge> EdgesAdjacentToCorner(Corner corner)
        {
            List<Edge> result = new List<Edge>();
            int i;
            int edgeId1;
            int edgeId2;
            int edgeId3;


            switch (corner.AdjacentHexesAndCorners.Count)
            {
                case 1:
                    i = 0;
                    Hexagon hex = corner.AdjacentHexesAndCorners.ElementAt(0).Key;
                    while (result.Count < 2)
                    {
                        if (EdgeList[i].AdjacentHexesAndEdges.Count == 1 && 
                            EdgeList[i].AdjacentHexesAndEdges.ElementAt(0).Key.AreEquals(hex))
                        {
                            result.Add(EdgeList[i]);
                        }
                        i++;
                    }
                    break;
                case 2:
                    i = 0;
                    Hexagon hex1 = corner.AdjacentHexesAndCorners.ElementAt(0).Key;
                    Hexagon hex2 = corner.AdjacentHexesAndCorners.ElementAt(1).Key;
                    while (result.Count < 3)
                    {
                        if(EdgeList[i].AdjacentHexesAndEdges.Count == 2)
                        {
                            if ((EdgeList[i].AdjacentHexesAndEdges.ElementAt(0).Key.AreEquals(hex1) || EdgeList[i].AdjacentHexesAndEdges.ElementAt(1).Key.AreEquals(hex1)) &&
                                (EdgeList[i].AdjacentHexesAndEdges.ElementAt(0).Key.AreEquals(hex2) || EdgeList[i].AdjacentHexesAndEdges.ElementAt(1).Key.AreEquals(hex2)))
                            {
                                result.Add(EdgeList[i]);
                            }
                        }
                        i++;
                    }
                    break;
                case 3:
                    i = 0;
                    Hexagon hex3 = corner.AdjacentHexesAndCorners.ElementAt(0).Key;
                    Hexagon hex4 = corner.AdjacentHexesAndCorners.ElementAt(1).Key;
                    Hexagon hex5 = corner.AdjacentHexesAndCorners.ElementAt(2).Key;
                    while(result.Count < 4)
                    {
                        if (EdgeList[i].AdjacentHexesAndEdges.Count == 3)
                        {
                            if ((EdgeList[i].AdjacentHexesAndEdges.ElementAt(0).Key.AreEquals(hex3) || EdgeList[i].AdjacentHexesAndEdges.ElementAt(1).Key.AreEquals(hex3) || EdgeList[i].AdjacentHexesAndEdges.ElementAt(2).Key.AreEquals(hex3)) &&
                                (EdgeList[i].AdjacentHexesAndEdges.ElementAt(0).Key.AreEquals(hex4) || EdgeList[i].AdjacentHexesAndEdges.ElementAt(1).Key.AreEquals(hex4) || EdgeList[i].AdjacentHexesAndEdges.ElementAt(2).Key.AreEquals(hex4)) &&
                                (EdgeList[i].AdjacentHexesAndEdges.ElementAt(0).Key.AreEquals(hex5) || EdgeList[i].AdjacentHexesAndEdges.ElementAt(1).Key.AreEquals(hex5) || EdgeList[i].AdjacentHexesAndEdges.ElementAt(2).Key.AreEquals(hex5)))
                            {
                                result.Add(EdgeList[i]);
                            }
                        }
                        i++;
                    }
                    break;
            }

            return result;
        }

        public List<Edge> EdgesAdjacentToEdges(Edge edge)
        {
            List<Edge> result = new List<Edge>();
            Hexagon hex1 = new Hexagon();
            int edgeId1 = edge.AdjacentHexesAndEdges.ElementAt(0).Value;
            Hexagon hex2 = new Hexagon();
            int edgeId2;
            hex1 = edge.AdjacentHexesAndEdges.ElementAt(0).Key;
            if (edge.AdjacentHexesAndEdges.Count == 2)
            {
                hex2 = edge.AdjacentHexesAndEdges.ElementAt(1).Key;
                edgeId2 = edge.AdjacentHexesAndEdges.ElementAt(0).Value;

                int edgeIdResult1 = edgeId1 - 1;
                if(edgeIdResult1 < 0) edgeIdResult1 += 6;
                Edge edgeResult1 = new Edge(hex1, edgeIdResult1, Limits);

                int edgeIdResult2 = edgeId1 + 1;
                if (edgeIdResult2 > 5) edgeIdResult1 -= 6;
                Edge edgeResult2 = new Edge(hex1, edgeIdResult2, Limits);

                int edgeIdResult3 = edgeId1 - 1;
                if (edgeIdResult3 < 0) edgeIdResult1 += 6;
                Edge edgeResult3 = new Edge(hex2, edgeIdResult3, Limits);

                int edgeIdResult4 = edgeId1 + 1;
                if (edgeIdResult4 < 0) edgeIdResult1 -= 6;
                Edge edgeResult4 = new Edge(hex2, edgeIdResult4, Limits);
            }
            else
            {
                int edgeIdResult1 = edgeId1 - 1;
                if (edgeIdResult1 < 0) edgeIdResult1 += 6;
                Edge edgeResult1 = new Edge(hex1, edgeIdResult1, Limits);

                int edgeIdResult2 = edgeId1 + 1;
                if (edgeIdResult2 > 5) edgeIdResult1 -= 6;
                Edge edgeResult2 = new Edge(hex1, edgeIdResult2, Limits);
            }








            return result;
        }


    }
}
