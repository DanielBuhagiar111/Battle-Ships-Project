using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShips_PresentationLayer
{
    public class AttackCell:Cell
    {
        private bool hit;

        public bool PHit
        {
            get { return hit; }
            set { hit = value; }
        }

        public override void PrintCell()
        {
            if (PHit == true)
            {
                Console.Write("X ");
            }
            else
            {
                Console.Write("O ");
            }

        }
    }
}
