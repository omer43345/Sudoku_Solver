using System;

namespace Sudoku_Solver
{
    public class SudokuBoardPrinter
    {
        // Print the board to the console window in a nice format
        public static void PrintBoard(int[,] board)
        {
            int size = board.GetLength(0);
            int sqrt = (int)Math.Sqrt(size);
            for (int i = 0; i < size; i++)
            {
                if (i % sqrt == 0 && i!=0)
                {
                    Console.WriteLine(new string('-', size *3 + sqrt + 1));
                }
                for (int j = 0; j < size; j++)
                {
                    if (j % sqrt == 0)
                    {
                        Console.Write("|");
                    }

                    Console.Write(" "+(char)(board[i, j]+'0')+" ");
                }
                Console.WriteLine("|");
            }
            
        }
    }
}