using Sudoku_Solver.Exceptions;
using System;


namespace Sudoku_Solver.SudokuBoardConvertors
{
    // This class creates a Sudoku board from a string of characters representing the board.
    public static class SudokuBoardBuilder
    {
        private static string _sudokuBoardString;
        private static byte[,] _sudokuBoard;
        private const int MaxBoardSize = 64;

        /// <summary>
        /// This method validate the sudoku string size
        /// </summary>
        /// <exception cref="InvalidSudokuBoardSizeException">raise this exception if the sudoku string size is not valid</exception>
        private static void ValidateSize()
        {
            double rowAndColumnCount = Math.Sqrt(_sudokuBoardString.Length);
            double boxSize = Math.Sqrt(rowAndColumnCount);
            // check if the sudoku string size is valid
            if (boxSize % 1 != 0 || rowAndColumnCount % 1 != 0 || _sudokuBoardString.Length == 0 || rowAndColumnCount > MaxBoardSize)
            {
                throw new InvalidSudokuBoardSizeException(_sudokuBoardString.Length);
            }

        }

        /// <summary>
        /// This method is used to build a Sudoku board from a string of characters.
        /// </summary>
        /// <param name="sudokuBoardString">the sudoku as a string</param>
        /// <returns>the sudoku board as a byte[,] matrix</returns>
        /// <exception cref="AllowedValuesException">raise when there is a char that not allowed according to the size
        /// of the board</exception>
        public static byte[,] BoardBuilder(string sudokuBoardString)
        {
            _sudokuBoardString = sudokuBoardString;
            ValidateSize(); // validate the size of the string
            _sudokuBoard = new byte[(int)Math.Sqrt(_sudokuBoardString.Length),
                (int)Math.Sqrt(_sudokuBoardString.Length)]; // create a new Sudoku board
            int rowAndColumnCount = (int)Math.Sqrt(_sudokuBoardString.Length);
            int cellIndex = 0;
            // iterate over the string and add the values to the Sudoku board
            foreach (char cell in _sudokuBoardString)
            {
                int value = cell - '0';
                // check if the value is allowed according to the size of the board
                if (value < 0 || value > rowAndColumnCount)
                {
                    throw new AllowedValuesException(_sudokuBoardString.IndexOf(cell) / rowAndColumnCount,
                        _sudokuBoardString.IndexOf(cell) % rowAndColumnCount);
                }

                // add the value to the Sudoku board
                _sudokuBoard[cellIndex / rowAndColumnCount, cellIndex % rowAndColumnCount] = (byte)value;
                cellIndex++;
            }

            return _sudokuBoard;
        }
    }
}