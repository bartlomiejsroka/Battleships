namespace Battleship.ValueObjects
{
    public record Coordinates(short Column, short Row);


    internal static class CoordinatesExtension
    {
        internal static Coordinates? TranslateToCoordinates(this string? input)
        {
            input = input?.ToUpper();
            if (input?.Length < 2 || input?.Length > 3)
                return null;

            if (!char.IsLetter(input[0]))
                return null;

            if (!char.IsDigit(input[1]))
                return null;

            if (input?.Length == 2)
                return new Coordinates((short)(input[0] - 65), (short)(char.GetNumericValue(input[1]) - 1));

            else
                return new Coordinates((short)(input[0] - 65), (short)((char.GetNumericValue(input[1])) * 10 + (char.GetNumericValue(input[2])) - 1));
        }
    }
}
