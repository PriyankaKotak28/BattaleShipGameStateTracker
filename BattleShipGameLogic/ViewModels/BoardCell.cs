using static BattleShipGameLogic.Enums.GameEnums;

namespace BattleShipGameLogic.ViewModels
{
    public class BoardCell
    {
        public BoardCell(int rows, int cols)
        {
            this._Rows = rows;
            this._Cols = cols;
        }
        public int _Rows { get; set; }
        public int _Cols { get; set; }
        public int _ShipLength { get; set; }
        public CellTypes _CellType { get; set; }

        public void ResetCell(CellTypes type)
        {
            this._CellType = type;
            this._ShipLength = -1;
        }
    }
}
