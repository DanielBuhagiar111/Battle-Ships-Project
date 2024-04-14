using BattleShips_DataLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace BattleShips_BusinessLayer
{
    public class Business
    {
        //Decided to leave this as a single class because some methods and properties are being used 
        //in many different methods
        private PlayersData playersData = new PlayersData();
        private GamesData gamesData = new GamesData();
        private ShipsData shipsData = new ShipsData();
        private GameShipConfigurationsData gameShipConfigurationsdata = new GameShipConfigurationsData();
        private AttacksData attacksdata = new AttacksData();
        private Players player;
        private Ships ship;
        private List<Attacks> rawListOfattackCoords;
        private List<(int, bool)> processedListOfAttackCoords;
        private List<string> rawListOfShipCoords;
        private List<int> processedListOfShipCoords;
        private string[] usernames = new string[2];
        private char[] rowAndColumn;
        private int gameId, usernameIndex = 0, total, hits, opponentIndex,index;
        private char row, firstRow;

        public PlayersData PPlayersData
        {
            get { return playersData; }
            set { playersData = value; }
        }

        public GamesData PGamesData
        {
            get { return gamesData; }
            set { gamesData = value; }
        }

        public ShipsData PShipsData
        {
            get { return shipsData; }
            set { shipsData = value; }
        }

        public GameShipConfigurationsData PGameShipConfigurationsData
        {
            get { return gameShipConfigurationsdata; }
            set { gameShipConfigurationsdata = value; }
        }

        public AttacksData PAttacksData
        {
            get { return attacksdata; }
            set { attacksdata = value; }
        }

        public Players PPlayer
        {
            get { return player; }
            set { player = value; }
        }

        public Ships PShip
        {
            get { return ship; }
            set { ship = value; }
        }

        public List<Attacks> PRawListOfattackCoords
        {
            get { return rawListOfattackCoords; }
            set { rawListOfattackCoords = value; }
        }

        public List<(int, bool)> PProcessedListOfAttackCoords
        {
            get { return processedListOfAttackCoords; }
            set { processedListOfAttackCoords = value; }
        }

        public List<string> PRawListOfShipCoords
        {
            get { return rawListOfShipCoords; }
            set { rawListOfShipCoords = value; }
        }

        public List<int> PProcessedListOfShipCoords
        {
            get { return processedListOfShipCoords; }
            set { processedListOfShipCoords = value; }
        }

        public string[] PUsernames
        {
            get { return usernames; }
            set { usernames = value; }
        }

        public char[] PRowAndColumn
        {
            get { return rowAndColumn; }
            set { rowAndColumn = value; }
        }

        public int PGameId
        {
            get { return gameId; }
            set { gameId = value; }
        }

        public int PUsernameIndex
        {
            get { return usernameIndex; }
            set { usernameIndex = value; }
        }

        public int PTotal
        {
            get { return total; }
            set { total = value; }
        }

        public int PHits
        {
            get { return hits; }
            set { hits = value; }
        }

        public int POpponentIndex
        {
            get { return opponentIndex; }
            set { opponentIndex = value; }
        }


        public int PIndex
        {
            get { return index; }
            set { index = value; }
        }

        public char PRow
        {
            get { return row; }
            set { row = value; }
        }

        public char PFirstRow
        {
            get { return firstRow; }
            set { firstRow = value; }
        }

        public bool CheckUsername(string usernameInput)
        {
            if (PUsernames.Contains(usernameInput) || usernameInput.Length < 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool FindUsername(string usernameInput)
        {
            PPlayer = PPlayersData.GetPlayer(usernameInput);
            if (PPlayer == null)
            {
                PUsernames[PUsernameIndex] = usernameInput;
                return false;
            }
            else
            {
                PUsernames[PUsernameIndex] = usernameInput;
                return true;
            }
        }

        public bool CheckPassword(string passwordInput)
        {
            PPlayer = PPlayersData.GetPlayer(PUsernames[PUsernameIndex]);
            if (PPlayer.Password == passwordInput)
            {
                PUsernameIndex++;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void AddPlayer(string newPassword)
        {
            PPlayersData.AddPlayer(PUsernames[PUsernameIndex], newPassword);
            PUsernameIndex++;
        }

        public void CreateNewGame(string title)
        {
            PGamesData.AddGame(title, PPlayersData.GetPlayer(PUsernames[0]).ID, PPlayersData.GetPlayer(PUsernames[1]).ID, false);
            PGameId = PGamesData.GetLastGameId();
        }

        public void AddShips(string[] shipTitles, int[] shipSizes)
        {
            foreach (string title in shipTitles)
            {
                if (PShipsData.GetShipByName(title) == null)
                {
                    PShipsData.AddShip(title, shipSizes[Array.IndexOf(shipTitles, title)]);
                }
            }
        }

        public void TranslateCoordinateToInt(string coord)
        {
            PRowAndColumn = coord.ToCharArray();
            PTotal = (int)PRowAndColumn[1] - (int)'0';
            switch (PRowAndColumn[0])
            {
                case 'B':
                    PTotal += 8;
                    break;
                case 'C':
                    PTotal += 16;
                    break;
                case 'D':
                    PTotal += 24;
                    break;
                case 'E':
                    PTotal += 32;
                    break;
                case 'F':
                    PTotal += 40;
                    break;
                case 'G':
                    PTotal += 48;
                    break;
            }
        }

        public string TranslateCoordinateToString(int coord)
        {
            switch ((int)((coord - 1) / 8))
            {
                case 0:
                    PRow = 'A';
                    break;
                case 1:
                    coord -= 8;
                    PRow = 'B';
                    break;
                case 2:
                    coord -= 16;
                    PRow = 'C';
                    break;
                case 3:
                    coord -= 24;
                    PRow = 'D';
                    break;
                case 4:
                    coord -= 32;
                    PRow = 'E';
                    break;
                case 5:
                    coord -= 40;
                    PRow = 'F';
                    break;
                case 6:
                    coord -= 48;
                    PRow = 'G';
                    break;
            }
            return PRow + coord.ToString();
        }

        public List<int> GetShipCoordinates(int playerIndex)
        {
            PRawListOfShipCoords = new List<string>();
            PRawListOfShipCoords = PGameShipConfigurationsData.GetGameShipConfigurationCoordinates(PPlayersData.GetPlayer(PUsernames[playerIndex]).ID, PGameId);
            PProcessedListOfShipCoords = new List<int>();
            foreach (string coord in PRawListOfShipCoords)
            {
                TranslateCoordinateToInt(coord);
                PProcessedListOfShipCoords.Add(PTotal);
            }
            return PProcessedListOfShipCoords;
        }

        public bool AddShipCoordinates(int playerIndex, int shipId, int[] shipsUsed, string oriantation, string coordinate)
        {
            PShip = PShipsData.GetShipByID(shipId);
            if (!shipsUsed.Contains(shipId) && (PShip != null) && (Regex.IsMatch(coordinate, "^[A-G][1-8]$")))
            {
                TranslateCoordinateToInt(coordinate);
                if (oriantation == "V")
                {
                    PFirstRow = TranslateCoordinateToString(PTotal)[0];
                    if (((PFirstRow - 'A' + 1) + PShip.Size - 1) > 7)
                    {
                        return false;
                    }
                    for (PIndex = 0; PIndex < PShip.Size; PIndex++)
                    {
                        if ((!Regex.IsMatch(TranslateCoordinateToString(PTotal + PIndex), "^[A-G][1-8]$")) || (PGameShipConfigurationsData.GetGameShipConfiguration(PPlayersData.GetPlayer(PUsernames[playerIndex]).ID, PGameId, TranslateCoordinateToString(PTotal + (PIndex * 8))) != null))
                        {
                            return false;
                        }
                    }
                    for (PIndex = 0; PIndex < PShip.Size; PIndex++)
                    {
                        PGameShipConfigurationsData.AddGameShipConfiguration(PPlayersData.GetPlayer(PUsernames[playerIndex]).ID, PGameId, TranslateCoordinateToString(PTotal + (PIndex * 8)));
                    }
                }
                else if (oriantation == "H")
                {
                    PFirstRow = TranslateCoordinateToString(PTotal)[0];
                    for (PIndex = 0; PIndex < PShip.Size; PIndex++)
                    {
                        if ((!Regex.IsMatch(TranslateCoordinateToString(PTotal + PIndex), "^[A-G][1-8]$")) || (PGameShipConfigurationsData.GetGameShipConfiguration(PPlayersData.GetPlayer(PUsernames[playerIndex]).ID, PGameId, TranslateCoordinateToString(PTotal + PIndex)) != null) || (TranslateCoordinateToString(PTotal + PIndex)[0] != PFirstRow))
                        {
                            return false;
                        }
                    }
                    for (PIndex = 0; PIndex < PShip.Size; PIndex++)
                    {
                        PGameShipConfigurationsData.AddGameShipConfiguration(PPlayersData.GetPlayer(PUsernames[playerIndex]).ID, PGameId, TranslateCoordinateToString(PTotal + PIndex));
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        public List<(int, bool)> GetPlayerAttackCoordinates(int playerIndex)
        {
            PRawListOfattackCoords = new List<Attacks>();
            PRawListOfattackCoords = PAttacksData.GetAttackCoordinates(PPlayersData.GetPlayer(PUsernames[playerIndex]).ID, PGameId);
            PProcessedListOfAttackCoords = new List<(int, bool)>();
            foreach (Attacks attack in PRawListOfattackCoords)
            {
                TranslateCoordinateToInt(attack.Coordinate);
                PProcessedListOfAttackCoords.Add((PTotal, attack.Hit));
            }
            return PProcessedListOfAttackCoords;
        }

        public int CountPlayerAttackshit(int playerIndex)
        {
            PHits = 0;
            PRawListOfattackCoords = new List<Attacks>();
            PRawListOfattackCoords = PAttacksData.GetAttackCoordinates(PPlayersData.GetPlayer(PUsernames[playerIndex]).ID, PGameId);
            foreach (Attacks attack in PRawListOfattackCoords)
            {
                if (attack.Hit == true)
                {
                    PHits += 1;
                }
            }
            return PHits;
        }

        public bool CheckAttackFormat(string coordinate)
        {
            if (!Regex.IsMatch(coordinate, "^[A-G][1-8]$"))
            {
                return false;
            }
            return true;
        }

        public bool CheckAttack(int playerIndex, string coordinate)
        {
            PRawListOfattackCoords = new List<Attacks>();
            PRawListOfattackCoords = PAttacksData.GetAttackCoordinates(PPlayersData.GetPlayer(PUsernames[playerIndex]).ID, PGameId);
            foreach (Attacks attack in PRawListOfattackCoords)
            {
                if (string.Equals(attack.Coordinate, coordinate))
                {
                    return true;
                }
            }
            return false;
        }

        public void AddAttackCoordinate(int playerIndex, string coordinate)
        {
            if (playerIndex == 0)
            {
                POpponentIndex = 1;
            }
            else
            {
                POpponentIndex = 0;
            }
            TranslateCoordinateToInt(coordinate);
            if ((PGameShipConfigurationsData.GetGameShipConfiguration(PPlayersData.GetPlayer(PUsernames[POpponentIndex]).ID, PGameId, TranslateCoordinateToString(PTotal)) != null))
            {
                PAttacksData.AddAttack(PPlayersData.GetPlayer(PUsernames[playerIndex]).ID, PGameId, coordinate, true);
            }
            else
            {
                PAttacksData.AddAttack(PPlayersData.GetPlayer(PUsernames[playerIndex]).ID, PGameId, coordinate, false);
            }
        }

        public void UpdateGameStatus()
        {
            PGamesData.UpdateGame(PGameId, true);
        }
    }
}