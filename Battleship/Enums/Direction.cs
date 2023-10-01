namespace Battleship.Enums
{
    public enum Direction
    {
        Up = 0,
        Right = 1,
        Down = 2,
        Left = 3
    }

    internal static class DirectionExtension
    {
        internal static Direction? TranslateToDirection(this string? direction)
            => direction?.ToUpper() switch
            {
                "U" => Direction.Up,
                "R" => Direction.Right,
                "D" => Direction.Down,
                "L" => Direction.Left,
                _ => null
            };

    }
}
