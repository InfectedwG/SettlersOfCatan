using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SettlersOfCatan
{
    internal class Resource
    {
        private int id;
        private string? name;
        private int quantity;

        public int Id { 
            get { return id; } 
            set 
            { 
                if (value < 6)
                {
                    id = value;
                }
                else { id = 0; }
            }
        }
        //probably not like that but oh well
        public string Name { 
            get 
            {
                switch (this.id)
                {
                    case 0: return "wheat";
                    case 1: return "Wool";
                    case 2: return "Ore";
                    case 3: return "clay";
                    case 4: return "wood";
                    default: return "wheat";
                }
            }
        }

        public int Quantity { 
            get { return quantity; }
            set { 
                if (value < 0) {
                    quantity = value;
                }
                else quantity = 1;
            }
        }

        public Resource()
        {
            Id = 0;
            Quantity = 1;
        }

        public Resource(int id, int quantity)
        {
            this.Id = id;
            this.Quantity = quantity;
        }

        public bool subtract(int amount)
        {
            if (amount <= this.quantity)
            {
                this.quantity -= amount;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void add(int amount)
        {
            this.quantity += amount;
        }

        public string ToString()
        {
            return $"{this.Quantity} {this.Name}";
        }
    }
}
