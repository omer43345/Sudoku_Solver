using System;

namespace Sudoku_Solver.Output
{
    /*
     * This class contains ways to print the sudoku
     */
    public static class SudokuBoardPrinter
    {
        // Print the board in a nice format
        public static void PrintBoard(string board)
        {
            var size = (int)Math.Sqrt(board.Length);
            var sqrt = (int)Math.Sqrt(size);

            for (int row = 0; row < size; row++)
            {
                if (row % sqrt == 0 && row != 0)
                {
                    Console.WriteLine(new string('-', size * 3 + sqrt + 1));
                }

                for (int column = 0; column < size; column++)
                {
                    if (column % sqrt == 0)
                    {
                        Console.Write("|");
                    }

                    Console.Write(" " + (board[row * size + column]) + " ");
                }

                Console.WriteLine("|");
            }
        }

        // Print the board as a string
        public static void PrintBoardString(string board)
        {
            Console.WriteLine(board);
        }
    }
}