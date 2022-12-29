using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Solver.Exceptions
{
    // Exception thrown when there is a duplicate value in a grid.
    public class DuplicateValueInGridException : Exception
    {
        public DuplicateValueInGridException(int grid, char value) : base("Duplicate value '" + value + "' in grid " + grid)
        {
        }
        public DuplicateValueInGridException(string message) : base(message)
        {
        }
        public DuplicateValueInGridException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
