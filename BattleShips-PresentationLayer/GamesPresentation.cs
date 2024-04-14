using BattleShips_BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShips_PresentationLayer
{
    public class GamesPresentation
    {

        private Business business;
        private GameScreen screen;
        private int loopCount = 2, player,index;
        private bool attacked;
        private string coordinate;

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

        public int PLoopCount
        {
            get { return loopCount; }
            set { loopCount = value; }
        }

        public int PPlayer
        {
            get { return player; }
            set { player = value; }
        }

        public int PIndex
        {
            get { return index; }
            set { index = value; }
        }

        public bool PAttacked
        {
            get { return attacked; }
            set { attacked = value; }
        }

        public string PCoordinate
        {
            get { return coordinate; }
            set { coordinate = value; }
        }

        public GamesPresentation(Business buisness)
        {
            PBusiness = buisness;
        }

        public int PlayGame(int menu)
        {
            while ((PBusiness.CountPlayerAttackshit(0) != 17) && (PBusiness.CountPlayerAttackshit(1) != 17))
            {
                if (PLoopCount % 2 == 0)
                {
                    PPlayer = 0;
                }
                else
                {
                    PPlayer = 1;
                }
                PAttacked = false;
                while (PAttacked == false)
                {
                    PScreen = new GameScreen(PBusiness.GetPlayerAttackCoordinates(PPlayer));
                    PScreen.PrintGrid();
                    Console.Write("Player " + (PPlayer + 1) + " please enter where you wish to attack: ");
                    PCoordinate = Console.ReadLine().ToUpper();
                    if (PBusiness.CheckAttackFormat(PCoordinate))
                    {
                        if (PBusiness.CheckAttack(PPlayer, PCoordinate))
                        {
                            Console.Write("Are you sure you wish to attack " + PCoordinate + "? Because it has already been attacked(Y,N):");
                            if (Console.ReadLine().ToUpper() == "Y")
                            {
                                PBusiness.AddAttackCoordinate(PPlayer, PCoordinate);
                                Console.Clear();
                                Console.WriteLine("Attack added!");
                                PAttacked = true;
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Attack not added!");
                            }
                        }
                        else
                        {
                            PBusiness.AddAttackCoordinate(PPlayer, PCoordinate);
                            Console.Clear();
                            Console.WriteLine("Attack added!");
                            PAttacked = true;
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Attack not added due to an invalid coordinate!");
                    }
                    if (PAttacked == true)
                    {
                        PScreen = new GameScreen(PBusiness.GetPlayerAttackCoordinates(PPlayer));
                        PScreen.PrintGrid();
                        Console.Write("Press enter so the next player can start his turn: ");
                        Console.ReadLine();
                        Console.Clear();
                    }
                }
                PLoopCount++;
            }
            Console.Write("Player " + (PPlayer + 1) + " Wins!!!");
            PBusiness.UpdateGameStatus();
            Console.ReadLine();
            Console.Clear();
            return menu+=1;
        }

        public void ViewGameAttacks()
        {
            for (PIndex = 0; PIndex < 2; PIndex++)
            {
                Console.WriteLine("Player " + (PIndex + 1) + " game attacks:");
                PScreen = new GameScreen(PBusiness.GetPlayerAttackCoordinates(PIndex));
                PScreen.PrintGrid();
                Console.Write("Press enter when done viewing: ");
                Console.ReadLine();
                Console.Clear();
            }
        }

    }
}