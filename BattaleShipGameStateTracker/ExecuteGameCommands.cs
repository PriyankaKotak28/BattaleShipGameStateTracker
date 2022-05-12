using BattleShipGameLogic;
using BattleShipGameLogic.ViewModels;
using System;
using static BattleShipGameLogic.Enums.GameEnums;

namespace BattaleShipGameStateTracker
{
    public class ExecuteGameCommands
    {
        public CommandTypes ExtractCommand(string[] cmds)
        {
            if (cmds.Length >= 1 && !string.IsNullOrEmpty(cmds[0]))
            {
                switch (cmds[0].ToLower())
                {
                    case "addship":
                        if (ValidateAddShipArgument(cmds, 5)) return CommandTypes.AddShip;
                        else goto default;

                    case "attack":
                        if (ValidateAttackArgument(cmds, 3)) return CommandTypes.Attack;
                        else goto default;

                    case "status": return CommandTypes.Status;

                    default: return CommandTypes.Help;
                }
            }
            return CommandTypes.Help;
        }

        private bool ValidateAddShipArgument(string[] cmdArgs, int index)
        {
            if (cmdArgs.Length == index)
            {
                if (!int.TryParse(cmdArgs[1], out int x))
                    return false;

                if (!int.TryParse(cmdArgs[2], out int y))
                    return false;

                switch (cmdArgs[3].ToLower())
                {
                    case "vertical":
                        break;
                    case "horizontal":
                        break;
                    default:
                        return false;
                }

                if (!int.TryParse(cmdArgs[4], out int length))
                    return false;
                return true;
            }
            else return false;
        }
        private bool ValidateAttackArgument(string[] cmdArgs, int index)
        {
            if (cmdArgs.Length == index)
            {
                bool isNumeric = false;
                for (int i = 1; i < index; i++)
                {
                    isNumeric = int.TryParse(cmdArgs[i], out int x);
                    if (isNumeric == false)
                        return false;
                }
                return true;
            }
            else return false;
        }


        public string ExecuteBattleshipGameCommand(BattleShipMethods game, 
            string[] command)
        {
            CommandTypes cmdType = ExtractCommand(command);
            string updateMessage = "\nYour command was invalid. Please type \\help for command details";
            try
            {
                switch (cmdType)
                {
                    case CommandTypes.AddShip:
                        updateMessage = "\n" + ExecuteAddShipCommand(game, command);
                        break;
                    case CommandTypes.Attack:
                        updateMessage = "\n" + ExecuteAttackCommand(game, command);
                        break;
                    case CommandTypes.Status:
                        updateMessage = "\n" + ExecuteGetStatusCommand(game);
                        break;
                    case CommandTypes.Help:
                        goto default;
                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                updateMessage = "There was an internal error. Please refer to the following message:" + e.Message;
                updateMessage += "\n Please type \\help for command details";
            }
            updateMessage += "\n";
            return updateMessage;
        }
        private static string ExecuteAddShipCommand(BattleShipMethods game, 
            string[] command)
        {
            string messageUpdate = string.Empty;
            int x = Convert.ToInt32(command[1]);
            int y = Convert.ToInt32(command[2]);
            Positions pos = command[3].ToLower() == "vertical" ? 
                Positions.Vertical : Positions.Horiztontal;
            int length = Convert.ToInt32(command[4]);
            BoardCell shipCell = new BoardCell(x, y);
            var isPlaced = game.AddShipOnBoard(shipCell, pos, length);
            if (isPlaced) messageUpdate = "Your ship has been added to the board";
            else messageUpdate = game.ErrMsg;
            return messageUpdate;
        }
        private static string ExecuteAttackCommand(BattleShipMethods game,
           string[] command)
        {
            string messageUpdate = string.Empty;
            int x = Convert.ToInt32(command[1]);
            int y = Convert.ToInt32(command[2]);

            try
            {
                var resultType = game.AttackShip(x, y);
                switch (resultType)
                {
                    case CellTypes.Damaged:
                        messageUpdate = "Attack succesful. Ship has been hit at x:" + x + ", y:" + y + ".";
                        break;
                    case CellTypes.Water:
                        messageUpdate = "Missed! There is no ship at x:" + x + ", y:" + y + ".";
                        break;
                    case CellTypes.Sunk:
                        messageUpdate = "That's a hit! The battle ship has sunk!";
                        break;
                }
            }
            catch (Exception ex)
            {
                messageUpdate = ex.Message;
                if(ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                    messageUpdate = ex.InnerException.Message;
            }
            return messageUpdate;
        }

        private static string ExecuteGetStatusCommand(BattleShipMethods game)
        {
            string messageUpdate = game.GetShipStatus();
            return messageUpdate;
        }
    }
}
