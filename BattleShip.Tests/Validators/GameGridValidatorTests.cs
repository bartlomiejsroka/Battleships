using Battleship.Enums;
using Battleship.Validators;
using Battleship.ValueObjects;
using BattleShip.Tests.ObjectBuilders;
using FluentAssertions;

namespace BattleShip.Tests.Validators
{
    public class GameGridValidatorTests
    {
        private readonly GameGridValidator validator;

        public GameGridValidatorTests()
        {
            validator = new GameGridValidator();
        }

        [Fact]
        public void GivenGameGridAndValidShip_WhenValidateShipPlacement_ThenReturnsTrueWithoutMessage()
        {
            //given
            var grid = new GameGridBuilder()
                .WithSize(10)
                .Build();
            grid.InitializeEmpty();

            var ship = new ShipBuilder()
                .WithSize(ShipSize.Destroyer)
                .WithDirection(Direction.Up)
                .WithCoordinates(1, 1)
                .Build();

            //when 
            var result = validator.ValidateShipPlacement(ship, grid);

            //then
            result.Should().BeTrue();
            validator.ValidationResult.Should().BeEmpty();
        }

        [Fact]
        public void GivenGameGridAndShipOutsideGridWithCoordnatesInsideGrid_WhenValidateShipPlacement_ThenReturnsFalseWithValidMessage()
        {
            //given
            var grid = new GameGridBuilder()
                .WithSize(10)
                .Build();
            grid.InitializeEmpty();

            var ship = new ShipBuilder()
                .WithSize(ShipSize.Destroyer)
                .WithDirection(Direction.Down)
                .WithCoordinates(1, 1)
                .Build();

            //when 
            var result = validator.ValidateShipPlacement(ship, grid);

            //then
            result.Should().BeFalse();
            validator.ValidationResult.Should().Be(ValidationResultMessages.ShipOutsideGameGrid);
        }

        [Fact]
        public void GivenGameGridAndShipWithCoordnatesOutsideGrid_WhenValidateShipPlacement_ThenReturnsFalseWithValidMessage()
        {
            //given
            var grid = new GameGridBuilder()
                .WithSize(10)
                .Build();
            grid.InitializeEmpty();

            var ship = new ShipBuilder()
                .WithSize(ShipSize.Destroyer)
                .WithDirection(Direction.Down)
                .WithCoordinates(12, 12)
                .Build();

            //when 
            var result = validator.ValidateShipPlacement(ship, grid);

            //then
            result.Should().BeFalse();
            validator.ValidationResult.Should().Be(ValidationResultMessages.CoordinatesOutsideGrid);
        }

        [Fact]
        public void GivenGameGridAndTwoCollidingShips_WhenValidateShipPlacement_ThenReturnsFalseWithValidMessage()
        {
            //given
            var grid = new GameGridBuilder()
                .WithSize(10)
                .Build();
            grid.InitializeEmpty();

            var ship = new ShipBuilder()
                .WithSize(ShipSize.Destroyer)
                .WithDirection(Direction.Up)
                .WithCoordinates(1, 1)
                .Build();
            grid.PlaceShip(ship);

            var collidingShip = new ShipBuilder()
                .WithSize(ShipSize.Destroyer)
                .WithDirection(Direction.Up)
                .WithCoordinates(1, 3)
                .Build();

            //when 
            var result = validator.ValidateShipPlacement(collidingShip, grid);

            //then
            result.Should().BeFalse();
            validator.ValidationResult.Should().Be(ValidationResultMessages.ShipColidesWithOccupiedFields);
        }

        [Fact]
        public void GivenEmptyGridAndValidCoordinates_ValidateCellHit_ThenReturnsTrueWithoutMessage()
        {
            //given
            var grid = new GameGridBuilder()
                .WithSize(10)
                .Build();
            grid.InitializeEmpty();

            var validCoordinates = new Coordinates(1, 1);

            //when 
            var result = validator.ValidateCellHit(validCoordinates, grid);

            //then
            result.Should().BeTrue();
            validator.ValidationResult.Should().BeEmpty();
        }

        [Fact]
        public void GivenEmptyGridAndInvalidCoordinates_ValidateCellHit_ThenReturnsTrueWithoutMessage()
        {
            //given
            var grid = new GameGridBuilder()
                .WithSize(10)
                .Build();
            grid.InitializeEmpty();

            var validCoordinates = new Coordinates(11, 11);

            //when 
            var result = validator.ValidateCellHit(validCoordinates, grid);

            //then
            result.Should().BeFalse();
            validator.ValidationResult.Should().Be(ValidationResultMessages.CoordinatesOutsideGrid);
        }

        [Fact]
        public void GivenGridAndValidCoordinatesOnAlreadyMissedCell_ValidateCellHit_ThenReturnsFalseWithValidMessage()
        {
            //given
            var grid = new GameGridBuilder()
                .WithSize(10)
                .Build();
            grid.InitializeEmpty();
            grid.HitCell(new Coordinates(1, 1));

            var validCoordinates = new Coordinates(1, 1);

            //when 
            var result = validator.ValidateCellHit(validCoordinates, grid);

            //then
            result.Should().BeFalse();
            validator.ValidationResult.Should().Be(ValidationResultMessages.CellWasAlreadyHit);
        }

        [Fact]
        public void GivenGridAndValidCoordinatesOnAlreadyHitCell_ValidateCellHit_ThenReturnsFalseWithValidMessage()
        {
            //given
            var grid = new GameGridBuilder()
                .WithSize(10)
                .Build();
            grid.InitializeEmpty();

            var ship = new ShipBuilder()
                .WithSize(ShipSize.Battleship)
                .WithDirection(Direction.Up)
                .WithCoordinates(1, 1);
            grid.HitCell(new Coordinates(1, 1));

            var validCoordinates = new Coordinates(1, 1);

            //when 
            var result = validator.ValidateCellHit(validCoordinates, grid);

            //then
            result.Should().BeFalse();
            validator.ValidationResult.Should().Be(ValidationResultMessages.CellWasAlreadyHit);
        }
    }
}