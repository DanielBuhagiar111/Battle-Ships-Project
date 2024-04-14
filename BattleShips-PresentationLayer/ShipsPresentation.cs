using BattleShips_BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BattleShips_PresentationLayer
{
    public class ShipsPresentation
    {
        private Business business;
        private GameScreen screen;
        private int shipId,index,index2;
        private string oriantation, coordinate;
        private int[] shipIds;

        public Business PBusiness
        {
            get { return business; }
            set { business = value; }
        }

        public GameScreen PScreen
        {
            get { return screen; }
            set { screen = value; }
        }

        public int PShipId
        {
            get { return shipId; }
            set { shipId = value; }
        }

        public int PIndex
        {
            get { return index; }
            set { index = value; }
        }

        public int PIndex2
        {
            get { return index2; }
            set { index2 = value; }
        }

        public string POriantation
        {
            get { return oriantation; }
            set { oriantation = value; }
        }

        public string PCoordinate
        {
            get { return coordinate; }
            set { coordinate = value; }
        }

        public int[] PShipIds
        {
            get { return shipIds; }
            set { shipIds = value; }
        }

        public ShipsPresentation(Business buisness)
        {
            PBusiness = buisness;
        }

        public int ConfigureShips(int menu)
        {
            for (PIndex = 0; PIndex < 2; PIndex++)
            {
                PShipIds = new int[5];
                for (PIndex2 = 0; PIndex2 < 5; PIndex2++)
                {
                    PScreen = new GameScreen(PBusiness.GetShipCoordinates(PIndex));
                    PScreen.PrintGrid();
                    Console.WriteLine("Player " + (PIndex + 1) + " please position ships.");
                    Console.WriteLine("Instructions: ");
                    Console.WriteLine("Ship ID's,name and length:\n1. CARRIER - 5 holes\n2. BATTLESHIP - 4 holes\n3. CRUISER - 3 holes\n4. SUBMARINE - 3 holes\n5. DESTROYER - 2 holes");
                    Console.WriteLine("To place a ship first enter if you want the ship to continue horizontally or vertically,\nthen select the starting coordinate of the ship.");
                    Console.WriteLine("Example if you choose to place the submarine at B4 horizontally it will be placed on B4,B5 and B6");
                    Console.WriteLine("and if you choose to place the SUBMARINE at B4 vertically it will be placed on B4,A5 and A6.");
                    Console.Write("Please enter the id of the ship: ");
                    try
                    {
                        PShipId = Convert.ToInt16(Console.ReadLine());
                    }
                    catch
                    {
                        PShipId = 0;
                    }
                    Console.Write("Please enter the oriantation of the ship (V or H): ");
                    POriantation = Console.ReadLine().ToUpper();
                    Console.Write("Please enter the starting coordinate of the ship: ");
                    PCoordinate = Console.ReadLine().ToUpper();
                    Console.Clear();
                    if (!PBusiness.AddShipCoordinates(PIndex, PShipId, PShipIds, POriantation, PCoordinate))
                    {
                        Console.WriteLine("Ship was not added due to an error, the error is most likely due to an invalid coordinate/input!");
                        PIndex2 -= 1;
                    }
                    else
                    {
                        Console.WriteLine("Ship added!");
                        PShipIds[PIndex2] = PShipId;
                    }
                }
            }
            return menu+=1;
        }

        public void ViewShips()
        {
            for (PIndex = 0; PIndex < 2; PIndex++)
            {
                Console.WriteLine("Player " + (PIndex + 1) + " ship configuration:");
                PScreen = new GameScreen(PBusiness.GetShipCoordinates(PIndex));
                PScreen.PrintGrid();
                Console.Write("Press enter when done viewing: ");
                Console.ReadLine();
                Console.Clear();
            }
        }
    }
}