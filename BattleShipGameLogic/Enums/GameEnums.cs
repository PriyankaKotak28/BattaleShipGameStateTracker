using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShipGameLogic.Enums
{
    public class GameEnums
    {
        public enum CellTypes
        {
            Water = 1,
            Undamaged = 2,
            Damaged = 3,
            Sunk = 4
        }

        public enum ShipTypes
        {
            PatrolBoat = 1,
            Submarine = 2,
            Destroyer = 3,
            Battleship = 4,
            Carrier = 5
        }

        public enum Positions
        {
            Vertical = 1,
            Horiztontal = 2
        }

        public enum CommandTypes
        {
            AddShip = 1,
            Attack = 2,
            Status = 3,
            Help = 4
        }
    }
}
