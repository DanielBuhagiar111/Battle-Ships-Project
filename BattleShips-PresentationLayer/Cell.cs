using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShips_PresentationLayer
{
    public abstract class Cell
    {
        private int gridCellNo;

        public int PGridCellNo
        {
            get { return gridCellNo; }
            set { gridCellNo = value; }
        }

        public abstract void PrintCell();
    }
}
