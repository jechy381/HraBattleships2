using GymVod.Battleships.Common;
using GymVod.Battleships.Services.GameServer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Orientation = GymVod.Battleships.Common.Orientation;

namespace BattleshipsGUI
{
    public partial class Form1 : Form
    {
        private GameSettings _gs;

        private (IBattleshipsGame player, List<Ship> ships, Graphics table, Label turn) _player1, _player2, _activePlayer;
        private (IBattleshipsGame player, List<Ship> ships, Graphics table, Label turn) _inactivePlayer
        {
            get
            {
                if (_activePlayer == _player1)
                    return _player2;
                else
                    return _player1;
            }
        }

        // tudy cesta prostě nevede: List<(ShipPosition ship, List<(Position position, bool isHit)> tiles, bool isSunken)> ships

        private bool _shipsVisibile;

        private int _tileWidth, _tileHeight;

        public Form1(IBattleshipsGame player1, IBattleshipsGame player2, GameSettings gs)
        {
            InitializeComponent();
            _player1 = (player1, null, panel1.CreateGraphics(), labP1);
            _player2 = (player2, null, panel2.CreateGraphics(), labP2);
            _gs = gs;

            timer1.Interval = 1000;
        }

        /* tablelayoutpanel je téměř úplně ale ne zcela naprosto na hovno
        private void SetupTable(TableLayoutPanel tab)
        {
            tab.ColumnCount = _gs.BoardWidth;
            tab.RowCount = _gs.BoardHeight;

            var pen = new Pen(Color.Black, 1);
            //var canvas = new Panel() { Location = tab.Location, Width = tab.Width, Height = tab.Height }.CreateGraphics();

            //canvas.DrawRectangle(pen, 0, 0, tab.Width, tab.Height);

            int width = (int)Math.Floor(tab.Width / (double)_gs.BoardWidth);

            tab.ColumnStyles.Clear();
            for (int i = 0; i < _gs.BoardWidth; i++)
            {
                tab.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, width));
                //canvas.DrawLine(pen, i * width, 0, i * width, tab.Height - 1);
            }

            int height = (int)Math.Floor(tab.Height / (double)_gs.BoardHeight);

            tab.RowStyles.Clear();
            for (int i = 0; i < _gs.BoardHeight; i++)
            {
                tab.RowStyles.Add(new RowStyle(SizeType.Absolute, height));
                //canvas.DrawLine(pen, 0, i * height, tab.Width - 1, i * height);
            }

            for (int i = 0; i < tab.ColumnCount; i++)
                for (int j = 0; j < tab.RowCount; j++)
                    tab.Controls.Add(new Label(), i, j);
        }*/

        private void SetupTable(Graphics gr)
        {
            var pen = new Pen(Color.Black, 1);
            gr.Clear(Color.White);

            _tileWidth = (int)Math.Floor(360 / (double)_gs.BoardWidth);
            _tileHeight = (int)Math.Floor(360 / (double)_gs.BoardHeight);

            for (int i = 0; i <= _gs.BoardWidth; i++)
                gr.DrawLine(pen, i * _tileWidth, 0, i * _tileWidth, 360);
            for (int i = 0; i <= _gs.BoardHeight; i++)
                gr.DrawLine(pen, 0, i * _tileHeight, 360, i * _tileHeight);
        }

        private Rectangle GetTile(int x, int y) => new Rectangle(x * _tileWidth + 1, y * _tileHeight + 1, _tileWidth - 1, _tileHeight - 1);

