using BattleShips_BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShips_PresentationLayer
{
    public class PlayersPresentation
    {
        private Business business;
        private bool found, correct;
        private string username, password, gameTitle;
        private int index;
        private ConsoleKeyInfo key;

        public Business PBusiness
        {
            get { return business; }
            set { business = value; }
        }

        public bool PFound
        {
            get { return found; }
            set { found = value; }
        }

        public bool PCorrect
        {
            get { return correct; }
            set { correct = value; }
        }

        public string PUsername
        {
            get { return username; }
            set { username = value; }
        }

        public string PPassword
        {
            get { return password; }
            set { password = value; }
        }

        public string PGameTitle
        {
            get { return gameTitle; }
            set { gameTitle = value; }
        }

        public int PIndex
        {
            get { return index; }
            set { index = value; }
        }

        public ConsoleKeyInfo PKey
        {
            get { return key; }
            set { key = value; }
        }

        public PlayersPresentation(Business buisness)
        {
            PBusiness = buisness;
        }

        public void MaskedReadLine()
        {
            PPassword = "";
            do
            {
                PKey = Console.ReadKey(true);

                if (PKey.Key != ConsoleKey.Backspace && PKey.Key != ConsoleKey.Enter)
                {
                    PPassword += PKey.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (PKey.Key == ConsoleKey.Backspace && PPassword.Length > 0)
                    {
                        PPassword = PPassword.Substring(0, (PPassword.Length - 1));
                        Console.Write("\b \b");
                    }
                }
            }
            while (PKey.Key != ConsoleKey.Enter);
        }

        public int AddPlayerSubMenu(int menu)
        {
            for (PIndex = 0; PIndex < 2; PIndex++)
            {
                Console.Write("Player " + (PIndex + 1) + " please enter your Username: ");
                PUsername = Console.ReadLine();
                while (PBusiness.CheckUsername(PUsername))
                {
                    Console.Clear();
                    Console.Write("Player " + (PIndex + 1) + " please enter another username.\nAs " + PUsername + " has already been added to this game or it is not 3 or more characters long: ");
                    PUsername = Console.ReadLine();
                }
                PFound = PBusiness.FindUsername(PUsername);
                if (PFound)
                {
                    Console.Write("Account found, please enter your current Password: ");
                    MaskedReadLine();
                    PCorrect = PBusiness.CheckPassword(PPassword);
                    while (!PCorrect)
                    {
                        Console.Write("\nIncorrect password please try again: ");
                        MaskedReadLine();
                        PCorrect = PBusiness.CheckPassword(PPassword);
                    }
                }
                else
                {
                    Console.Write("Account not found, please enter your new Password: ");
                    MaskedReadLine();
                    while (PPassword.Length < 3)
                    {
                        Console.Clear();
                        Console.Write("Player " + (PIndex + 1) + " please enter another password.\nAs the password has to be 3 or more characters long: ");
                        MaskedReadLine();
                    }
                    PBusiness.AddPlayer(PPassword);
                }
                Console.Clear();
            }
            Console.Write("Both Players have been declared.\nPlease enter the game title so you can start your game: ");
            PGameTitle = Console.ReadLine();
            while (PGameTitle.Length < 3)
            {
                Console.Clear();
                Console.Write("Please enter another title.\nAs the title has to be 3 or more characters long: ");
                PGameTitle = Console.ReadLine();
            }
            PBusiness.CreateNewGame(PGameTitle);
            PPassword = "";
            Console.Clear();
            return menu+=1;
        }
    }
}