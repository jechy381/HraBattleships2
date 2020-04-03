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

        private HashSet<Position> misses = new HashSet<Position>();
        private HashSet<Position> hits = new HashSet<Position>();

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
        public static void ExludeAdjancentPositionsToSunkenShip(HashSet<Position> hits, Orientation orientation)
        {
            int pocetPrvku = hits.Count;
            var positions = new HashSet<Position>();
            positions.UnionWith(hits);
            //pokud je lod 1 pole
            if (pocetPrvku == 1)
            {
                var pos = positions.First();
                hits.Add(new Position((byte)(pos.X + 1), (byte)(pos.Y)));
                hits.Add(new Position((byte)(pos.X - 1), (byte)(pos.Y)));
                hits.Add(new Position((byte)(pos.X), (byte)(pos.Y + 1)));
                hits.Add(new Position((byte)(pos.X), (byte)(pos.Y - 1)));
            }
            //horizontalni lod
            if (orientation == Orientation.Left || orientation == Orientation.Right)
            {
                byte minX = 0, maxX = 0;
                foreach (Position pos in positions)
                {
                    if (minX == 0 || minX > pos.X)
                    {
                        minX = pos.X;
                    }
                    if (maxX == 0 || maxX < pos.X)
                    {
                        maxX = pos.X;
                    }
                    //prida okolni policka u kazde X souradnice
                    hits.Add(new Position(pos.X, (byte)(pos.Y - 1)));
                    hits.Add(new Position(pos.X, (byte)(pos.Y + 1)));
                }
                //prida limitni policka lodi
                hits.Add(new Position((byte)(minX - 1), positions.First().Y));
                hits.Add(new Position((byte)(maxX + 1), positions.First().Y));
            }
            //vertikalni lod
            if (orientation == Orientation.Down || orientation == Orientation.Up)
            {
                byte minY = 0, maxY = 0;
                foreach (Position pos in positions)
                {
                    if (minY == 0 || minY > pos.Y)
                    {
                        minY = pos.Y;
                    }
                    if (maxY == 0 || maxY < pos.Y)
                    {
                        maxY = pos.Y;
                    }
                    //prida okolni policka u kazde Y souradnice
                    hits.Add(new Position((byte)(pos.X - 1), pos.Y));
                    hits.Add(new Position((byte)(pos.X + 1), pos.Y));
                }
                //prida limitni policka lodi
                hits.Add(new Position(positions.First().X, (byte)(minY - 1)));
                hits.Add(new Position(positions.First().X, (byte)(maxY + 1)));
            }
        }

        

        public Position GetNextShotPosition()
        {
            if (gameState == GameState.Seek)
            {
                return GetRandomPosition(misses);
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
                gameState = GameState.Destroy;
                hits.Add(shotResult.Position);
                if (firstShot == null)
                {
                    firstShot = shotResult.Position;
                }
                if (shotResult.ShipSunken)
                {
                    ExludeAdjancentPositionsToSunkenShip(hits, orientation);
                    gameState = GameState.Seek;
                    horizontal = true;
                    firstShot = null; //Za předpokladu že tohle bude fungovat, by to mělo jít 
                    orientation = Orientation.Right;
                    hits.UnionWith(misses); //presune policka s trefenou lodi do misses
                    hits.Clear(); //uvolni hits pro dalsi lod
                }
            }
            else
            {
                misses.Add(shotResult.Position);
            }
        }


        Random rnd = new Random();
        public Position GetRandomPosition(HashSet<Position> used)
        {
            HashSet<Position> exlude = new HashSet<Position>();
            exlude.UnionWith(used);

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
