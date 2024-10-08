using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieApp
{
    public class Enemy
    {
        public int Health { get; set; }
        public int Speed { get; set; }

        public Enemy()
        {
            Health = 100;
            Speed = 10;
        }
    }
}

