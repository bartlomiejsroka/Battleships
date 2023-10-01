using Battleship.RandomGenerators;

namespace Battleship.GameDispatchers
{
    internal interface IGridDispatcher
    {
        /// <summary>
        /// Initialize empty player grid and computer grid with random ships
        /// </summary>
        /// <param name="playerGrid">Player grid</param>
        /// <param name="computerGrid">Computer grid</param>
        /// <param name="destroytersNumber">Destroyters number</param>
        /// <param name="battleshipsNumber">Battleships number</param>
        void InitializePlayerAndComputerGrid(IGameGrid playerGrid, IGameGrid computerGrid, short destroytersNumber, short battleshipsNumber);
    }

    internal class GridDispatcher : IGridDispatcher
    {
        private IRandomShipGenerator _randomShipGenerator;

        public GridDispatcher()
        {
            _randomShipGenerator = new RandomShipGenerator();
        }

        public void InitializePlayerAndComputerGrid(IGameGrid playerGrid, IGameGrid computerGrid, short destroytersNumber, short battleshipsNumber)
        {
            computerGrid.InitializeEmpty();
            playerGrid.InitializeEmpty();

            var placed = false;
            for (int i = 0; i < battleshipsNumber; i++)
            {
                while (!placed)
                {
                    var randomShip = _randomShipGenerator.GenerateBattleship(computerGrid.Size);
                    (placed, _) = computerGrid.PlaceShip(randomShip);
                }
                placed = false;
            }

            for (int i = 0; i < destroytersNumber; i++)
            {
                while (!placed)
                {
                    var randomShip = _randomShipGenerator.GenerateDestroyer(computerGrid.Size);
                    (placed, _) = computerGrid.PlaceShip(randomShip);
                }
                placed = false;
            }
        }

        internal void ChangeRandomShipGenerator(IRandomShipGenerator randomShipGenerator)
        {
            _randomShipGenerator = randomShipGenerator;
        }
    }
}
