# BattaleShipGameStateTracker
Single player (1 board, no enemy) battle ship game with state tracker console application in .NET Core 3.1.

## The Task
The task is to implement a Battleship state-tracker for a single player that must support
the following logic:

* Create a board
* Add a battleship to the board
* Take an “attack” at a given position, and report back whether the attack resulted in a hit or a miss
* Return whether the player has lost the game yet (i.e. all battleships are sunk)

### Dependencies
* [.NET Core 3.1](https://www.microsoft.com/net/download)

### Unit Testing
**GameUnitTesting** : In Visual Studio, go to **`Test > Run > All Tests`**

### Setup Project
Project should be ready to run upon pulling the solution from github, should all dependencies requirements are met.

#### 1. Run the project
Run the project from visual studio and follow the instruction on the promt, or see below

#### 2. Instructions

Once the console applicatin is running, the user must type commands follow by the Enter key. See available commands listed below:
* '**/help**' : Provide a list of all commands
* '**/quit**' : Exit the application
* '**status**' : Give the current status of the game (ships on board with details: position, health, damaged, undamaged, sunk...)
* '**addship [x] [y] [orientation] [length]**' : Add a ship on the board, specifying its coordinates (with x: number (no decimal), y: number (no decimal)), orientation ('vertical' or 'hozirontal'), and its length: number (no decimal)
* '**attack [x] [y]**' Fire at specified coordinates (with x: number (no decimal), y: number (no decimal))

