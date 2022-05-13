using BattleShipGameLogic;
using BattleShipGameLogic.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static BattleShipGameLogic.Enums.GameEnums;

namespace GameUnitTesting
{
    [TestClass]
    public class GameTestCases
    {
        [TestMethod]
        public void TestMethod1()
        {
            BoardCell ship1Cell = new BoardCell(5, 6);
            Assert.IsNotNull(ship1Cell);
        }

        [TestMethod]
        public void Create_BoardCell_Expect_CorrectCoordinates()
        {
            BoardCell ship1Cell = new BoardCell(5, 6);
            Assert.AreEqual(5, ship1Cell._Rows);
            Assert.AreEqual(6, ship1Cell._Cols);
        }

        [TestMethod]
        public void Create_Board_Expect_CorrectBoardCreation()
        {
            BattleShipMethods game = new BattleShipMethods(10);
            Assert.IsNotNull(game.GameBoard);
        }

        [TestMethod]
        public void Create_Board_Expect_CorrectRowNumber()
        {
            BattleShipMethods game = new BattleShipMethods(10);
            Assert.AreEqual(game.GameBoard.Count, 10);
        }

        [TestMethod]
        public void Create_Board_Expect_CorrectColNumber()
        {
            BattleShipMethods game = new BattleShipMethods(10);
            for (int i = 0; i != game._BoardSize; ++i)
                Assert.AreEqual(game.GameBoard[i].Count, 10);
        }

        [TestMethod]
        public void Initialize_BoardWithWater_ExpectAllCellsToBeWater()
        {
            BattleShipMethods game = new BattleShipMethods(10);
            for (int i = 0; i != game._BoardSize; ++i)
                for (int j = 0; j != game._BoardSize; ++j)
                    Assert.AreEqual(game.GameBoard[i][j]._CellType, CellTypes.Water);
        }

        [TestMethod]
        public void Create_Ship_Expect_CorrectCreation()
        {
            BoardCell ship1Cell = new BoardCell(5, 6);
            Ships ship = new Ships(ship1Cell, Positions.Horiztontal, (int)ShipTypes.Destroyer);
            Assert.IsNotNull(ship);
        }

        [TestMethod]
        public void Create_Ship_Expect_CorrectHeadPosition()
        {
            BoardCell ship1Cell = new BoardCell(5, 6);
            Ships ship = new Ships(ship1Cell, Positions.Horiztontal, (int)ShipTypes.Destroyer);

            Assert.AreEqual(ship.shipPlacement._Rows, 5);
            Assert.AreEqual(ship.shipPlacement._Cols, 6);
        }

        [TestMethod]
        public void Create_Ship_Expect_CorrectPositions()
        {
            BoardCell ship1Cell = new BoardCell(5, 6);
            Ships ship = new Ships(ship1Cell, Positions.Horiztontal, (int)ShipTypes.Destroyer);
            Assert.AreEqual(ship.shipPosition, Positions.Horiztontal);
        }


        [TestMethod]
        public void Add_VerticalShipToBoard_Expect_CorrectPlacement()
        {
            BattleShipMethods game = new BattleShipMethods(10);

            BoardCell ship1Cell = new BoardCell(5, 6);
            Ships ship = new Ships(ship1Cell, Positions.Horiztontal, (int)ShipTypes.Destroyer);

            Assert.AreEqual(ship.shipPosition, Positions.Horiztontal);
        }

        [TestMethod]
        public void Add_HorizontalShipToBoard_Expect_AddedShip()
        {
            BattleShipMethods game = new BattleShipMethods(10);

            BoardCell ship1Cell = new BoardCell(5, 6);

            game.AddShipOnBoard(ship1Cell, Positions.Horiztontal, (int)ShipTypes.Destroyer);

            Assert.AreEqual(game.Ships.Count, 1);
        }

        [TestMethod]
        public void Add_VerticalShipToBoard_Expect_AddedShip()
        {
            BattleShipMethods game = new BattleShipMethods(10);

            BoardCell ship1Cell = new BoardCell(5, 6);

            game.AddShipOnBoard(ship1Cell, Positions.Vertical, (int)ShipTypes.Destroyer);

            Assert.AreEqual(game.Ships.Count, 1);
        }

        [TestMethod]
        public void Add_TooLongVerticalShipToBoard_Expect_OutOfBoardErrorMessage()
        {
            BattleShipMethods game = new BattleShipMethods(2);

            BoardCell ship1Cell = new BoardCell(1, 1);

            var returnValue = game.AddShipOnBoard(ship1Cell, Positions.Vertical, 15);

            Assert.AreEqual(returnValue, false);
            Assert.AreEqual(game.ErrMsg, "Coordinates for the ship do not match board witdh and height");
        }

        [TestMethod]
        public void Add_TooLongHorizontalShipToBoard_OutOfBoardErrorMessage()
        {
            BattleShipMethods game = new BattleShipMethods(10);

            BoardCell ship1Cell = new BoardCell(5, 6);

            var returnValue = game.AddShipOnBoard(ship1Cell, Positions.Horiztontal, 15);

            Assert.AreEqual(returnValue, false);
            Assert.AreEqual(game.ErrMsg, "Coordinates for the ship do not match board witdh and height");
        }

