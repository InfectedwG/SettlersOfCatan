﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlersOfCatan
{
    internal class Hexagon
    {
        public readonly Dictionary<int, int[]> adjacentHexRelativePosition = new Dictionary<int, int[]>();
        public readonly Dictionary<int, int[]> cornerAdjacentHex = new Dictionary<int, int[]>();
        private readonly int[] position = new int[3];
        private readonly int id;
        private int counter = 0;
        
        public int[] Position { get { return position; } }
        public Hexagon(int col, int row, int slice)
        {
            id = counter;
            counter++;

            position[0] = col; //q
            position[1] = row;
            position[2] = slice;

            adjacentHexRelativePosition.Add(0, new int[] { 1, -1, 0 });
            adjacentHexRelativePosition.Add(1, new int[] { 1, 0, -1 });
            adjacentHexRelativePosition.Add(2, new int[] { 0, 1, -1 });
            adjacentHexRelativePosition.Add(3, new int[] { -1, 1, 0 });
            adjacentHexRelativePosition.Add(4, new int[] { -1, 0, 1 });
            adjacentHexRelativePosition.Add(5, new int[] { 0, -1, 1 });

            cornerAdjacentHex.Add(0, new int[] { 5, 0 });
            cornerAdjacentHex.Add(1, new int[] { 0, 1 });
            cornerAdjacentHex.Add(2, new int[] { 1, 2 });
            cornerAdjacentHex.Add(3, new int[] { 2, 3 });
            cornerAdjacentHex.Add(4, new int[] { 3, 4 });
            cornerAdjacentHex.Add(5, new int[] { 4, 5 });
        }

        public Hexagon[] RelativePositionOfAdjacentHexesWithCorner(int corner)
        {
            Hexagon hex1;
            Hexagon hex2;
            int[] hexesId = this.cornerAdjacentHex[corner];
            
            int[] position1 = adjacentHexRelativePosition[hexesId[0]];
            hex1 = new Hexagon(position1[0], position1[1], position1[2]);

            int[] position2 = adjacentHexRelativePosition[hexesId[1]];
            hex2 = new Hexagon(position2[0], position2[1], position2[2]);

            Hexagon[] result = {hex1, hex2};

            return result;
        }

        public Hexagon RelativePositionOfAdjacentHexToEdge(int edge)
        {
            Hexagon hex;
            int hexId = edge;

            int[] position = adjacentHexRelativePosition[hexId];
            hex = new Hexagon(position[0], position[1], position[2]);

            return hex;
        }

        public List<Hexagon> AbsolutePositionOfAdjacentHexesWithCorner(int corner, Dictionary<char, int[]> limits)
        {
            List<Hexagon> result = new List<Hexagon>();
            result.Add(this);
            Hexagon[] relativePositions = this.RelativePositionOfAdjacentHexesWithCorner(corner);

            Hexagon hex1 = new Hexagon(
                this.Position[0] + relativePositions[0].Position[0], 
                this.Position[1] + relativePositions[0].Position[1], 
                this.Position[2] + relativePositions[0].Position[2]);

            Hexagon hex2 = new Hexagon(
                this.Position[0] + relativePositions[1].Position[0],
                this.Position[1] + relativePositions[1].Position[1],
                this.Position[2] + relativePositions[1].Position[2]);

            if (hex1.HexExists(limits)) result.Add(hex1);

            if(hex2.HexExists(limits)) result.Add(hex2);

            return result;
        }

        public List<Hexagon> AbsolutePositionOfAdjacentHexesToEdge(int vertice, Dictionary<char, int[]> limits)
        {
            List<Hexagon> result = new List<Hexagon>();
            result.Add(this);
            Hexagon relativePosition = this.RelativePositionOfAdjacentHexToEdge(vertice);

            Hexagon hex = new Hexagon(
                this.Position[0] + relativePosition.Position[0],
                this.Position[1] + relativePosition.Position[1],
                this.Position[2] + relativePosition.Position[2]);

            if (hex.HexExists(limits)) result.Add(hex);

            return result;
        }


        public bool HexExists(Dictionary<char, int[]> limits) // limits represented as q : limits + and -, r : limits + and -, s : limits + and -
        {
            if (this.position[0] > limits['q'][0] || this.position[0] > limits['q'][1] || 
                this.position[1] > limits['r'][0] || this.position[1] > limits['r'][1] || 
                this.position[2] > limits['s'][0] || this.position[2] > limits['s'][1]) return false;
            else return true;
        }
    }
}
