using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Solver
{
    /*
    * This class is used to validate a Sudoku board.
      It is used to check if a Sudoku board is valid before solving it.
    */

    internal class SudokuValidator
    {
        private char[] _allowedValues;// allowed values for a Sudoku board
        private char[,] _sudokuBoard;// the Sudoku board to validate
        private char _emptyValue;// empty value in the board
        public SudokuValidator(char[,] sudokuBoard, char[] allowedValues,char emptyValue)
        {
            _sudokuBoard = sudokuBoard;
            _allowedValues = allowedValues;
            _emptyValue = emptyValue;
        }
        static void Main(string[] args)
        {
            // checking this class functions
            char[] allowedValues = { '0','1','2','3', '4', '5', '6', '7', '8', '9' };
            char emptyValue = '0';
            char[,] sudokuBoard = {
                    {'5','3','0','0','7','0','0','0','0'},
                    {'6','0','0','1','9','5','0','0','0'},
                    {'0','9','0','0','0','0','0','6','0'},
                    {'8','0','0','0','6','0','0','0','3'},
                    {'4','0','0','8','0','3','0','0','1'},
                    {'7','0','0','0','2','0','0','0','6'},
                    {'0','6','0','0','0','0','9','8','0'},
                    {'0','0','0','4','1','9','0','0','5'},
                    {'0','0','0','0','8','0','0','7','3'}
                };
            SudokuValidator validator = new SudokuValidator(sudokuBoard, allowedValues, emptyValue);
            validator.Validate();
        }
        public bool Validate()
        {
            ValidateAllowedValues();
            ValidateDuplicateValues();
            return true;

        }


        //This method is used to validate if the board contains only allowed values.
        private void ValidateAllowedValues()
        {
            int cellNumber = 0;
            foreach (char cellValue in _sudokuBoard)
            {
                if (!_allowedValues.Contains(cellValue))
                {
                    throw new Exception("Invalid character in sudoku board in row " + (cellNumber /_sudokuBoard.GetLength(0)+1) +" column " + (cellNumber%_sudokuBoard.GetLength(1)+1));
                }
                cellNumber++;
            }
        }

        // This method is used to validate if the board contains duplicate values in a row, column or a grid.
        private void ValidateDuplicateValues()
        {
            ValidateRows();
            ValidateColumns();
            ValidateGrid();
        }

        // This method is used to validate if there is duplicate values in one of the rows.
        private void ValidateRows()
        {
            for (int row = 0; row < _sudokuBoard.GetLength(0); row++)
            {
                ValidateRow(row);
            }
        }
        // This method is used to validate if there is duplicate values in the row that is passed as a parameter.
        private void ValidateRow(int row)
        {
            List<char> values = new List<char>();
            for (int column = 0; column < _sudokuBoard.GetLength(1); column++)
            {
                char cellValue = _sudokuBoard[row, column];
                if (cellValue != _emptyValue)
                {
                    if (values.Contains(cellValue))
                    {
                        throw new Exception("Duplicate value in row " + (row+1));
                    }
                    values.Add(cellValue);
                }
            }
        }
        // This method is used to validate if there is duplicate values in one of the columns.
        private void ValidateColumns()
        {
            for (int column = 0; column < _sudokuBoard.GetLength(1); column++)
            {
                ValidateColumn(column);
            }
        }
        // This method is used to validate if there is duplicate values in the column that is passed as a parameter.
        private void ValidateColumn(int column)
        {
            List<char> values = new List<char>();
            for (int row = 0; row < _sudokuBoard.GetLength(0); row++)
            {
                char cellValue = _sudokuBoard[row, column];
                if (cellValue != _emptyValue)
                {
                    if (values.Contains(cellValue))
                    {
                        throw new Exception("Duplicate value in column " + (column+1));
                    }
                    values.Add(cellValue);
                }
            }
        }
        // This method is used to validate if there is duplicate values in one of the grids.
        private void ValidateGrid()
        {
            int gridSize = (int)Math.Sqrt(_sudokuBoard.GetLength(0));
            for (int grid = 0; grid < _sudokuBoard.GetLength(0); grid++)
            {
                ValidateGrid(grid, gridSize);
            }

        }
        // This method is used to validate if there is duplicate values in the grid that is passed as a parameter.
        private void ValidateGrid(int grid,int gridSize)
        {
            List<char> values = new List<char>();
            int gridRow = (grid / gridSize) * gridSize;
            int gridColumn = (grid % gridSize) * gridSize;
            for (int row = gridRow; row < (gridRow + gridSize); row++)
            {
                for (int column = gridColumn; column < (gridColumn + gridSize); column++)
                {
                    char cellValue = _sudokuBoard[row, column];
                    if (cellValue != _emptyValue)
                    {
                        if (values.Contains(cellValue))
                        {
                            throw new Exception("Duplicate value in grid " + (grid + 1));
                        }
                        values.Add(cellValue);
                    }
                }
            }
        }


    }
}
