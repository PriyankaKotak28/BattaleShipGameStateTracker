using BattleShipGameLogic.ViewModels;
using System;
using System.Collections.Generic;
using static BattleShipGameLogic.Enums.GameEnums;

namespace BattleShipGameLogic
{
    public class BattleShipMethods
    {
        public List<List<BoardCell>> GameBoard { get; set; } 
            = new List<List<BoardCell>>();
        public List<Ships> Ships { get; set; }
        public int _BoardSize { get; set; }
        public string ErrMsg { get; set; } = string.Empty;
        public string ShipStatusUpdate { get; set; }
        public bool IsGameOver { get; set; }
        public BattleShipMethods(int boardSize)
        {
            Ships = new List<Ships>();
            SetupBoard(boardSize);
        }

        #region SetupBoard
        private void SetupBoard(int boardSize)
        {
            _BoardSize = boardSize;
            GameBoard = new List<List<BoardCell>>();
            for (int i = 0; i != _BoardSize; ++i)
            {
                GameBoard.Add(new List<BoardCell>());
                for (int j = 0; j != _BoardSize; ++j)
                    GameBoard[i].Add(new BoardCell(i, j));
            }
            InitialBoardCells();
        }
        private void InitialBoardCells()
        {
            for (int i = 0; i != _BoardSize; ++i)
                for (int j = 0; j != _BoardSize; ++j)
                    GameBoard[i][j].ResetCell(CellTypes.Water);
        }
        #endregion

        #region AddShip

