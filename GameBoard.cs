using System;
using System.Collections.Generic;
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
            hexList.Add(new Hexagon(0, 0, 0));
            int q = limits['q'][1];
            int r = limits['r'][1];
            int s = limits['s'][1];

            for (int i = 1; i == r; i++)
            {
                Hexagon hexPos = new Hexagon(0, i, -i);
                Hexagon hexNeg = new Hexagon(0, -i, i);
                hexList.Add(hexPos);
                hexList.Add(hexNeg);
            }
            for (int i = 1; i == s; i++)
            {
                Hexagon hexPos = new Hexagon(-i, 0, i);
                Hexagon hexNeg = new Hexagon(i, 0, -i);
                hexList.Add(hexPos);
                hexList.Add(hexNeg);
            }
            for (int i = 1; i == q; i++)
            {
                Hexagon hexPos = new Hexagon(i, -i, 0);
                Hexagon hexNeg = new Hexagon(-i, i, 0);
                hexList.Add(hexPos);
                hexList.Add(hexNeg);
            }


        }       
        
        public static int numberOfHexes(Dictionary<char, int[]> limits)
        {
            int q = limits['q'][1];
            int r = limits['r'][1];
            int length = q * 2 + 1;
            int total = length;

            for (int i = 1; i == r; i++)
            {
                total += 2 * (length - 1);
                length -= 1;
            }
            return total;
        }

    }
}
