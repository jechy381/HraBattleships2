using System;
using System.Collections.Generic;
using System.Linq;
using GymVod.Battleships.Common;

namespace HraBattleships2
{
    public class Hrac : IBattleshipsGame
    {
        int height, width;
        enum GameState
        {
            Seek,
            Destroy
        }
        GameState gameState = GameState.Seek;

        public ShipPosition[] NewGame(GameSettings gameSettings)
        {
            height = gameSettings.BoardHeight;
            width = gameSettings.BoardWidth;

            var shipPositions = new List<ShipPosition>();

            shipPositions.Add(new ShipPosition(ShipType.Submarine, new Position(1, 2), Orientation.Right));
            shipPositions.Add(new ShipPosition(ShipType.Destroyer, new Position(12, 4), Orientation.Right));
            shipPositions.Add(new ShipPosition(ShipType.Cruiser, new Position(4,5), Orientation.Down));
            shipPositions.Add(new ShipPosition(ShipType.Battleship, new Position(8, 10), Orientation.Down));
            shipPositions.Add(new ShipPosition(ShipType.Carrier, new Position(1, 12), Orientation.Right));

            /*foreach (var shipType in gameSettings.ShipTypes.OrderByDescending(x => (int)x))
            {
                var position = new Position(1, y);
                shipPositions.Add(new ShipPosition(shipType, position, Orientation.Right));
                y += 2;
            }
            */

            return shipPositions.ToArray();
        }

        HashSet<Position> misses = new HashSet<Position>();
        HashSet<Position> hits = new HashSet<Position>();

        public Position GetNextShotPosition()
        {
            if (gameState == GameState.Seek)
            {
                if (!hits.Any(x => true))
                {
                    return null;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                
                return null;
            }
        }


        public void ShotResult(ShotResult shotResult)
        {
            if (shotResult.Hit)
            {
                hits.Add(shotResult.Position);
                if (shotResult.ShipSunken)
                {
                    gameState = GameState.Seek;
                }
            }
            else
            {
                misses.Add(shotResult.Position);
            }
        }


        Random rnd = new Random();
        public Position GetRandomPosition(HashSet<Position> hits, HashSet<Position> misses)
        {
            HashSet<Position> exlude = new HashSet<Position>();
            exlude.UnionWith(hits);
            exlude.UnionWith(misses);

            byte x = (byte)rnd.Next(1, width - 1);
            byte y = (byte)rnd.Next(1, height - 1);
            Position position = new Position(x, y);
            while (exlude.Contains(position))
            {
                x = (byte)rnd.Next(1, width - 1);
                y = (byte)rnd.Next(1, height - 1);
                position = new Position(x, y);
            }
            exlude.Add(position);
            return position;
        }
    }
}
