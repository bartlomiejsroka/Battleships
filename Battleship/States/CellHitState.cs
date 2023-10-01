using Battleship.Enums;

namespace Battleship.States
{
    internal class CellHitState : ICellState
    {
        public CellStates StateEnum => CellStates.Hit;

        public void HandleAction(Cell cell, CellTrigger cellTrigger)
        {

            switch (cellTrigger)
            {
                case CellTrigger.PlaceShip:
                    throw new ApplicationException("Place ship on hit cell");

                case CellTrigger.HitCell:
                    throw new ApplicationException("Cannot hit already hit cell");
            }
        }
    }
}
