using Battleship.ValueObjects;
using FluentAssertions;

namespace BattleShip.Tests.Coordinate
{
    public class CoordinatesExtensionTests
    {

        [Fact]
        public void GivenValidOneDigitCoordinatesString_WhenTranslate_ThenCoordinatesReturned()
        {
            //given
            var coordinates = "A5";

            //when
            var result = coordinates.TranslateToCoordinates();

            //then
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(new Coordinates(0, 4));
        }

        [Fact]
        public void GivenValidTwoDigitCoordinatesString_WhenTranslate_ThenCoordinatesReturned()
        {
            //given
            var coordinates = "B12";

            //when
            var result = coordinates.TranslateToCoordinates();

            //then
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(new Coordinates(1, 11));
        }

        [Fact]
        public void GivenIncorrectFormatCoordinates_WhenTranslate_ThenNullReturned()
        {
            //given
            var coordinates = "BB";

            //when
            var result = coordinates.TranslateToCoordinates();

            //then
            result.Should().BeNull();
        }

        [Fact]
        public void GivenTooShortCoordinates_WhenTranslate_ThenNullReturned()
        {
            //given
            var coordinates = "B";

            //when
            var result = coordinates.TranslateToCoordinates();

            //then
            result.Should().BeNull();
        }

        [Fact]
        public void GivenTooLongCoordinates_WhenTranslate_ThenNullReturned()
        {
            //given
            var coordinates = "B122";

            //when
            var result = coordinates.TranslateToCoordinates();

            //then
            result.Should().BeNull();
        }
    }
}
