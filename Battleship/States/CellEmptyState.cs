using Battleship.Enums;

namespace Battleship.States
{
    internal class CellEmptyState : ICellState
    {
        public CellStates StateEnum => CellStates.Empty;

        public void HandleAction(Cell cell, CellTrigger cellTrigger)
        {
            switch (cellTrigger)
            {
                case CellTrigger.PlaceShip:
                    cell.State = new CellOccupiedState();
                    break;

                case CellTrigger.HitCell:
                    cell.State = new CellMissedState();
                    break;
            }
        }
    }
}
