using Battleship.Enums;
using Battleship.ValueObjects;

namespace Battleship.RandomGenerators
{
    internal interface IRandomShipGenerator
    {
        /// <summary>
        /// Generates destroyer with random coordinates and direction
        /// </summary>
        /// <param name="gridSize">Size of game grid</param>
        /// <returns>Random destroyer</returns>
        Ship GenerateDestroyer(short gridSize);

        /// <summary>
        /// Generates battleship with random coordinates and direction
        /// </summary>
        /// <param name="gridSize">Size of game grid</param>
        /// <returns>Random battleship</returns>
        Ship GenerateBattleship(short gridSize);
    }

    internal class RandomShipGenerator : IRandomShipGenerator
    {
        private IRandomCoordniatesGenerator _randomCoordniatesGenerator;
        private readonly Random _random;

        public RandomShipGenerator()
        {
            _random = new Random();
            _randomCoordniatesGenerator = new RandomCoordinatesGenerator();
        }

        public Ship GenerateDestroyer(short gridSize)
        {
            GetRandomCoordinatesAndDirection(gridSize, out var coordinates, out var direction);

            return new Ship(ShipSize.Destroyer, coordinates, direction);
        }

        public Ship GenerateBattleship(short gridSize)
        {
            GetRandomCoordinatesAndDirection(gridSize, out var coordinates, out var direction);

            return new Ship(ShipSize.Battleship, coordinates, direction);
        }

        internal void ChangeRandomCoordniatesGenerator(IRandomCoordniatesGenerator randomCoordniatesGenerator)
        {
            _randomCoordniatesGenerator = randomCoordniatesGenerator;
        }

        private void GetRandomCoordinatesAndDirection(short gridSize, out Coordinates coordinates, out Direction direction)
        {
            coordinates = _randomCoordniatesGenerator.GetRandomCoordiantes(gridSize);
            var directionNumber = _random.Next(0, 1024) % 4;
            switch (directionNumber)
            {
                case 0:
                    direction = Direction.Up; break;
                case 1:
                    direction = Direction.Down; break;
                case 2:
                    direction = Direction.Left; break;
                case 3:
                    direction = Direction.Right; break;
                default:
                    direction = Direction.Up; break;

            }
        }
    }
}
