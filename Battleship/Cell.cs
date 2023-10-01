using Battleship.Enums;
using Battleship.States;

namespace Battleship
{
    internal class Cell
    {
        public ICellState State { get; set; }
        public Ship? Ship { get; private set; }
        public CellStates StateEnum { get => State.StateEnum; }

        public Cell()
        {
            State = new CellEmptyState();
        }

        public void AddShip(Ship ship)
        {
            State.HandleAction(this, CellTrigger.PlaceShip);
            Ship = ship;
        }

        public void Hit()
        {
            State.HandleAction(this, CellTrigger.HitCell);
        }
    }
}
