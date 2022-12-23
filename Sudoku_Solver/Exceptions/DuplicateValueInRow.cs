using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Solver.Exceptions
{
    // Exception thrown when there is a duplicate value in a row.
    public class DuplicateValueInRow:Exception
    {
        public DuplicateValueInRow(int row, char value) : base("Duplicate value '" + value + "' in row " + row)
        {
        }
        public DuplicateValueInRow(string message) : base(message)
        {
        }
        public DuplicateValueInRow(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
