using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShips_DataLayer
{
    public class AttacksData
    {
        private BattleShipsEntities battleShipsEntities;

        public AttacksData()
        {
            battleShipsEntities = new BattleShipsEntities();
        }

        public List<Attacks> GetAttackCoordinates(int playerId, int gameId)
        {
            var distinctAttacks = battleShipsEntities.Attacks
                .Where(x => x.PlayerFK == playerId && x.GameFK == gameId)
                .GroupBy(x => new { x.Coordinate })
                .Select(group => group.FirstOrDefault())
                .ToList();

            return distinctAttacks;
        }

        public void AddAttack(int playerId, int gameId, string coordinate,bool hit)
        {
            Attacks attack = new Attacks
            {
                PlayerFK = playerId,
                GameFK = gameId,
                Coordinate = coordinate,
                Hit=hit
            };
            battleShipsEntities.Attacks.Add(attack);
            battleShipsEntities.SaveChanges();
        }
    }
}
