using System;

namespace Sudoku_Solver.Exceptions
{
    public class InvalidChoiceException : Exception
    {
        // Exception thrown when the user enters an invalid choice.
        public InvalidChoiceException() : base("Invalid choice, please enter a valid choice from the menu")
        {
        }

        public InvalidChoiceException(string message) : base(message)
        {
        }

        public InvalidChoiceException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
