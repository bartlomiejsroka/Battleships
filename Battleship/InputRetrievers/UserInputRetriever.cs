namespace Battleship.InputRetrievers
{
    internal interface IUserInputRetriever
    {
        string? GetInput();
    }

    internal class UserInputRetriever : IUserInputRetriever
    {
        public string? GetInput()
        {
            return Console.ReadLine();
        }
    }
}
