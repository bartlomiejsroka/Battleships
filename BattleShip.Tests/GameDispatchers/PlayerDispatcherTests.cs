using Battleship;
using Battleship.GameDispatchers;
using Battleship.InputRetrievers;
using Battleship.RandomGenerators;
using Battleship.ValueObjects;
using BattleShip.Tests.Common;
using BattleShip.Tests.ObjectBuilders;
using FluentAssertions;
using NSubstitute;

namespace BattleShip.Tests.GameDispatchers
{
    public class PlayerDispatcherTests
    {
        private readonly IRandomCoordniatesGenerator _randomCoordniatesGenerator;
        private readonly IUserInputRetriever _userInputRetriever;

        public PlayerDispatcherTests()
        {
            _randomCoordniatesGenerator = Substitute.For<IRandomCoordniatesGenerator>();
            _userInputRetriever = Substitute.For<IUserInputRetriever>();
        }

        [Fact]
        public void GivenPlayerDispatcherAndInitializedGridsWithValidCoordinates_WhenHitComputerGridAndMakeComputerMove_ThenPlayerAndComputerGridHit()
        {
            //given
            var playerGrid = new GameGrid(10);
            var computerGrid = new GameGrid(10);
            playerGrid.InitializeEmpty();
            computerGrid.InitializeEmpty();
            _randomCoordniatesGenerator.GetRandomCoordiantes(playerGrid.Size).Returns(new Coordinates(1, 1));
            _userInputRetriever.GetInput().Returns("C2");
            var playerDispatcher = new PlayerDispatcher();
            playerDispatcher.ChangeRandomCoordniatesGenerator(_randomCoordniatesGenerator);
            playerDispatcher.ChangeUserInputRetriever(_userInputRetriever);

            //when
            var result = playerDispatcher.HitComputerGridAndMakeComputerMove(playerGrid, computerGrid);

            //then
            result.Should().BeTrue();
            playerGrid.Grid[1, 1].Should().BeEquivalentTo(new CellBuilder().WithMissedState().Build());
            computerGrid.Grid[2, 1].Should().BeEquivalentTo(new CellBuilder().WithMissedState().Build());
        }

        [Fact]
        public void GivenPlayerDispatcherAndInitializedGridsWithInvalidCoordinates_WhenHitComputerGridAndMakeComputerMove_ThenGridsUnchanged()
        {
            //given
            var playerGrid = new GameGrid(10);
            var computerGrid = new GameGrid(10);
            playerGrid.InitializeEmpty();
            computerGrid.InitializeEmpty();
            _randomCoordniatesGenerator.GetRandomCoordiantes(playerGrid.Size).Returns(new Coordinates(1, 1));
            _userInputRetriever.GetInput().Returns("C22");
            var playerDispatcher = new PlayerDispatcher();
            playerDispatcher.ChangeRandomCoordniatesGenerator(_randomCoordniatesGenerator);
            playerDispatcher.ChangeUserInputRetriever(_userInputRetriever);

            //when
            var result = playerDispatcher.HitComputerGridAndMakeComputerMove(playerGrid, computerGrid);

            //then
            result.Should().BeFalse();
            Assertions.AsserGridEmpty(playerGrid);
            Assertions.AsserGridEmpty(computerGrid);
        }
    }
}
