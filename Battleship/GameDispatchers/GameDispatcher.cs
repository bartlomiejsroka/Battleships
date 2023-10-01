using Battleship.Enums;

namespace Battleship.GameDispatchers
{
    public class GameDispatcher
    {
        private IGameGrid _playerGrid;
        private IGameGrid _computerGrid;
        private IShipDispatcher _shipDispatcher;
        private IPlayerDispatcher _playerDispatcher;
        private IGridDispatcher _gridDispatcher;

        private readonly short _destroyerNumber;
        private readonly short _battleshipNumber;
        private readonly short _gridSize;

        public GameDispatcher(short destroyerNumber, short battleshipNumber, short gridSize)
        {
            _gridSize = gridSize;
            _destroyerNumber = destroyerNumber;
            _battleshipNumber = battleshipNumber;

            _playerGrid = new GameGrid(_gridSize);
            _computerGrid = new GameGrid(_gridSize);
            _shipDispatcher = new ShipDispatcher();
            _playerDispatcher = new PlayerDispatcher();
            _gridDispatcher = new GridDispatcher();
        }

        public void Play()
        {
            _gridDispatcher.InitializePlayerAndComputerGrid(_playerGrid, _computerGrid, _destroyerNumber, _battleshipNumber);

            PlacePlayerShips();

            GetPlayerCurrentGrid().DisplayGrid();

            StartGame();
        }

        private void StartGame()
        {
            var gameOver = false;

            while (!gameOver)
            {
                Console.WriteLine("Enter hit coordinates: \n");

                var result = _playerDispatcher.HitComputerGridAndMakeComputerMove(_playerGrid, _computerGrid);

                while (!result)
                {
                    Console.WriteLine("Enter hit coordinates: ");
                    result = _playerDispatcher.HitComputerGridAndMakeComputerMove(_playerGrid, _computerGrid);
                }

                Console.WriteLine("Player: ");
                GetPlayerCurrentGrid().DisplayGrid();
                Console.WriteLine("Computer: ");
                GetComputerCurrentGrid().DisplayGrid();

                if (_computerGrid.AllShipsSunk())
                {
                    Console.WriteLine("You win!");
                    gameOver = true;
                }
                else if (_playerGrid.AllShipsSunk())
                {
                    Console.WriteLine("You lose!");
                    gameOver = true;
                }
            }
        }

        private void PlacePlayerShips()
        {
            for (int i = 0; i < _destroyerNumber; i++)
            {
                GetPlayerCurrentGrid().DisplayGrid();
                Console.WriteLine($"Place destroyer {i + 1} enter cordinates and direction U/D/L/R");

                var placeResult = _shipDispatcher.PlaceHumanPlayerShip(_playerGrid, ShipSize.Destroyer);

                while (!placeResult)
                    placeResult = _shipDispatcher.PlaceHumanPlayerShip(_playerGrid, ShipSize.Destroyer);

            }

            for (int i = 0; i < _battleshipNumber; i++)
            {
                GetPlayerCurrentGrid().DisplayGrid();
                Console.WriteLine($"Place battleship {i + 1} enter cordinates and direction U/D/L/R");

                var placeResult = _shipDispatcher.PlaceHumanPlayerShip(_playerGrid, ShipSize.Battleship);

                while (!placeResult)
                    placeResult = _shipDispatcher.PlaceHumanPlayerShip(_playerGrid, ShipSize.Battleship);
            }
        }

        private Cell[,] GetComputerCurrentGrid()
           => _computerGrid.GetAnonymizedGrid();

        private Cell[,] GetPlayerCurrentGrid()
           => _playerGrid.Grid;
    }
}
