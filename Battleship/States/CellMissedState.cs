using Battleship.Enums;

namespace Battleship.States
{
    internal class CellMissedState : ICellState
    {
        public CellStates StateEnum => CellStates.Missed;

        public void HandleAction(Cell cell, CellTrigger cellTrigger)
        {
            throw new ApplicationException("Cannot hit already hit cell");
        }
    }
}
