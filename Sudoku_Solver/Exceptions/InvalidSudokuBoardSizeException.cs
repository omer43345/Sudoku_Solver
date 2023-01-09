using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Solver.Exceptions
{
    public class InvalidSudokuBoardSizeException : Exception
    {
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