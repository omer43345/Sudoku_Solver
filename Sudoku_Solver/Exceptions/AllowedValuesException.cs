﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Solver.Exceptions
{
    // Exception thrown when a value is not allowed in a Sudoku board.
    public class AllowedValuesException : Exception
    {
        public AllowedValuesException(int row, int column) : base("Invalid character in sudoku board in row " + row + " column " + column)
        {
            
        }
        public AllowedValuesException(string message) : base(message)
        {
        }
        public AllowedValuesException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
    
    
}