using static BattleShipGameLogic.Enums.GameEnums;

namespace BattleShipGameLogic.ViewModels
{
    public class Ships
    {
        public int shipLife { get; set; }
        public int _shipSize { get; set; }
        public BoardCell shipPlacement { get; set; }
        public Positions shipPosition { get; set; }

        public Ships(BoardCell cell, Positions pos, int shipSize)
        {
            shipPlacement = cell;
            shipPosition = pos;
            shipLife = shipSize;
            _shipSize = shipSize;
        }

        public bool IsDamaged()
        {
            shipLife--;
            return IsSunk();
        }

        public bool IsSunk()
        {
            return shipLife == 0 ? true : false;
        }
    }
}
