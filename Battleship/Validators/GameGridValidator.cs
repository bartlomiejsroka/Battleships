using Battleship.Enums;
using Battleship.ValueObjects;

namespace Battleship.Validators
{
    internal interface IGameGridValidator
    {
        /// <summary>
        /// Check if ship can be placed
        /// </summary>
        /// <param name="ship">Ship that need to be placed</param>
        /// <param name="grid">Game grid</param>
        /// <returns>True if ship can be placed, otherwise false</returns>
        bool ValidateShipPlacement(Ship ship, IGameGrid grid);

        /// <summary>
        /// Check if cell can be hit
        /// </summary>
        /// <param name="coordinates">Ceel coordinates</param>
        /// <returns>True if cell can be hit</returns>
        bool ValidateCellHit(Coordinates coordinates, IGameGrid gameGrid);

        /// <summary>
        /// Validation message if valid then empty
        /// </summary>
        public string ValidationResult { get; }
    }

    internal class GameGridValidator : IGameGridValidator
    {
        public string ValidationResult { get; private set; }

        public GameGridValidator()
        {
            ValidationResult = string.Empty;
        }

        public bool ValidateShipPlacement(Ship ship, IGameGrid gameGrid)
        {

            if (!AreCoordinatesValid(ship, gameGrid))
            {
                ValidationResult = ValidationResultMessages.CoordinatesOutsideGrid;
                return false;
            }

            if (IsShipOutsideGridRange(ship, gameGrid))
            {
                ValidationResult = ValidationResultMessages.ShipOutsideGameGrid;
                return false;
            }

            if (IsShipCollidingOccupied(ship, gameGrid))
            {
                ValidationResult = ValidationResultMessages.ShipColidesWithOccupiedFields;
                return false;
            }

            ValidationResult = string.Empty;
            return true;
        }

        private bool IsShipOutsideGridRange(Ship ship, IGameGrid gameGrid)
            => ship.Direction switch
            {
                Direction.Up => ship.Size + ship.Coordinates.Row >= gameGrid.Size,
                Direction.Down => ship.Coordinates.Row - ship.Size < 0,
                Direction.Left => ship.Coordinates.Column - ship.Size < 0,
                Direction.Right => ship.Size + ship.Coordinates.Column >= gameGrid.Size,
                _ => true
            };


        private bool AreCoordinatesValid(Ship ship, IGameGrid gameGrid)
            => ship.Coordinates.Column <= gameGrid.Size && ship.Coordinates.Row <= gameGrid.Size;

        private bool AreCoordinatesValid(Coordinates coordinates, IGameGrid gameGrid)
           => coordinates.Column <= gameGrid.Size && coordinates.Row <= gameGrid.Size;


        public bool ValidateCellHit(Coordinates coordinates, IGameGrid gameGrid)
        {
            if (!AreCoordinatesValid(coordinates, gameGrid))
            {
                ValidationResult = ValidationResultMessages.CoordinatesOutsideGrid;
                return false;
            }

            if (gameGrid.Grid[coordinates.Column, coordinates.Row].StateEnum == CellStates.Missed ||
                gameGrid.Grid[coordinates.Column, coordinates.Row].StateEnum == CellStates.Hit)
            {
                ValidationResult = ValidationResultMessages.CellWasAlreadyHit;
                return false;
            }

            ValidationResult = string.Empty;
            return true;
        }

        private bool IsShipCollidingOccupied(Ship ship, IGameGrid gameGrid)
        {
            var isCellOccupied = false;
            switch (ship.Direction)
            {
                case Direction.Up:
                    for (int i = ship.Coordinates.Row; i < ship.Coordinates.Row + ship.Size; i++)
                    {
                        if (gameGrid.Grid[ship.Coordinates.Column, i].StateEnum != CellStates.Empty)
                            isCellOccupied = true;
                    }
                    break;
                case Direction.Down:
                    for (int i = ship.Coordinates.Row; i > ship.Coordinates.Row - ship.Size; i--)
                    {
                        if (gameGrid.Grid[ship.Coordinates.Column, i].StateEnum != CellStates.Empty)
                            isCellOccupied = true;
                    }
                    break;
                case Direction.Left:
                    for (int i = ship.Coordinates.Column; i > ship.Coordinates.Column - ship.Size; i--)
                    {
                        if (gameGrid.Grid[i, ship.Coordinates.Row].StateEnum != CellStates.Empty)
                            isCellOccupied = true;
                    }
                    break;
                case Direction.Right:
                    for (int i = ship.Coordinates.Column; i < ship.Coordinates.Column + ship.Size; i++)
                    {
                        if (gameGrid.Grid[i, ship.Coordinates.Row].StateEnum != CellStates.Empty)
                            isCellOccupied = true;
                    }
                    break;
                default:
                    isCellOccupied = true;
                    break;
            }

            return isCellOccupied;
        }
    }
}
