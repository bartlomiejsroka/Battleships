using Battleship.Enums;
using Battleship.InputRetrievers;
using Battleship.RandomGenerators;
using Battleship.Validators;
using Battleship.ValueObjects;

namespace Battleship.GameDispatchers
{
    internal interface IPlayerDispatcher
    {
        public bool HitComputerGridAndMakeComputerMove(IGameGrid playerGrid, IGameGrid computerGrid);
    }

    internal class PlayerDispatcher : IPlayerDispatcher
    {
        private IUserInputRetriever _userInputRetriever;
        private IRandomCoordniatesGenerator _randomCoordniatesGenerator;

        public PlayerDispatcher()
        {
            _userInputRetriever = new UserInputRetriever();
            _randomCoordniatesGenerator = new RandomCoordinatesGenerator();
        }

        public bool HitComputerGridAndMakeComputerMove(IGameGrid playerGrid, IGameGrid computerGrid)
        {
            var coordinatesString = _userInputRetriever.GetInput();

            var coordinates = coordinatesString?.TranslateToCoordinates();
            if (coordinates == null)
            {
                Console.WriteLine(ValidationResultMessages.WrongCoordinates);
                return false;
            }

            var result = computerGrid.HitCell(coordinates);

            if (!result.CanHit)
            {
                Console.WriteLine(result.ValidationResult);
                return false;
            }

            Console.WriteLine(computerGrid.LastHitShip == null ? "You missed" : $"You hit {(ShipSize)computerGrid.LastHitShip.Size}");

            var validComputerMove = false;
            while (!validComputerMove)
            {
                (validComputerMove, _) = playerGrid.HitCell(_randomCoordniatesGenerator.GetRandomCoordiantes(playerGrid.Size));
            }
            Console.WriteLine(playerGrid.LastHitShip == null ? "Computer missed" : $"Computer hit your {(ShipSize)playerGrid.LastHitShip.Size}");

            return true;
        }

        internal void ChangeUserInputRetriever(IUserInputRetriever userInputRetriever)
        {
            _userInputRetriever = userInputRetriever;
        }

        internal void ChangeRandomCoordniatesGenerator(IRandomCoordniatesGenerator randomCoordniatesGenerator)
        {
            _randomCoordniatesGenerator = randomCoordniatesGenerator;
        }
    }
}
