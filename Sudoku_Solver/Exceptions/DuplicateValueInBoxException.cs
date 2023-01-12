using System;


namespace Sudoku_Solver.Exceptions
{
    // Exception thrown when there is a duplicate value in a grid.
    public class DuplicateValueInBoxException : Exception
    {
        public DuplicateValueInBoxException(int grid, char value) : base("Duplicate value '" + value + "' in box " +
                                                                          grid)
        {
        }

        public DuplicateValueInBoxException(string message) : base(message)
        {
        }

        public DuplicateValueInBoxException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}