        [TestMethod]
        public void Add_SeveralValidShips_Expect_CorrectCountAddedShips()
        {
            BattleShipMethods game = new BattleShipMethods(100);

            BoardCell ship1Cell = new BoardCell(1, 1);
            BoardCell ship2Cell = new BoardCell(10, 10);
            BoardCell ship3Cell = new BoardCell(20, 20);
            BoardCell ship4Cell = new BoardCell(30, 30);

            game.AddShipOnBoard(ship1Cell, Positions.Vertical, (int)ShipTypes.Battleship);
            game.AddShipOnBoard(ship2Cell, Positions.Horiztontal, (int)ShipTypes.Destroyer);
            game.AddShipOnBoard(ship3Cell, Positions.Vertical, (int)ShipTypes.Carrier);
            game.AddShipOnBoard(ship4Cell, Positions.Horiztontal, (int)ShipTypes.PatrolBoat);

            Assert.AreEqual(game.Ships.Count, 4);
        }

        [TestMethod]
        public void Add_InvalidShips_Expect_InvalidCoordinatesError()
        {
            BattleShipMethods game = new BattleShipMethods(10);

            BoardCell ship1Cell = new BoardCell(15, 12);
            bool returnValue = game.AddShipOnBoard(ship1Cell, Positions.Vertical, (int)ShipTypes.Battleship);

            Assert.AreEqual(returnValue, false);
            Assert.AreEqual(game.ErrMsg, "Coordinates for the ship do not match board witdh and height");
        }

        [TestMethod]
        public void Add_ShipOnAlreadyPlacedShip_Expect_NoCellAvailableError()
        {
            BattleShipMethods game = new BattleShipMethods(10);

            BoardCell ship1Cell = new BoardCell(1, 1);
            BoardCell ship2Cell = new BoardCell(1, 1);

            game.AddShipOnBoard(ship1Cell, Positions.Vertical, (int)ShipTypes.Destroyer);
            bool returnValue = game.AddShipOnBoard(ship2Cell, Positions.Vertical, (int)ShipTypes.Destroyer);

            Assert.AreEqual(returnValue, false);
            Assert.AreEqual(game.ErrMsg, "Not all cells are available for this ship to be place at these vertical coordinates");
        }

        [TestMethod]
        public void Add_Ship_WhenShipOverlay_Expect_NoCellAvailableError()
        {
            BattleShipMethods game = new BattleShipMethods(50);

            BoardCell ship1Cell = new BoardCell(20, 20);
            BoardCell ship2Cell = new BoardCell(18, 22);

            game.AddShipOnBoard(ship1Cell, Positions.Horiztontal, (int)ShipTypes.Carrier);
            bool returnValue = game.AddShipOnBoard(ship2Cell, Positions.Vertical, (int)ShipTypes.Carrier);

            Assert.AreEqual(returnValue, false);
            Assert.AreEqual(game.ErrMsg, "Not all cells are available for this ship to be place at these vertical coordinates");
        }

        [TestMethod]
        public void Add_Ship_OnAlreadySunkShip_Expect_NoCellAvailableError()
        {
            BattleShipMethods game = new BattleShipMethods(50);

            BoardCell ship1Cell = new BoardCell(20, 20);
            BoardCell ship2Cell = new BoardCell(18, 22);

            game.AddShipOnBoard(ship1Cell, Positions.Horiztontal, (int)ShipTypes.Carrier);
            game.AttackShip(20, 20);
            game.AttackShip(20, 21);
            game.AttackShip(20, 22);
            game.AttackShip(20, 23);
            game.AttackShip(20, 24);
            //ship1 is now sunk
            Assert.AreEqual(game.Ships[0].IsSunk(), true);
            bool returnValue = game.AddShipOnBoard(ship2Cell, Positions.Vertical, (int)ShipTypes.Carrier);

            Assert.AreEqual(returnValue, false);
            Assert.AreEqual(game.ErrMsg, "Not all cells are available for this ship to be place at these vertical coordinates");
        }

        [TestMethod]
        public void Attack_Ship_Expect_ShipToBeDamaged()
        {
            BattleShipMethods game = new BattleShipMethods(10);
            BoardCell ship1Cell = new BoardCell(1, 1);
            int x = 1;
            int y = 2;

            game.AddShipOnBoard(ship1Cell, Positions.Horiztontal, (int)ShipTypes.Carrier);
            int shipHealth = game.Ships[0].shipLife;

            game.AttackShip(x, y);

            Assert.AreEqual(game.Ships[0].shipLife, shipHealth - 1);
        }

        [TestMethod]
        public void Attack_Ship_Expect_CellUpdated()
        {
            BattleShipMethods game = new BattleShipMethods(10);
            BoardCell ship1Cell = new BoardCell(1, 1);
            int x = 1;
            int y = 2;

            game.AddShipOnBoard(ship1Cell, Positions.Horiztontal, (int)ShipTypes.Carrier);
            int shipHealth = game.Ships[0].shipLife;

            game.AttackShip(x, y);

            Assert.AreEqual(game.GameBoard[x][y]._CellType, CellTypes.Damaged);
        }

