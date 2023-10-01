using Battleship.Enums;

namespace Battleship.States
{
    internal interface ICellState
    {
        public CellStates StateEnum { get; }

        void HandleAction(Cell cell, CellTrigger cellTrigger);
    }
}