        public bool AddShipOnBoard(BoardCell cell, Positions orientation, int length)
        {
            bool isAdded = false;
            try
            {
                Ships objShip = new Ships(cell, orientation, length);
                int remainingLength = objShip.shipLife;
                if (objShip.shipPosition == Positions.Horiztontal) isAdded = CheckAddShipHorizontal(objShip);
                else isAdded = CheckAddShipVertical(objShip);
                if (isAdded) Ships.Add(objShip);
            }
            catch (Exception ex)
            {
                ErrMsg = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                    ErrMsg = ex.InnerException.Message;
            }
            return isAdded;
        }
        private bool CheckAddShipHorizontal(Ships ship)
        {
            try
            {
                int row = ship.shipPlacement._Rows;
                int col = ship.shipPlacement._Cols;
                int shipSize = ship._shipSize;
                if (IsPlacementPossible(ship))
                {
                    for (int currentCol = col; shipSize != 0; ++currentCol)
                    {
                        GameBoard[row][currentCol]._CellType = CellTypes.Undamaged;
                        GameBoard[row][currentCol]._ShipLength = Ships.Count;
                        --shipSize;
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                ErrMsg = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                    ErrMsg = ex.InnerException.Message;
            }
            return false;
        }

        private bool CheckAddShipVertical(Ships ship)
        {
            try
            {
                int row = ship.shipPlacement._Rows;
                int col = ship.shipPlacement._Cols;
                int shipSize = ship._shipSize;
                if (IsPlacementPossible(ship))
                {
                    for (int currentCol = col; shipSize != 0; ++currentCol)
                    {
                        GameBoard[row][currentCol]._CellType = CellTypes.Undamaged;
                        GameBoard[row][currentCol]._ShipLength = Ships.Count;
                        --shipSize;
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                ErrMsg = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                    ErrMsg = ex.InnerException.Message;
            }
            return false;
        }

        private bool IsPlacementPossible(Ships ship)
        {
            var IsPoss = false;
            try
            {
                if (ship != null)
                {
                    int row = ship.shipPlacement._Rows;
                    int col = ship.shipPlacement._Cols;
                    int shipLength = ship._shipSize;

                    if (ship.shipPosition == Positions.Horiztontal)
                    {
                        for (int currentCol = col; shipLength != 0; currentCol++)
                        {
                            if (!IsCoordinatesAvailable(row, currentCol))
                            {
                                if (string.IsNullOrEmpty(ErrMsg))
                                    ErrMsg = "Not all cells are available for this ship to be place at these horizontal coordinates";
                                return IsPoss;
                            }
                            --shipLength;
                        }
                        return true;
                    }
                    else
                    {
                        for (int currentRow = row; shipLength != 0; currentRow++)
                        {
                            if (!IsCoordinatesAvailable(currentRow, col))
                            {
                                if (string.IsNullOrEmpty(ErrMsg))
                                    ErrMsg = "Not all cells are available for this ship to be place at these vertical coordinates";
                                return IsPoss;
                            }
                            --shipLength;
                        }
                        return IsPoss = true;
                    }
                }
                else ErrMsg = "Please enter ship";
            }
            catch (Exception e)
            {
                ErrMsg = "There was an internal issue and the ship cannot be added. Please refer to execption: " + e.Message;
            }
            return IsPoss;
        }
        #endregion

        #region AttackShip
        public CellTypes AttackShip(int x, int y)
        {
            if (IsValidCoordinates(x, y))
            {
                switch (GameBoard[x][y]._CellType)
                {
                    case CellTypes.Water:
                        return CellTypes.Water;

                    case CellTypes.Undamaged:
                        var attackedShip = Ships[GameBoard[x][y]._ShipLength];
                        attackedShip.shipLife = attackedShip.shipLife - 1;

                        if (attackedShip.shipLife > 0)
                        {
                            GameBoard[x][y]._CellType = CellTypes.Damaged;
                            return CellTypes.Damaged;
                        }
                        else
                        {
                            GameBoard[x][y]._CellType = CellTypes.Sunk;
                            IsGameOver = CheckGameOver();
                            return CellTypes.Sunk;
                        }

                    case CellTypes.Damaged:
                        ShipStatusUpdate = "Ship is already damaged at these coordinates";
                        throw new AlreadyDamagedShipException(ShipStatusUpdate);

                    case CellTypes.Sunk:
                        ShipStatusUpdate = "Ship at these coordinates is already sunk";
                        throw new ShipAlreadySunkException(ShipStatusUpdate);

                    default:
                        throw new Exception("There was an error with the attack");
                }
            }
            else throw new AttackInvalidCoordinatesException();
        }
        #endregion

        #region ShipList
        public string GetShipsList()
        {
            string shipList = string.Empty;
            for (var i = 0; i != Ships.Count; i++)
            {
                string status = string.Empty;
                if (Ships[i].shipLife == Ships[i]._shipSize) status = "Unadamaged";
                else if (Ships[i].shipLife == 0) status = "Sunk";
                else status = "Damaged";

                var position = Ships[i].shipPosition == Positions.Horiztontal ? "Horizontal" : "Vertical";

                shipList += String.Format("Ship #{0} - Head Position: [{1}][{2}] - Health:{3}/{4} - Placement: {5} - Status: {6}\n",
                    i, Ships[i].shipPlacement._Rows, Ships[i].shipPlacement._Cols, Ships[i].shipLife,
                    Ships[i]._shipSize, position, status);
            }

            return shipList;
        }
        #endregion

        #region CommonPrivateMethods
        private bool CheckGameOver()
        {
            int shipsAlive = 0;

            foreach (Ships ship in Ships)
            {
                if (!ship.IsSunk())
                    shipsAlive++;
            }
            return shipsAlive == 0 ? true : false;
        }
        private bool IsCoordinatesAvailable(int row, int col)
        {
            if (IsValidCoordinates(row,col))
                return (GameBoard[row][col]._ShipLength == -1) ? true : false;
            else
            {
                ErrMsg = "Coordinates for the ship do not match board witdh and height";
                return false;
            }
        }
        private bool IsValidCoordinates(int row, int col)
        {
            var isValid = false;
            if (row < _BoardSize && col < _BoardSize && row >= 0 && col >= 0) return isValid = true;
            return isValid;
        }

        public string GetShipStatus()
        {
            string status;
            int shipsAlive = 0;
            int shipsSunk = 0;
            foreach (Ships ship in Ships)
            {
                if (!ship.IsSunk())
                    shipsAlive++;
                else
                    shipsSunk++;
            }

            if (shipsAlive == 0 && shipsSunk == 0) status = "You currently have no ship placed on the board. Please add at least one ship to continue.";
            else if (shipsAlive == 0) status = "All of your ships sunk. GAME OVER...";
            else
            {
                status = String.Format("You currently have {0} ship(s) alive, and {1} ship(s) sunk\n", shipsAlive, shipsSunk);
                status += "\nShips List:\n" + GetShipsList();
            }
            return status;
        }

        #endregion
    }
}