        [TestMethod]
        public void Attack_Ship_Expect_CellAroundDamageToBeUndamaged()
        {
            BattleShipMethods game = new BattleShipMethods(10);
            BoardCell ship1Cell = new BoardCell(1, 1);
            int x = 1;
            int y = 2;

            game.AddShipOnBoard(ship1Cell, Positions.Horiztontal, (int)ShipTypes.Carrier);
            int shipHealth = game.Ships[0].shipLife;

            game.AttackShip(x, y);

            Assert.AreEqual(game.GameBoard[x][y - 1]._CellType, CellTypes.Undamaged);
            Assert.AreEqual(game.GameBoard[x][y]._CellType, CellTypes.Damaged);
            Assert.AreEqual(game.GameBoard[x][y + 1]._CellType, CellTypes.Undamaged);
        }

        [TestMethod]
        public void Attack_Ship_Expect_ShipToBeDamagedButNotSunk()
        {
            BattleShipMethods game = new BattleShipMethods(10);
            BoardCell ship1Cell = new BoardCell(1, 1);
            int x = 1;
            int y = 2;

            game.AddShipOnBoard(ship1Cell, Positions.Horiztontal, (int)ShipTypes.Carrier);
            int shipHealth = game.Ships[0].shipLife;

            game.AttackShip(x, y);

            Assert.AreEqual(game.Ships[0].IsSunk(), false);
        }

        [TestMethod]
        public void Attack_AllShipCells_Expect_ShipToBeSunk()
        {
            BattleShipMethods game = new BattleShipMethods(10);
            BoardCell ship1Cell = new BoardCell(1, 1);
            int x = 1;
            int y = 1;

            game.AddShipOnBoard(ship1Cell, Positions.Horiztontal, (int)ShipTypes.Carrier);
            int shipHealth = game.Ships[0].shipLife;

            // Carrier has 5 cells
            game.AttackShip(x, y);
            game.AttackShip(x, y + 1);
            game.AttackShip(x, y + 2);
            game.AttackShip(x, y + 3);
            game.AttackShip(x, y + 4);

            Assert.AreEqual(game.Ships[0].IsSunk(), true);
        }

        [TestMethod]
        public void Attack_WaterCell_Expect_FailAttackAndWaterCell()
        {
            BattleShipMethods game = new BattleShipMethods(10);
            BoardCell ship1Cell = new BoardCell(1, 1);
            int x = 8;
            int y = 9;

            game.AddShipOnBoard(ship1Cell, Positions.Horiztontal, (int)ShipTypes.Destroyer);

            game.AttackShip(x, y);

            Assert.AreEqual(game.GameBoard[x][y]._CellType, CellTypes.Water);
        }

        [TestMethod]
        [ExpectedException(typeof(AlreadyDamagedShipException))]
        public void Attack_AlreadyDamagedShipCell_Expect_FailAttackWith_AlreadyDamagedShipCellException()
        {
            BattleShipMethods game = new BattleShipMethods(10);
            BoardCell ship1Cell = new BoardCell(1, 1);
            int x = 1;
            int y = 1;

            game.AddShipOnBoard(ship1Cell, Positions.Horiztontal, (int)ShipTypes.Destroyer);

            game.AttackShip(x, y); // Here we hit the ship for the first time, updating the cell to 'damaged'
            game.AttackShip(x, y); // As the ship is already damage at this coordinates, exception is expected
        }

        [TestMethod]
        [ExpectedException(typeof(ShipAlreadySunkException))]
        public void Attack_AlreadySunkShipCell_Expect_FailAttackWith_ShipAlreadySunkException()
        {
            BattleShipMethods game = new BattleShipMethods(10);
            BoardCell ship1Cell = new BoardCell(1, 1);
            int x = 1;
            int y = 1;

            game.AddShipOnBoard(ship1Cell, Positions.Horiztontal, (int)ShipTypes.Carrier);

            // Carrier has 5 cells
            game.AttackShip(x, y);
            game.AttackShip(x, y + 1);
            game.AttackShip(x, y + 2);
            game.AttackShip(x, y + 3);
            game.AttackShip(x, y + 4);
            // by this point the ship is sunk

            game.AttackShip(x, y + 4); // expect exception to be thrown here
        }

        [TestMethod]
        [ExpectedException(typeof(AttackInvalidCoordinatesException))]
        public void AttackWith_InvalidCoordinates_Expect_FailAttackWith_AttackCoordinatesInvalidException()
        {
            BattleShipMethods game = new BattleShipMethods(10);
            BoardCell ship1Cell = new BoardCell(1, 1);
            int x = 15;
            int y = 22;

            game.AddShipOnBoard(ship1Cell, Positions.Horiztontal, (int)ShipTypes.Destroyer);

            game.AttackShip(x, y);
        }
    }
}
