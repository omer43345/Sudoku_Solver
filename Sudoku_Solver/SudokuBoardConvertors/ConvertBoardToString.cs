using System;

namespace Sudoku_Solver.SudokuBoardConvertors
{
    public static class ConvertBoardToString
    {
        /*
         * Converts the board to a string.
         */
        public static string ConvertToString(byte[,] board)
        {
            string boardString = "";
            var size = board.GetLength(0);
            for (int row = 0; row < size; row++)
            {
                for (var column = 0; column < size; column++)
                {
                    boardString += (Char)(board[row, column] + '0');
                }
            }

            return boardString;
        }
    }
}