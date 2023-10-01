using Battleship.ValueObjects;

namespace Battleship.RandomGenerators
{
    internal interface IRandomCoordniatesGenerator
    {
        /// <summary>
        /// Get random coordinates
        /// </summary>
        /// <param name="gridSize">Size of game grid</param>
        /// <returns>Random coordiantes</returns>
        Coordinates GetRandomCoordiantes(short gridSize);
    }

    internal class RandomCoordinatesGenerator : IRandomCoordniatesGenerator
    {
        private readonly Random _random;

        public RandomCoordinatesGenerator()
        {
            _random = new Random();
        }

        public Coordinates GetRandomCoordiantes(short gridSize)
            => new Coordinates((short)(_random.Next(0, 1024) % gridSize), (short)(_random.Next(0, 1024) % gridSize));
    }
}
