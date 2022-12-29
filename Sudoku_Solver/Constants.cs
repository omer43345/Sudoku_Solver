using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace Sudoku_Solver
{
    // This class contains the allowed values for each cell in the grid for each possible board size
    public class Constants
    {
        // hash map for every sudoku board size and the allowed values for each cell in that board
        public static readonly char EMPTY_VALUE = '0';
        public static readonly Dictionary<int, char[]> allowedValues = new Dictionary<int, char[]>()
        {
            { 4, new char[] { '1', '2', '3', '4' } },
            { 9, new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' } },
            { 16, new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' , 'A', 'B', 'C', 'D', 'E', 'F', 'G' } }
        };
        public static int GetIndex(char value, int boardSize)
        {
            return allowedValues[boardSize].ToList().IndexOf(value);
        }
        public static char GetValue(int index, int boardSize)
        {
            return allowedValues[boardSize][index];
        }

        
        
    }
}