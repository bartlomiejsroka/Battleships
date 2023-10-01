using Battleship;
using Battleship.Enums;
using Battleship.Validators;
using Battleship.ValueObjects;
using BattleShip.Tests.Common;
using BattleShip.Tests.ObjectBuilders;
using FluentAssertions;
using NSubstitute;

namespace BattleShip.Tests.GameGrids
{
    public class GameGridTests
    {
        private readonly IGameGridValidator _validator;
        public GameGridTests()
        {
            _validator = Substitute.For<IGameGridValidator>();
        }

        [Fact]
        public void GivenGameGrid_WhenInitializeEmpty_ThenEmptyGridCreated()
        {
            //given
            var grid = new GameGridBuilder()
                .WithSize(6)
                .Build();

            //when
            grid.InitializeEmpty();

            //then
            Assertions.AsserGridEmpty(grid);
        }

        [Fact]
        public void GivenGameGridAndShip_WhenPlaceShipOnEmptyGrid_ThenGridWithShipCreated()
        {
            //given
            var grid = new GameGridBuilder()
                .WithSize(6)
                .Build();
            grid.InitializeEmpty();
            grid.ChangeValidator(_validator);
            _validator.ValidateShipPlacement(default, default).ReturnsForAnyArgs(true);

            var ship = new ShipBuilder()
                .WithSize(ShipSize.Destroyer)
                .WithCoordinates(5, 1)
                .WithDirection(Direction.Left)
                .Build();

            //when
            var placeResul = grid.PlaceShip(ship);

            //then
            placeResul.canPlace.Should().BeTrue();
            placeResul.validationResult.Should().BeEmpty();
            AssertShipPlaced(grid, ship);
        }

        [Fact]
        public void GivenGameGridAndShip_WhenPlaceShipNotValid_ThenGridEmpty()
        {
            //given
            var grid = new GameGridBuilder()
                .WithSize(6)
                .Build();
            grid.InitializeEmpty();
            grid.ChangeValidator(_validator);
            _validator.ValidateShipPlacement(default, default).ReturnsForAnyArgs(false);

            var ship = new ShipBuilder()
                .WithSize(ShipSize.Destroyer)
                .WithCoordinates(5, 1)
                .WithDirection(Direction.Left)
                .Build();

            //when
            var placeResul = grid.PlaceShip(ship);

            //then
            placeResul.canPlace.Should().BeFalse();
            Assertions.AsserGridEmpty(grid);
        }

        [Fact]
        public void GivenGameGridAndTwoShips_WhenPlaceShipsOnEmptyGrid_ThenGridWithShipsCreated()
        {
            //given
            var grid = new GameGridBuilder()
                .WithSize(6)
                .Build();
            grid.InitializeEmpty();
            grid.ChangeValidator(_validator);
            _validator.ValidateShipPlacement(default, default).ReturnsForAnyArgs(true);

            var ship1 = new ShipBuilder()
                .WithSize(ShipSize.Destroyer)
                .WithCoordinates(5, 1)
                .WithDirection(Direction.Left)
                .Build();

            var ship2 = new ShipBuilder()
                .WithSize(ShipSize.Destroyer)
                .WithCoordinates(1, 0)
                .WithDirection(Direction.Up)
                .Build();

            //when
            var placeResul1 = grid.PlaceShip(ship1);
            var placeResul2 = grid.PlaceShip(ship2);

            //then
            placeResul1.canPlace.Should().BeTrue();
            placeResul1.validationResult.Should().BeEmpty();
            placeResul2.canPlace.Should().BeTrue();
            placeResul2.validationResult.Should().BeEmpty();
            AssertShipPlaced(grid, ship1);
            AssertShipPlaced(grid, ship2);
        }

