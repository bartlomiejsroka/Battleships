using Battleship.Enums;
using Battleship.Validators;

namespace Battleship.States
{
    internal class CellOccupiedState : ICellState
    {
        public CellStates StateEnum => CellStates.Occupied;

        public void HandleAction(Cell cell, CellTrigger cellTrigger)
        {
            switch (cellTrigger)
            {
                case CellTrigger.PlaceShip:
                    throw new ApplicationException(ValidationResultMessages.ShipColidesWithOccupiedFields);

                case CellTrigger.HitCell:
                    cell.State = new CellHitState();
                    break;
            }
        }
    }
}
