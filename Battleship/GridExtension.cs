namespace Battleship
{
    internal static class GridExtension
    {
        public static void DisplayGrid(this Cell[,] grid)
        {

            Console.WriteLine();

            for (int i = grid.GetLength(0) - 1; i >= 0; i--)
            {
                var displayCol = (i + 1).ToString();
                Console.Write(displayCol + (displayCol.Length > 1 ? " " : "  "));

                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    Console.Write(((char)grid[j, i].State.StateEnum).ToString() + " ");
                }
                Console.WriteLine();
            }

            Console.Write("   ");

            for (int i = 0; i <= grid.GetLength(0) - 1; i++)
            {
                Console.Write((char)('A' + i) + " ");
            }

            Console.WriteLine();
        }
    }
}
