using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Solver.Exceptions
{
    public class InvalidSudokuBoardSizeException : Exception
    {
        public InvalidSudokuBoardSizeException(int size) : base("Invalid sudoku board size, string length must be rootable perfectly twice, but the size was " + size)
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
