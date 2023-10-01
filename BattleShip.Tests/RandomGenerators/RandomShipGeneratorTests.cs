using Battleship.RandomGenerators;
using Battleship.ValueObjects;
using FluentAssertions;
using NSubstitute;

namespace BattleShip.Tests.RandomGenerators
{
    public class RandomShipGeneratorTests
    {
        private RandomShipGenerator _randomShipGenerator;
        private IRandomCoordniatesGenerator _randomCoordniatesGenerator;
        public RandomShipGeneratorTests()
        {
            _randomCoordniatesGenerator = Substitute.For<IRandomCoordniatesGenerator>();
            _randomShipGenerator = new RandomShipGenerator();
            _randomShipGenerator.ChangeRandomCoordniatesGenerator(_randomCoordniatesGenerator);
        }

        [Fact]
        public void GivenRandomShipGenerator_WhenGenerateDestroyer_ThenValidShipReturned()
        {
            //given
            _randomCoordniatesGenerator.GetRandomCoordiantes(6).Returns(new Coordinates(1, 4));

            //when
            var resultShip = _randomShipGenerator.GenerateDestroyer(6);

            //then
            resultShip.Size.Should().Be(4);
            resultShip.Coordinates.Column.Should().BeLessThan(6);
            resultShip.Coordinates.Row.Should().BeLessThan(6);
        }

        [Fact]
        public void GivenRandomShipGenerator_WhenGenerateBattleship_ThenValidShipReturned()
        {
            //given
            _randomCoordniatesGenerator.GetRandomCoordiantes(6).Returns(new Coordinates(1, 4));

            //when
            var resultShip = _randomShipGenerator.GenerateBattleship(6);

            //then
            resultShip.Size.Should().Be(5);
            resultShip.Coordinates.Column.Should().BeLessThan(6);
            resultShip.Coordinates.Row.Should().BeLessThan(6);
        }
    }
}
