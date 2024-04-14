using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShips_DataLayer
{
    public class ShipsData
    {
        private BattleShipsEntities battleShipsEntities;

        public ShipsData()
        {
            battleShipsEntities = new BattleShipsEntities();
        }

        public Ships GetShipByName(string title)
        {
            var ship = battleShipsEntities.Ships
                .SingleOrDefault(x => x.Title == title);

            return ship;
        }

        public Ships GetShipByID(int id)
        {
            var ship = battleShipsEntities.Ships
                .SingleOrDefault(x => x.ID == id);

            return ship;
        }

        public void AddShip(string title, int size)
        {
            Ships ship = new Ships
            {
                Title = title,
                Size = size
            };
            battleShipsEntities.Ships.Add(ship);
            battleShipsEntities.SaveChanges();
        }
    }
}