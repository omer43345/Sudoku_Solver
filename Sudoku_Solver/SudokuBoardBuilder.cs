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
    public class SudokuBoardBuilder
    {
        private string _sudokuBoardString;
        private int[,] _sudokuBoard;
        private int _boardSize;
        public SudokuBoardBuilder(string sudokuBoardString)
        {
            _sudokuBoardString = sudokuBoardString;
            validateSize();
            _boardSize = (int)Math.Sqrt(_sudokuBoardString.Length);
            _sudokuBoard = new int[(int)Math.Sqrt(_sudokuBoardString.Length), (int)Math.Sqrt(_sudokuBoardString.Length)];
            boardBuilder();
        }
        // This method is used to validate the size of the string if it is suitable for a Sudoku board.
        private void validateSize()
        {
            double rowAndColumnCount = Math.Sqrt(_sudokuBoardString.Length);
            double gridSideLength = Math.Sqrt(rowAndColumnCount);
            if (gridSideLength % 1 != 0|| rowAndColumnCount%1!=0)
            {
                throw new InvalidSudokuBoardSizeException(_sudokuBoardString.Length);
            }

        }
        // This method is used to build the Sudoku board from the string.
        private void boardBuilder()
        {
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
        }
        public int[,] GetSudokuBoard()
        {
            return _sudokuBoard;
        }
    }
}
