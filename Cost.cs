using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlersOfCatan
{
    internal class Cost
    {
        private int WheatQty { get; set; }
        private int WoolQty { get; set; }
        private int OreQty { get; set; }
        private int ClayQty { get; set; }
        private int WoodQty { get; set; }

        public Cost(int wheatQty, int woolQty, int oreQty, int clayQty, int woodQty) 
        {
            WheatQty = wheatQty;
            WoodQty = woolQty;
            OreQty = oreQty;
            ClayQty = clayQty;
            WoodQty = woodQty;
        }
    }
}
