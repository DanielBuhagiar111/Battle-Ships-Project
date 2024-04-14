using BattleShips_BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleShips_PresentationLayer
{
    public class Presentation
    {
        private Business business = new Business();
        private PlayersPresentation playerPresentation;
        private ShipsPresentation shipsPresentation;
        private GamesPresentation gamePresentation;
        private List<Cell> cells;
        private int optionChosen = 0, menu = 1;

        public Business PBusiness
        {
            get { return business; }
            set { business = value; }
        }

        public PlayersPresentation PPlayerPresentation
        {
            get { return playerPresentation; }
            set { playerPresentation = value; }
        }

        public ShipsPresentation PShipsPresentation
        {
            get { return shipsPresentation; }
            set { shipsPresentation = value; }
        }

        public GamesPresentation PGamePresentation
        {
            get { return gamePresentation; }
            set { gamePresentation = value; }
        }

        public List<Cell> PCells
        {
            get { return cells; }
            set { cells = value; }
        }

        public int POptionChosen
        {
            get { return optionChosen; }
            set { optionChosen = value; }
        }

        public int PMenu
        {
            get { return menu; }
            set { menu = value; }
        }

        public Presentation()
        {
            playerPresentation = new PlayersPresentation(business);
            gamePresentation = new GamesPresentation(business);
            shipsPresentation = new ShipsPresentation(business);
        }

        public void MainProgramDisplay()
        {
            while (POptionChosen != 4)
            {
                switch (PMenu)
                {
                    case 1:
                        Console.WriteLine("1. Add Player details");
                        Console.WriteLine("2. Configure Ships (Disabled)");
                        Console.WriteLine("3. Launch Attack (Disabled)");
                        Console.WriteLine("4. Quit");
                        break;
                    case 2:
                        Console.WriteLine("1. Add Player details (Disabled)");
                        Console.WriteLine("2. Configure Ships");
                        Console.WriteLine("3. Launch Attack (Disabled)");
                        Console.WriteLine("4. Quit");
                        break;
                    case 3:
                        Console.WriteLine("1. Add Player details (Disabled)");
                        Console.WriteLine("2. View Ships");
                        Console.WriteLine("3. Launch Attack");
                        Console.WriteLine("4. Quit");
                        break;
                    case 4:
                        Console.WriteLine("1. Add Player details (Disabled)");
                        Console.WriteLine("2. View Ships");
                        Console.WriteLine("3. View Game Attacks");
                        Console.WriteLine("4. Quit");
                        break;
                }
                try {
                    Console.Write("Enter what you wish to do: ");
                    POptionChosen = Convert.ToInt16(Console.ReadLine());
                    PBusiness.AddShips(new string[] { "CARRIER", "BATTLESHIP", "CRUISER", "SUBMARINE", "DESTROYER" }, new int[] { 5, 4, 3, 3, 2 });
                    Console.Clear();
                    switch (POptionChosen)
                    {
                        case 1:
                            if (PMenu == 1)
                            {
                                PMenu = PPlayerPresentation.AddPlayerSubMenu(PMenu);
                            }
                            break;
                        case 2:
                            if (PMenu == 2)
                            {
                                PMenu = PShipsPresentation.ConfigureShips(PMenu);
                            }
                            else if (PMenu == 3 || PMenu==4)
                            {
                                PShipsPresentation.ViewShips();
                            }
                            break;
                        case 3:
                            if (PMenu == 3)
                            {
                                PMenu=PGamePresentation.PlayGame(PMenu);
                            }
                            else if (PMenu == 4)
                            {
                                PGamePresentation.ViewGameAttacks();
                            }
                            break;
                        case 4:
                            Environment.Exit(0);
                            break;
                        default:
                            throw new Exception("Incorrect input!");
                    }
                }
                catch (Exception)
                {
                    Console.Clear();
                    Console.WriteLine("An unexpected error occured!!");
                }
            }
        }
    }
}