        private void Shoot(object sender, EventArgs e)
        {
            var pos = _activePlayer.player.GetNextShotPosition();

            if (pos.X < 0 || pos.X >= _gs.BoardWidth || pos.Y < 0 || pos.Y >= _gs.BoardHeight)
            {
                timer1.Enabled = false;
                int player;
                if (_activePlayer == _player1)
                    player = 1;
                else
                    player = 2;
                MessageBox.Show(this, $"Hráč {player} je máslo, protože vystřelil mimo hrací pole ({pos.X}, {pos.Y}).", "Chyba při střelbě", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Ship hitShip = null;
            ShipTile hitTile = null;

            foreach (var s in _inactivePlayer.ships)
                foreach (var t in s.Tiles)
                    if (t.Position == pos)
                    {
                        hitShip = s;
                        hitTile = t;
                    }

            if(hitShip != null)
            {
                _inactivePlayer.table.DrawString("x", DefaultFont, Brushes.Black, GetTile(pos.X, pos.Y));

                hitTile.IsHit = true;

                if (hitShip.IsSunken)
                {
                    foreach (var t in hitShip.Tiles)
                        _inactivePlayer.table.FillRectangle(Brushes.Black, GetTile(t.Position.X, t.Position.Y));
                    _activePlayer.player.ShotResult(new ShotResult(pos, true, true));
                }
                else
                    _activePlayer.player.ShotResult(new ShotResult(pos, true, false));
            }
            else
            {
                _inactivePlayer.table.DrawString("0", DefaultFont, Brushes.Black, GetTile(pos.X, pos.Y));
                _activePlayer.player.ShotResult(new ShotResult(pos, false, false));
            }

            _activePlayer = _inactivePlayer;
            _activePlayer.turn.Visible = true;
            _inactivePlayer.turn.Visible = false;

            // TODO počítání kol, tahů
        }

        private void StartGame(object sender, EventArgs e)
        {
            if (PlacingSuccessful(_player1.player.NewGame(_gs), 1, out _player1.ships) && PlacingSuccessful(_player2.player.NewGame(_gs), 2, out _player2.ships))
            {
                SetupTable(_player1.table);
                SetupTable(_player2.table);

                _shipsVisibile = miShowShips.Checked;
                ShowOrHideShips(_player1.ships, _player1.table);
                ShowOrHideShips(_player2.ships, _player2.table);

                _activePlayer = _player1;
                _activePlayer.turn.Visible = true;
                _inactivePlayer.turn.Visible = false;

                timer1.Enabled = miAuto.Checked;
            }
        }

        private void ShowOrHideShips(List<Ship> ships, Graphics gr)
        {
            foreach (var ship in ships)
                foreach (var tile in ship.Tiles)
                    gr.FillRectangle(Brushes.Aqua ,GetTile(tile.Position.X, tile.Position.Y));
        }

        private bool PlacingSuccessful(ShipPosition[] ships, int player, out List<Ship> shipTiles)
        {
            var gb = new Gameboard(_gs.BoardWidth, _gs.BoardHeight);
            shipTiles = new List<Ship>();
            if (!PlacedAllShips(_gs, ships))
            {
                MessageBox.Show(this, $"Hráč {player} je máslo, protože neumístil všechny lodě.", "Chyba při rozestavování lodí", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            try
            {
                gb.PlaceShips(ships);

                foreach (ShipPosition s in ships)
                {
                    Console.WriteLine($"{s.ShipType} {s.Position.X} {s.Position.Y} {s.Orientation}");
                    shipTiles.Add(new Ship(s));
                }
                Console.WriteLine();
            }
            catch (GameOverException e)
            {
                MessageBox.Show(this, $"Hráč {player} je máslo, protože: \n {e.Message}", "Chyba při rozestavování lodí", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"error! {player}");
                return false;
            }
            return true;
        }

        // Václavkův kód, uvolněný pod licencí MIT https://github.com/vaclavek/GymVod.Battleships
        private bool PlacedAllShips(GameSettings gameSettings, ShipPosition[] shipPositions)
        {
            var tempPositions = new List<ShipPosition>();
            tempPositions.AddRange(shipPositions);

            foreach (var shipType in gameSettings.ShipTypes)
            {
                var position = tempPositions.FirstOrDefault(x => x.ShipType == shipType);
                if (position == null)
                {
                    return false;
                }

                tempPositions.Remove(position);
            }

            return true;
        }
    }

    class Ship
    {
        public ShipPosition Position { get; }
        public List<ShipTile> Tiles { get; set; }
        public bool IsSunken
        {
            get
            {
                foreach (var tile in Tiles)
                    if (!tile.IsHit)
                        return false;
                return true;
            }
        }

        public Ship(ShipPosition s)
        {
            Position = s;

            Tiles = new List<ShipTile>();

            for (int i = 0; i < (int)Position.ShipType; i++)
            {
                switch (Position.Orientation)
                {
                    case Orientation.Left:
                        Tiles.Add(new ShipTile(new Position((byte)(Position.Position.X - i), Position.Position.Y), false));
                        break;
                    case Orientation.Right:
                        Tiles.Add(new ShipTile(new Position((byte)(Position.Position.X + i), Position.Position.Y), false));
                        break;
                    case Orientation.Up:
                        Tiles.Add(new ShipTile(new Position(Position.Position.X, (byte)(Position.Position.Y - i)), false));
                        break;
                    case Orientation.Down:
                        Tiles.Add(new ShipTile(new Position(Position.Position.X, (byte)(Position.Position.Y + i)), false));
                        break;
                }
            }
        }
    }

    class ShipTile
    {
        public Position Position { get; }
        public bool IsHit { get; set; }

        public ShipTile(Position pos, bool isHit)
        {
            Position = pos;
            IsHit = isHit;
        }
    }
}
