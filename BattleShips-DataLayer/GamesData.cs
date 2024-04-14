using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BattleShips_DataLayer
{
    public class GamesData
    {
        private BattleShipsEntities battleShipsEntities;

        public GamesData()
        {
            battleShipsEntities = new BattleShipsEntities();
        }

        public Games GetGame(int id)
        {
            var game = battleShipsEntities.Games
                .SingleOrDefault(x => x.ID == id);

            return game;
        }

        public int GetLastGameId()
        {
            var lastGameId = battleShipsEntities.Games
                .OrderByDescending(x => x.ID)
                .Select(x => x.ID)
                .FirstOrDefault();

            return lastGameId;
        }

        public void AddGame(string title, int creatorfk, int opponentfk, bool complete)
        {
            Games game = new Games
            {
                Title = title,
                CreatorFK = creatorfk,
                OpponentFK = opponentfk,
                Complete = complete
            };

            battleShipsEntities.Games.Add(game);
            battleShipsEntities.SaveChanges();
        }

        public void UpdateGame(int id, bool complete)
        {
            var game = GetGame(id);
            if (game == null)
            {
                throw new Exception("Unable to update game");
            }

            game.Complete = complete;

            battleShipsEntities.SaveChanges();
        }

    }
}