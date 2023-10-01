using Battleship.RandomGenerators;
using FluentAssertions;

namespace BattleShip.Tests.RandomGenerators
{
    public class RandomCoordinatesGeneratorTests
    {
        private readonly RandomCoordinatesGenerator _randomCoordinatesGenerator;

        public RandomCoordinatesGeneratorTests()
        {
            _randomCoordinatesGenerator = new RandomCoordinatesGenerator();
        }

        [Fact]
        public void GivenRandomCoordinatesGenerator_WhenGetRandomCoordiantes_ThenCoordinatesWithinGridSizeReturned()
        {
            //given

            //when
            var result = _randomCoordinatesGenerator.GetRandomCoordiantes(10);

            //then
            result.Column.Should().BeLessThan(10);
            result.Row.Should().BeLessThan(10);
        }
    }
}
