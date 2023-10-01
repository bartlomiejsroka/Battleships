using Battleship;
using Battleship.Enums;
using Battleship.GameDispatchers;
using Battleship.InputRetrievers;
using Battleship.ValueObjects;
using BattleShip.Tests.Common;
using FluentAssertions;
using NSubstitute;

namespace BattleShip.Tests.GameDispatchers
{
    public class ShipDispatcherTests
    {
        private readonly IUserInputRetriever _userInputRetriever;

        public ShipDispatcherTests()
        {
            _userInputRetriever = Substitute.For<IUserInputRetriever>();
        }

        [Fact]
        public void GivenShipDispatcherWithValidCoordinates_WhenPlaceHumanPlayerShip_ThenShipPlaced()
        {
            //when
            var shipDispatcher = new ShipDispatcher();
            var grid = new GameGrid(10);
            grid.InitializeEmpty();
            _userInputRetriever.GetInput().Returns(x => "B1", x => "U");
            shipDispatcher.ChangeUserInputRetriever(_userInputRetriever);

            //when
            var result = shipDispatcher.PlaceHumanPlayerShip(grid, ShipSize.Destroyer);

            //then
            result.Should().BeTrue();
            AssertDestroyerPlacedUp(grid.Grid, new Coordinates(1, 0));
        }

        [Fact]
        public void GivenShipDispatcherWithInvalidCoordinates_WhenPlaceHumanPlayerShip_ThenGridEmpty()
        {
            //when
            var shipDispatcher = new ShipDispatcher();
            var grid = new GameGrid(10);
            grid.InitializeEmpty();
            _userInputRetriever.GetInput().Returns(x => "B11", x => "U");
            shipDispatcher.ChangeUserInputRetriever(_userInputRetriever);

            //when
            var result = shipDispatcher.PlaceHumanPlayerShip(grid, ShipSize.Destroyer);

            //then
            result.Should().BeFalse();
            Assertions.AsserGridEmpty(grid);
        }

        [Fact]
        public void GivenShipDispatcherWithValidCoordinatesWrongDirection_WhenPlaceHumanPlayerShip_ThenGridEmpty()
        {
            //when
            var shipDispatcher = new ShipDispatcher();
            var grid = new GameGrid(10);
            grid.InitializeEmpty();
            _userInputRetriever.GetInput().Returns(x => "B1", x => "C");
            shipDispatcher.ChangeUserInputRetriever(_userInputRetriever);

            //when
            var result = shipDispatcher.PlaceHumanPlayerShip(grid, ShipSize.Destroyer);

            //then
            result.Should().BeFalse();
            Assertions.AsserGridEmpty(grid);
        }

        [Fact]
        public void GivenShipDispatcherWithValidCoordinatesShipOutsideGRid_WhenPlaceHumanPlayerShip_ThenGridEmpty()
        {
            //when
            var shipDispatcher = new ShipDispatcher();
            var grid = new GameGrid(10);
            grid.InitializeEmpty();
            _userInputRetriever.GetInput().Returns(x => "B9", x => "U");
            shipDispatcher.ChangeUserInputRetriever(_userInputRetriever);

            //when
            var result = shipDispatcher.PlaceHumanPlayerShip(grid, ShipSize.Destroyer);

            //then
            result.Should().BeFalse();
            Assertions.AsserGridEmpty(grid);
        }


        private void AssertDestroyerPlacedUp(Cell[,] cells, Coordinates startCoordinates)
        {
            for (var i = 0; i < (int)ShipSize.Destroyer; i++)
            {
                cells[startCoordinates.Column, startCoordinates.Row + i].StateEnum.Should().Be(CellStates.Occupied);
                cells[startCoordinates.Column, startCoordinates.Row + i].Ship.Should().NotBeNull();
                cells[startCoordinates.Column, startCoordinates.Row + i].Ship!.Size.Should().Be((short)ShipSize.Destroyer);
            }
        }

    }
}
