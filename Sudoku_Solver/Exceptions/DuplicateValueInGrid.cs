using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Solver.Exceptions
{
    // Exception thrown when there is a duplicate value in a grid.
    public class DuplicateValueInGrid : Exception
    {
        public DuplicateValueInGrid(int grid, char value) : base("Duplicate value '" + value + "' in grid " + grid)
        {
        }
        public DuplicateValueInGrid(string message) : base(message)
        {
        }
        public DuplicateValueInGrid(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
