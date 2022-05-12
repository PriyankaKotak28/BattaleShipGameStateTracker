using BattleShipGameLogic;
using System;

namespace BattaleShipGameStateTracker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello Players! Welcome to Flare Battleship Ground.");
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine("Please enter Size of board : eg 10");
            var boardSize = Console.ReadLine();
            if (!string.IsNullOrEmpty(boardSize) && Convert.ToInt32(boardSize) > 0)
            {
                var cmd = ""; var msgStatus = ""; var isQuit = false;
                BattleShipMethods shipGame = new BattleShipMethods(Convert.ToInt32(boardSize));
                ExecuteGameCommands commands = new ExecuteGameCommands();
                Console.WriteLine(string.Format("A battleship board of {0}x{0} has been created.", boardSize));
                Console.WriteLine("Before starting game please read the below commands for your help.");
                HelpCommandList();
                while (!isQuit)
                {
                    cmd = Console.ReadLine();
                    switch (cmd)
                    {
                        case "/help":
                            HelpCommandList();
                            break;
                        case "/quit":
                            Console.WriteLine("Thank you for playing. Now exiting...");
                            isQuit = true;
                            break;
                        default:
                            msgStatus = commands.ExecuteBattleshipGameCommand(shipGame, cmd.Split(' '));
                            Console.WriteLine(msgStatus);
                            break;
                    }
                }
            }
            else Console.WriteLine("Please enter valid size of board.");
        }

        private static void HelpCommandList()
        {
            Console.WriteLine("Commands list:\n");
            Console.WriteLine("To add ship : 'addship [x] [y] [orientation] [length]'");
            Console.WriteLine("with x: number (no decimal), y: number (no decimal)");
            Console.WriteLine("orientation: 'vertical' or 'hozirontal'(without ''), length: number (no decimal)\n");
            Console.WriteLine("To attack on ship : 'attack [x] [y]'");
            Console.WriteLine("with x: number (no decimal), y: number (no decimal)\n");
            Console.WriteLine("'status' for current game status\n");
            Console.WriteLine("'/help' for commands list\n");
            Console.WriteLine("'/quit' to exit the game\n");
            Console.WriteLine("Please consult Read.me for more information\n");
        }
    }
}
