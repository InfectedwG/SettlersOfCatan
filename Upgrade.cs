using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlersOfCatan
{
    internal class Upgrade
    {
        private int victoryPoints;

        public int VictoryPoints
        {
            get { return victoryPoints; }
            set
            {
                if (value >= 0) victoryPoints = value;
                else victoryPoints = 0;
            }
        }

        public Upgrade(int victoryPoints) 
        {
            VictoryPoints = victoryPoints;
        }
    }

    
}
