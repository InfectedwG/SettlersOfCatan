using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlersOfCatan
{
    internal class Player
    {
        private string name;
        private int wheatQty;
        private int woolQty;
        private int oreQty;
        private int clayQty;
        private int woodQty;
        public List<Road> roadInventory = new List<Road>();
        public List<Settlement> settlementInventory = new List<Settlement>();
        public List<City> cityInventory = new List<City>();

        public string Name { get { return name; } set { name = value; } }
        public int WheatQty 
        {
            get {  return wheatQty; } 
            set 
            {
                if (value >= 0) wheatQty = value;
                else wheatQty = 0;
            } 
        }

        public int WoolQty
        {
            get { return woolQty; }
            set
            {
                if (value >= 0) woolQty = value;
                else woolQty = 0;
            }
        }

        public int OreQty
        {
            get { return oreQty; }
            set
            {
                if (value >= 0) oreQty = value;
                else oreQty = 0;
            }
        }

        public int ClayQty
        {
            get { return clayQty; }
            set
            {
                if (value >= 0) clayQty = value;
                else clayQty = 0;
            }
        }

        public int WoodQty
        {
            get { return woodQty; }
            set
            {
                if (value >= 0) woodQty = value;
                else woodQty = 0;
            }
        }
        

        public Player(string name)
        {
            Name = name;
            WheatQty = 0;
            WoolQty = 0;
            OreQty = 0;
            ClayQty = 0;
            WoodQty = 0;
        }

        public int[] diceRoll()
        {
            Random random = new Random();
            int roll1 = random.Next(1, 6);
            int roll2 = random.Next(1, 6);
            return new[] { roll1, roll2 };
        }
    }
}
