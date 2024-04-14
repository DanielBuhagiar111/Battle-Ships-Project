using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleShips_DataLayer
{
    public class GameShipConfigurationsData
    {
        private BattleShipsEntities battleShipsEntities;

        public GameShipConfigurationsData()
        {
            battleShipsEntities = new BattleShipsEntities();
        }

        public List<string> GetGameShipConfigurationCoordinates(int playerId, int gameId)
        {
            var coordinates = battleShipsEntities.GameShipConfigurations
                .Where(x => x.PlayerFK == playerId && x.GameFK == gameId)
                .Select(x => x.Coordinate)
                .ToList();

            return coordinates;
        }

        public GameShipConfigurations GetGameShipConfiguration(int playerId, int gameId, string coordinate)
        {
            var configuration = battleShipsEntities.GameShipConfigurations
                .SingleOrDefault(x => x.PlayerFK == playerId && x.GameFK == gameId && x.Coordinate == coordinate);

            return configuration;
        }

        public void AddGameShipConfiguration(int playerId, int gameId, string coordinate)
        {
            GameShipConfigurations gameshipConfiguration = new GameShipConfigurations
            {
                PlayerFK = playerId,
                GameFK = gameId,
                Coordinate = coordinate
            };
            battleShipsEntities.GameShipConfigurations.Add(gameshipConfiguration);
            battleShipsEntities.SaveChanges();
        }
    }
}