﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Solver.Exceptions
{
    // Exception thrown when there is a duplicate value in a column.
    public class DuplicateValueInColumn : Exception
    {
        public DuplicateValueInColumn(int column, char value) : base("Duplicate value '" + value + "' in column " + column)
        {
        }
        public DuplicateValueInColumn(string message) : base(message)
        {
        }
        public DuplicateValueInColumn(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

}