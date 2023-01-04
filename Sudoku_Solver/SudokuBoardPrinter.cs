using System;

namespace Sudoku_Solver
{
    public static class SudokuBoardPrinter
    {
        // Print the board to the console window in a nice format
        public static void PrintBoard(byte[,] board)
        {
            var size = board.GetLength(0);
            var sqrt = (int)Math.Sqrt(size);
            for (int i = 0; i < size; i++)
            {
                if (i % sqrt == 0 && i!=0)
                {
                    Console.WriteLine(new string('-', size *3 + sqrt + 1));
                }
                for (var j = 0; j < size; j++)
                {
                    if (j % sqrt == 0)
                    {
                        Console.Write("|");
                    }

                    Console.Write(" "+(Char)(board[i, j]+'0')+" ");
                }
                Console.WriteLine("|");
            }
            
        }
    }
}