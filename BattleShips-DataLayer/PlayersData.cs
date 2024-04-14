using System;
using System.Linq;

namespace BattleShips_DataLayer
{
    public class PlayersData
    {
        private BattleShipsEntities battleShipsEntities;

        public PlayersData()
        {
            battleShipsEntities = new BattleShipsEntities();
        }

        public Players GetPlayer(string username)
        {
            var player = battleShipsEntities.Players
                .SingleOrDefault(x => x.Username == username);

            return player;
        }

        public void AddPlayer(String username, String password)
        {
            Players player = new Players
            {
                Username = username,
                Password = password
            };

            battleShipsEntities.Players.Add(player);
            battleShipsEntities.SaveChanges();
        }
    }
}