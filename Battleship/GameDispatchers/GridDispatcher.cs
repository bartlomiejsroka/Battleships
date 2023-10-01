using Battleship.RandomGenerators;

namespace Battleship.GameDispatchers
{
    internal interface IGridDispatcher
    {
        void InitializePlayerAndComputerGrid(IGameGrid playerGrid, IGameGrid computerGrid, short destroyterNumber, short battleshipNumber);
    }

    internal class GridDispatcher : IGridDispatcher
    {
        private IRandomShipGenerator _randomShipGenerator;

        public GridDispatcher()
        {
            _randomShipGenerator = new RandomShipGenerator();
        }

        public void InitializePlayerAndComputerGrid(IGameGrid playerGrid, IGameGrid computerGrid, short destroyterNumber, short battleshipNumber)
        {
            computerGrid.InitializeEmpty();
            playerGrid.InitializeEmpty();

            var placed = false;
            for (int i = 0; i < battleshipNumber; i++)
            {
                while (!placed)
                {
                    var randomShip = _randomShipGenerator.GenerateBattleship(computerGrid.Size);
                    (placed, _) = computerGrid.PlaceShip(randomShip);
                }
                placed = false;
            }

            for (int i = 0; i < destroyterNumber; i++)
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
