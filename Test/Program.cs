using GymVod.Battleships.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Test;

namespace BattleshipsGUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // zde zvol hráče
            IBattleshipsGame hrac1 = new HraBattleships2.Hrac();
            IBattleshipsGame hrac2 = new DumbLodiiis.DumbBot();
            Debug.WriteLine("neco");
            var gs = new GameSettings(20, 20, new ShipType[]
            {
                ShipType.Carrier,                       // 1x
                ShipType.Battleship,                    // 1x
                ShipType.Cruiser, ShipType.Cruiser,     // 2x
                ShipType.Destroyer, ShipType.Destroyer, // 2x
                ShipType.Submarine, ShipType.Submarine, // 2x
            }); // mělo by to být stejné jako Václavkova stránka, upravuj dle nálady, nestěžuj si pokud to rozbiješ
            // záloha Václavkovo hodnot: var gs = new GameSettings(20, 20, new ShipType[] { ShipType.Carrier, ShipType.Battleship, ShipType.Cruiser, ShipType.Cruiser, ShipType.Destroyer, ShipType.Destroyer, ShipType.Submarine, ShipType.Submarine, });

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(hrac1, hrac2, gs));
        }
    }
}
