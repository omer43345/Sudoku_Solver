using Sudoku_Solver.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Sudoku_Solver
{
    // This class creates a Sudoku board from a string of characters representing the board.
    public static class SudokuBoardBuilder
    {
        private static string _sudokuBoardString;
        private static int[,] _sudokuBoard;
        private static int _boardSize;
        // This method is used to validate the size of the string if it is suitable for a Sudoku board.
        private static void validateSize()
        {
            double rowAndColumnCount = Math.Sqrt(_sudokuBoardString.Length);
            double gridSideLength = Math.Sqrt(rowAndColumnCount);
            if (gridSideLength % 1 != 0|| rowAndColumnCount%1!=0 || _sudokuBoardString.Length == 0)
            {
                throw new InvalidSudokuBoardSizeException(_sudokuBoardString.Length);
            }

        }
        // This method is used to build the Sudoku board from the string.
        public static int[,] BoardBuilder(string sudokuBoardString)
        {
            _sudokuBoardString = sudokuBoardString;
            validateSize();
            _boardSize = (int)Math.Sqrt(_sudokuBoardString.Length);
            _sudokuBoard = new int[(int)Math.Sqrt(_sudokuBoardString.Length), (int)Math.Sqrt(_sudokuBoardString.Length)];
            int rowAndColumnCount = (int)Math.Sqrt(_sudokuBoardString.Length);
            int cellIndex = 0;
            foreach (char cell in _sudokuBoardString)
            {
                int value=cell-'0';
                if (value < 0 || value > rowAndColumnCount)
                {
                    throw new AllowedValuesException(_sudokuBoardString.IndexOf(cell)/rowAndColumnCount, _sudokuBoardString.IndexOf(cell)%rowAndColumnCount);
                }
                _sudokuBoard[cellIndex / rowAndColumnCount, cellIndex % rowAndColumnCount]=value;
                cellIndex++;
            }
            return _sudokuBoard;
        }
    }
}
