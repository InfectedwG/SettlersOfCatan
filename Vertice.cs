﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlersOfCatan
{
    internal class Vertice
    {
        private int id;

        public int Id
        {
            get { return id; }
            set
            {
                if (value >= 0 && value <= 5) id = value;
                else id = 0;
            }
        }

        public Vertice(int id) { Id = id; }
    }
}
