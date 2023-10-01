using Battleship;
using Battleship.States;
using BattleShip.Tests.ObjectBuilders;
using FluentAssertions;

namespace BattleShip.Tests.Cells
{
    public class CellTests
    {
        [Fact]
        public void GivenNoCell_WhenCreate_ThenCellInEmptyState()
        {
            //given

            //when
            var cell = new Cell();

            //then
            cell.Ship.Should().BeNull();
            cell.State.Should().BeEquivalentTo(new CellEmptyState());
        }

        [Fact]
        public void GivenEmptyCell_WhenHit_ThenCellInMissState()
        {
            //given
            var cell = new Cell();

            //when
            cell.Hit();

            //then
            cell.Ship.Should().BeNull();
            cell.State.Should().BeEquivalentTo(new CellMissedState());
        }

        [Fact]
        public void GivenEmptyCell_WhenAddShip_ThenCellInOccupiedState()
        {
            //given
            var cell = new Cell();

            //when
            cell.AddShip(new ShipBuilder().Build());

            //then
            cell.Ship.Should().NotBeNull();
            cell.State.Should().BeEquivalentTo(new CellOccupiedState());
        }

        [Fact]
        public void GivennOccupiedCell_WhenAddShip_ThenThrows()
        {
            //given
            var cell = new CellBuilder()
                .WithOccupiedState(new ShipBuilder().Build())
                .Build();

            //when
            var action = () => cell.AddShip(new ShipBuilder().Build());

            //then
            action.Should().Throw<ApplicationException>();
            cell.Ship.Should().NotBeNull();
            cell.State.Should().BeEquivalentTo(new CellOccupiedState());
        }

        [Fact]
        public void GivennOccupiedCell_WhenHit_ThenCellInHitState()
        {
            //given
            var cell = new CellBuilder()
                .WithOccupiedState(new ShipBuilder().Build())
                .Build();

            //when
            cell.Hit();

            //then
            cell.Ship.Should().NotBeNull();
            cell.State.Should().BeEquivalentTo(new CellHitState());
        }

        [Fact]
        public void GivennHitCell_WhenHit_ThenThrows()
        {
            //given
            var cell = new CellBuilder()
                .WithHitState(new ShipBuilder().Build())
                .Build();

            //when
            var action = () => cell.Hit();

            //then
            action.Should().Throw<ApplicationException>();
            cell.Ship.Should().NotBeNull();
            cell.State.Should().BeEquivalentTo(new CellHitState());
        }

        [Fact]
        public void GivennHitCell_WhenAddShip_ThenThrows()
        {
            //given
            var cell = new CellBuilder()
                .WithHitState(new ShipBuilder().Build())
                .Build();

            //when
            var action = () => cell.AddShip(new ShipBuilder().Build());

            //then
            action.Should().Throw<ApplicationException>();
            cell.Ship.Should().NotBeNull();
            cell.State.Should().BeEquivalentTo(new CellHitState());
        }

        [Fact]
        public void GivennMissedtCell_WhenAddShip_ThenThrows()
        {
            //given
            var cell = new CellBuilder()
                .WithMissedState()
                .Build();

            //when
            var action = () => cell.AddShip(new ShipBuilder().Build());

            //then
            action.Should().Throw<ApplicationException>();
            cell.Ship.Should().BeNull();
            cell.State.Should().BeEquivalentTo(new CellMissedState());
        }

        [Fact]
        public void GivennMissedCell_WhenHit_ThenThrows()
        {
            //given
            var cell = new CellBuilder()
                .WithMissedState()
                .Build();

            //when
            var action = () => cell.Hit();

            //then
            action.Should().Throw<ApplicationException>();
            cell.Ship.Should().BeNull();
            cell.State.Should().BeEquivalentTo(new CellMissedState());
        }
    }
}
