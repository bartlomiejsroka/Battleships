// See https://aka.ms/new-console-template for more information
using Battleship.GameDispatchers;

var newGame = true;
while (newGame)
{
    Console.WriteLine("Welcome to battleship game");
    var gameDispatcher = new GameDispatcher(2, 1, 10);
    gameDispatcher.Play();
    Console.WriteLine("New game? Y/N");
    newGame = Console.ReadLine()?.ToUpper() == "Y";
}