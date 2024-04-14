using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShips_PresentationLayer
{
    public class ShipCell:Cell
    {
        public override void PrintCell()
        {
            Console.Write("S ");
        }
    }
}
