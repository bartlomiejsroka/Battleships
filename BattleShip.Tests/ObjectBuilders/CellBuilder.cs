using Battleship;

namespace BattleShip.Tests.ObjectBuilders
{
    internal class CellBuilder
    {
        public Cell Cell { get; private set; }

        public CellBuilder()
        {
            Cell = new Cell();
        }

        public Cell Build()
            => Cell;

        public CellBuilder WithOccupiedState(Ship ship)
        {
            Cell = new Cell();
            Cell.AddShip(ship);
            return this;
        }

        public CellBuilder WithMissedState()
        {
            Cell = new Cell();
            Cell.Hit();
            return this;
        }

        public CellBuilder WithHitState(Ship ship)
        {
            Cell = new Cell();
            Cell.AddShip(ship);
            Cell.Hit();
            return this;
        }
    }
}