        [Fact]
        public void GivenGameGridWithShip_WhenHitEmptyCell_ThenCellMarkAsMissed()
        {
            //given
            var grid = new GameGridBuilder()
                .WithSize(6)
                .Build();
            grid.InitializeEmpty();
            grid.ChangeValidator(_validator);
            _validator.ValidateCellHit(default, default).ReturnsForAnyArgs(true);

            var ship = new ShipBuilder()
                .WithSize(ShipSize.Destroyer)
                .WithCoordinates(5, 1)
                .WithDirection(Direction.Left)
                .Build();
            grid.PlaceShip(ship);

            //when
            var result = grid.HitCell(new Coordinates(0, 0));

            //then
            result.CanHit.Should().BeTrue();
            grid.Grid[0, 0].Should().BeEquivalentTo(new CellBuilder().WithMissedState().Build());
        }

        [Fact]
        public void GivenGameGridWithShip_WhenHitOccupiedCell_ThenCellMarkAsHit()
        {
            //given
            var grid = new GameGridBuilder()
                .WithSize(6)
                .Build();
            grid.InitializeEmpty();
            grid.ChangeValidator(_validator);
            _validator.ValidateCellHit(default, default).ReturnsForAnyArgs(true);
            _validator.ValidateShipPlacement(default, default).ReturnsForAnyArgs(true);

            var ship = new ShipBuilder()
                .WithSize(ShipSize.Destroyer)
                .WithCoordinates(5, 1)
                .WithDirection(Direction.Left)
                .Build();
            grid.PlaceShip(ship);

            //when
            var result = grid.HitCell(new Coordinates(5, 1));

            //then
            result.CanHit.Should().BeTrue();
            grid.Grid[5, 1].Should().BeEquivalentTo(new CellBuilder().WithHitState(ship).Build());
        }

        [Fact]
        public void GivenGameGridWithShips_WhenGetAnnonymizedGrid_ThenReturnGridEmpty()
        {
            //given
            var grid = new GameGridBuilder()
                .WithSize(6)
                .Build();
            grid.InitializeEmpty();
            grid.ChangeValidator(_validator);
            _validator.ValidateShipPlacement(default, default).ReturnsForAnyArgs(true);

            var ship = new ShipBuilder()
                .WithSize(ShipSize.Destroyer)
                .WithCoordinates(5, 1)
                .WithDirection(Direction.Left)
                .Build();
            grid.PlaceShip(ship);

            //when
            var result = grid.GetAnonymizedGrid();

            //then
            foreach (var item in result)
            {
                item.Should().BeEquivalentTo(new CellBuilder().Build());
            }
        }

        private void AssertShipPlaced(GameGrid grid, Ship ship)
        {
            switch (ship.Direction)
            {

                case Direction.Up:
                    for (var i = ship.Coordinates.Row; i < ship.Coordinates.Row + ship.Size; i++)
                    {
                        grid.Grid[ship.Coordinates.Column, i].Should().BeEquivalentTo(new CellBuilder().WithOccupiedState(ship).Build());
                    }
                    break;
                case Direction.Down:
                    for (var i = ship.Coordinates.Row; i > ship.Coordinates.Row - ship.Size; i--)
                    {
                        grid.Grid[ship.Coordinates.Column, i].Should().BeEquivalentTo(new CellBuilder().WithOccupiedState(ship).Build());
                    }
                    break;
                case Direction.Right:
                    for (var i = ship.Coordinates.Column; i < ship.Coordinates.Column + ship.Size; i++)
                    {
                        grid.Grid[i, ship.Coordinates.Row].Should().BeEquivalentTo(new CellBuilder().WithOccupiedState(ship).Build());
                    }
                    break;
                case Direction.Left:
                    for (var i = ship.Coordinates.Column; i > ship.Coordinates.Column - ship.Size; i--)
                    {
                        grid.Grid[i, ship.Coordinates.Row].Should().BeEquivalentTo(new CellBuilder().WithOccupiedState(ship).Build());
                    }
                    break;
            }
        }
    }
}
