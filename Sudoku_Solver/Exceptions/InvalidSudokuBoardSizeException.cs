using System;


namespace Sudoku_Solver.Exceptions
{
    public class InvalidSudokuBoardSizeException : Exception
    {
        // Exception thrown when the size of the Sudoku board is invalid.
        public InvalidSudokuBoardSizeException(int size) : base(
            "Invalid sudoku board size, string length is not valid for representing a valid sudoku board , the size was " +
            size)
        {
        }

        public InvalidSudokuBoardSizeException(string message) : base(message)
        {
        }

        public InvalidSudokuBoardSizeException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}