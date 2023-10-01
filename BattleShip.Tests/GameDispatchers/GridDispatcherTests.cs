using Battleship;
using Battleship.Enums;
using Battleship.GameDispatchers;
using Battleship.RandomGenerators;
using BattleShip.Tests.Common;
using BattleShip.Tests.ObjectBuilders;
using FluentAssertions;
using NSubstitute;

namespace BattleShip.Tests.GameDispatchers
{
    public class GridDispatcherTests
    {
        private readonly IRandomShipGenerator _randomShipGenerator;

        public GridDispatcherTests()
        {
            _randomShipGenerator = Substitute.For<IRandomShipGenerator>();
        }

        [Fact]
        public void GivenGridDispatcherAndTwoEmptyGrids_WhenInitialize_ThenPlayerGridEmptyComputerGridWithRandomShips()
        {
            //given
            var playerGrid = new GameGrid(10);
            var computerGrid = new GameGrid(10);
            var gridDispatcher = new GridDispatcher();
            var ship = new ShipBuilder()
                .WithCoordinates(1, 1)
                .WithSize(ShipSize.Battleship)
                .WithDirection(Direction.Up)
                .Build();
            _randomShipGenerator.GenerateDestroyer(10).Returns(ship);
            gridDispatcher.ChangeRandomShipGenerator(_randomShipGenerator);

            //when
            gridDispatcher.InitializePlayerAndComputerGrid(playerGrid, computerGrid, 1, 0);

            //then
            Assertions.AsserGridEmpty(playerGrid);
            computerGrid.Grid[1, 1].Should().BeEquivalentTo(new CellBuilder().WithOccupiedState(ship).Build());

        }
    }
}
