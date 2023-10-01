using Battleship.Enums;
using Battleship.Validators;
using Battleship.ValueObjects;

namespace Battleship
{
    internal interface IGameGrid
    {
        /// <summary>
        /// Size of game grid 
        /// </summary>
        short Size { get; }

        /// <summary>
        /// Game grid with cell state and shipId
        /// </summary>
        Cell[,] Grid { get; }

        /// <summary>
        /// Check if all ships are sunk
        /// </summary>
        /// <returns>True if all ships are sunk otherwise false</returns>
        public bool AllShipsSunk();

        /// <summary>
        /// Creates empty game grid
        /// </summary>
        void InitializeEmpty();

        /// <summary>
        /// Palce ship on game grid
        /// </summary>
        /// <param name="ship">Ship</param>
        /// <returns> True if place was possible if not then false and validation message </returns>
        (bool canPlace, string validationResult) PlaceShip(Ship ship);

        /// <summary>
        /// Hit selected cell
        /// </summary>
        /// <param name="coordinates">Coordinates of cell</param>
        /// <returns>True/false if cell can be hit, validation message if hit is not possible</returns>
        public (bool CanHit, string ValidationResult) HitCell(Coordinates coordinates);

        /// <summary>
        /// Get anonymized grid - ships replaced with empty cells
        /// </summary>
        /// <returns>Anonymized grid</returns>
        public Cell[,] GetAnonymizedGrid();

        /// <summary>
        /// Get last hit ship
        /// </summary>
        /// <returns>Last hit ship</returns>
        public Ship? LastHitShip { get; }
    }

    internal class GameGrid : IGameGrid
    {
        private IGameGridValidator _validator;

        private List<Ship> _placedShips;

        public Ship? LastHitShip { get; private set; }

        public short Size { get; private set; }

        public Cell[,] Grid { get; private set; }


        public GameGrid(short size)
        {
            Size = size;
            Grid = new Cell[size, size];
            _validator = new GameGridValidator();
            _placedShips = new List<Ship>();
        }

        public bool AllShipsSunk()
         => _placedShips.All(x => x.IsDead());

        public void InitializeEmpty()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Grid[i, j] = new Cell();
                }
            }
        }

        public (bool canPlace, string validationResult) PlaceShip(Ship ship)
        {
            var canplace = _validator.ValidateShipPlacement(ship, this);

            if (!canplace)
                return (false, _validator.ValidationResult);

            PlaceShipOnGameGrid(ship);
            _placedShips.Add(ship);

            return (true, string.Empty);
        }

        public (bool CanHit, string ValidationResult) HitCell(Coordinates coordinates)
        {
            var canHit = _validator.ValidateCellHit(coordinates, this);
            if (!canHit)
                return (false, _validator.ValidationResult);

            var targetCell = Grid[coordinates.Column, coordinates.Row];
            targetCell.Ship?.HitShip();
            LastHitShip = targetCell.Ship;
            Grid[coordinates.Column, coordinates.Row].Hit();
            return (true, string.Empty);
        }

        public Cell[,] GetAnonymizedGrid()
        {
            var annonymizedGrid = new Cell[Size, Size];

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (Grid[i, j].StateEnum == CellStates.Missed || Grid[i, j].StateEnum == CellStates.Hit)
                        annonymizedGrid[i, j] = Grid[i, j];
                    else
                        annonymizedGrid[i, j] = new Cell();
                }
            }

            return annonymizedGrid;
        }

        internal void ChangeValidator(IGameGridValidator validator)
        {
            _validator = validator;
        }

        private void PlaceShipOnGameGrid(Ship ship)
        {
            switch (ship.Direction)
            {
                case Direction.Up:
                    for (int i = ship.Coordinates.Row; i < ship.Coordinates.Row + ship.Size; i++)
                    {
                        Grid[ship.Coordinates.Column, i].AddShip(ship);
                    }
                    break;

                case Direction.Down:
                    for (int i = ship.Coordinates.Row; i > ship.Coordinates.Row - ship.Size; i--)
                    {
                        Grid[ship.Coordinates.Column, i].AddShip(ship);
                    }
                    break;
                case Direction.Left:
                    for (int i = ship.Coordinates.Column; i > ship.Coordinates.Column - ship.Size; i--)
                    {
                        Grid[i, ship.Coordinates.Row].AddShip(ship);
                    }
                    break;
                case Direction.Right:
                    for (int i = ship.Coordinates.Column; i < ship.Coordinates.Column + ship.Size; i++)
                    {
                        Grid[i, ship.Coordinates.Row].AddShip(ship);
                    }
                    break;

            }
        }
    }
}
