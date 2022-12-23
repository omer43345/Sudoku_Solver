using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Solver.Exceptions
{
    public class InvalidSudokuBoardSize : Exception
    {
        public InvalidSudokuBoardSize(int size) : base("Invalid sudoku board size, string length must be rootable perfectly twice, but the size was " + size)
        {
        }
        public InvalidSudokuBoardSize(string message) : base(message)
        {
        }
        public InvalidSudokuBoardSize(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

}
