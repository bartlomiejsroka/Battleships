using Battleship;

namespace BattleShip.Tests.ObjectBuilders
{
    internal class GameGridBuilder
    {
        public short Size { get; private set; }

        public GameGrid Build()
        {
            return new GameGrid(Size);
        }

        public GameGridBuilder WithSize(short size)
        {
            Size = size;
            return this;
        }
    }
}
