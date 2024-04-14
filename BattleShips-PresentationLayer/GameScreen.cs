using BattleShips_BusinessLayer;
using BattleShips_PresentationLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShips_PresentationLayer
{
    public class GameScreen
    {
        private List<Cell> cells=new List<Cell>();
        private char[] rowLabels = { 'A', 'B', 'C', 'D', 'E', 'F', 'G' };
        private int labelCount,index;

        public List<Cell> PCells
        {
            get { return cells; }
            set { cells = value; }
        }

        public char[] PRowLabels
        {
            get { return rowLabels; }
            set { rowLabels = value; }
        }

        public int PLabelCount
        {
            get { return labelCount; }
            set { labelCount = value; }
        }

        public int PIndex
        {
            get { return index; }
            set { index = value; }
        }

        public GameScreen(List<Cell> cells) 
        {
            PCells = cells;
        }

        public GameScreen(List<int> gameShipConfigurationCoordinates)
        {
            foreach (int gridCellNo in gameShipConfigurationCoordinates)
            {
                PCells.Add(new ShipCell { PGridCellNo = gridCellNo });
            }
        }

        public GameScreen(List<(int, bool)> attacks)
        {
            foreach ((int, bool) attack in attacks)
            {
                PCells.Add(new AttackCell { PGridCellNo=attack.Item1,PHit=attack.Item2});
            }
        }

        public void PrintGrid()
        {
            Console.WriteLine("  1 2 3 4 5 6 7 8");
            PLabelCount = 0;
            for (PIndex = 1; PIndex < 57; PIndex ++)
            {
                if ((PIndex - 1) % 8 == 0 && PIndex != 56)
                {
                    Console.Write(PRowLabels[PLabelCount] + " ");
                    PLabelCount += 1;
                }

                bool cellMatched = false;

                foreach (Cell cell in PCells)
                {
                    if (cell.PGridCellNo == PIndex)
                    {
                        cell.PrintCell();
                        cellMatched = true;
                        break;
                    }
                }

                if (!cellMatched)
                {
                    Console.Write("~ ");
                }

                if (PIndex % 8 == 0)
                {
                    Console.Write("\n");
                }
            }
        }
    }
}
