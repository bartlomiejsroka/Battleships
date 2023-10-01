using Battleship.Enums;
using Battleship.InputRetrievers;
using Battleship.Validators;
using Battleship.ValueObjects;

namespace Battleship.GameDispatchers
{
    internal interface IShipDispatcher
    {
        public bool PlaceHumanPlayerShip(IGameGrid gameGrid, ShipSize shipSize);
    }

    internal class ShipDispatcher : IShipDispatcher
    {
        private IUserInputRetriever _userInputRetriever;

        public ShipDispatcher()
        {
            _userInputRetriever = new UserInputRetriever();
        }

        public bool PlaceHumanPlayerShip(IGameGrid gameGrid, ShipSize shipSize)
        {
            var coordinatesString = _userInputRetriever.GetInput();
            var directionString = _userInputRetriever.GetInput();

            var coordinates = coordinatesString?.ToUpper().TranslateToCoordinates();
            if (coordinates == null)
            {
                Console.WriteLine(ValidationResultMessages.WrongCoordinates);
                return false;
            }

            var direction = directionString?.ToUpper().TranslateToDirection();
            if (direction == null)
            {
                Console.WriteLine(ValidationResultMessages.WrongDirection);
                return false;
            }

            var ship = new Ship(shipSize, coordinates, direction.Value);
            var result = gameGrid.PlaceShip(ship);

            if (!result.canPlace)
                Console.WriteLine(result.validationResult);

            return result.canPlace;
        }

        internal void ChangeUserInputRetriever(IUserInputRetriever userInputRetriever)
        {
            _userInputRetriever = userInputRetriever;
        }
    }
}
