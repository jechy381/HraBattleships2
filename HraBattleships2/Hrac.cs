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

        Orientation orientation = Orientation.Right;
        public bool horizontal = true;
        Position firstShot = null;

        public ShipPosition[] NewGame(GameSettings gameSettings)
        {
            height = gameSettings.BoardHeight;
            width = gameSettings.BoardWidth;

            ExludePositions(misses, width);
            var shipPositions = new List<ShipPosition>();

            shipPositions.Add(new ShipPosition(ShipType.Submarine, new Position(1, 2), Orientation.Right));
            shipPositions.Add(new ShipPosition(ShipType.Destroyer, new Position(12, 4), Orientation.Right));
            shipPositions.Add(new ShipPosition(ShipType.Cruiser, new Position(4, 5), Orientation.Down));
            shipPositions.Add(new ShipPosition(ShipType.Battleship, new Position(8, 10), Orientation.Down));
            shipPositions.Add(new ShipPosition(ShipType.Carrier, new Position(1, 12), Orientation.Right));
            return shipPositions.ToArray();
        }
        //probehne na startu
        public void ExludePositions(HashSet<Position> exlude, int width)
        {
            for (int i = 0; i <= width; i++)
            {
                exlude.Add(new Position(0, (byte)i));
                exlude.Add(new Position(15, (byte)i));
                exlude.Add(new Position((byte)i, 0));
                exlude.Add(new Position((byte)i, 15));
            }
        }
        public void ExludeAdjacentToSunkenShip()
        {

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
                if (horizontal == true)
                {
                    return ModeDamageHorizontal(firstShot.X, firstShot.Y);
                }
                else
                {
                    return ModeDamageVertical(firstShot.X, firstShot.Y);
                }
            }
        }


        public void ShotResult(ShotResult shotResult)
        {
            if (shotResult.Hit)
            {
                hits.Add(shotResult.Position);
                if (firstShot == null)
                {
                    firstShot = shotResult.Position;
                }
                if (shotResult.ShipSunken)
                {
                    gameState = GameState.Seek;
                    horizontal = true;
                    firstShot = null; //Za předpokladu že tohle bude fungovat, by to mělo jít 
                    orientation = Orientation.Right;                    
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

        public Position ModeDamageHorizontal(byte x, byte y)
        {
            Position poleVpravo = new Position(x++, y);
            Position poleVlevo = new Position(x--, y);
            Position poleNahore = new Position(x, y++);
            Position poleDole = new Position(x, y--);

            if (orientation == Orientation.Right)
            {
                if (hits.Contains(poleVpravo))
                {
                    x++;
                    misses.Add(poleNahore);
                    misses.Add(poleDole);
                    return ModeDamageHorizontal(x, y);
                }
                else if (misses.Contains(poleVpravo))
                {
                    orientation = Orientation.Left;
                    return ModeDamageHorizontal(x, y);
                }
                else
                {
                    return new Position(x, y);
                }
            }
            else
            {
                if (hits.Contains(poleVlevo))
                {
                    x--;
                    misses.Add(poleNahore);
                    misses.Add(poleDole);
                    return ModeDamageHorizontal(x, y);
                }
                else if (misses.Contains(poleVlevo))
                {
                    orientation = Orientation.Up;
                    horizontal = false;
                    return ModeDamageVertical(x, y);
                }
                else
                {
                    return new Position(x, y);
                }
            }
        }
        public Position ModeDamageVertical(byte x, byte y)
        {
            Position poleVpravo = new Position(x++, y);
            Position poleVlevo = new Position(x--, y);
            Position poleNahore = new Position(x, y++);
            Position poleDole = new Position(x, y--);

            if (orientation == Orientation.Up)
            {
                if (hits.Contains(poleNahore))
                {
                    y++;
                    misses.Add(poleVpravo);
                    misses.Add(poleVlevo);
                    return ModeDamageVertical(x, y);
                }
                else if (misses.Contains(poleNahore))
                {
                    orientation = Orientation.Down;
                    return ModeDamageVertical(x, y);
                }
                else
                {
                    return new Position(x, y);
                }
            }
            else
            {
                if (hits.Contains(poleDole))
                {
                    y--;
                    misses.Add(poleVpravo);
                    misses.Add(poleVlevo);
                    return ModeDamageVertical(x, y);
                }
                else
                {
                    return new Position(x, y);
                }
            }
        }
    }
}
