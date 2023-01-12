using System;


namespace Sudoku_Solver.Exceptions
{
    // Exception thrown when there is a duplicate value in a column.
    public class DuplicateValueInColumnException : Exception
    {
        public DuplicateValueInColumnException(int column, char value) : base("Duplicate value '" + value +
                                                                              "' in column " + column)
        {
        }

        public DuplicateValueInColumnException(string message) : base(message)
        {
        }

        public DuplicateValueInColumnException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}