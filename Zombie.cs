using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieApp
{
    internal class Zombie : Enemy
    {
        public int Damage { get; set; }

        // Constructor for the Zombie class
        public Zombie()
        {
            // Add speed value for zombie enemies
            Damage = 10;
        }

        public void Attack()
        {
            // Add special attack 
        }

    }
}

