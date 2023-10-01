using BattleShip.Tests.ObjectBuilders;
using Battleship;
using FluentAssertions;

namespace BattleShip.Tests.Common
{
    internal static class Assertions
    {
        public static void AsserGridEmpty(GameGrid grid)
        {
            for (var i = 0; i < grid.Size; i++)
            {
                for (var j = 0; j < grid.Size; j++)
                {
                    grid.Grid[i, j].Should().BeEquivalentTo(new CellBuilder().Build());
                }
            }
        }
    }
}
