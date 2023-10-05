using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlersOfCatan
{
    internal class DevelopmentCard
    {
        private int id;
        private string? name;
        private int OwnershipLength; //in turns
        private readonly Cost cost = new(1, 1, 1, 0, 0);


        public bool canSpend()
        {
            if (OwnershipLength > 0) return true;
            else return false;
        }
    }
}
