using Sudoku_Solver.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sudoku_Solver
{
    /*
    * This class is used to validate a Sudoku board.
      It is used to check if a Sudoku board is valid before solving it.
    */

    public static class SudokuValidator
    {
        private static int[,] _sudokuBoard;// the Sudoku board to validate
        private static int _boardSize;// size of the board
        public static void Validate(int[,] sudokuBoard)
        {
            _sudokuBoard = sudokuBoard;
            _boardSize = sudokuBoard.GetLength(0);
            ValidateDuplicateValues();
        }


        // This method is used to validate if the board contains duplicate values in a row, column or a grid.
        private static void ValidateDuplicateValues()
        {
            ValidateRows();
            ValidateColumns();
            ValidateGrid();
        }

        // This method is used to validate if there is duplicate values in one of the rows.
        private static void ValidateRows()
        {
            for (int row = 0; row < _boardSize; row++)
            {
                ValidateRow(row);
            }
        }
        // This method is used to validate if there is duplicate values in the row that is passed as a parameter.
        private static void ValidateRow(int row)
        {
            //check if there is duplicate values in the row that is passed as a parameter
            
            for (int i = 0; i < _boardSize; i++)
            {
                for (int j = i + 1; j < _boardSize; j++)
                {
                    if (_sudokuBoard[row, i]!=0 && _sudokuBoard[row, i] == _sudokuBoard[row, j])
                    {
                        throw new DuplicateValueInRowException(row + 1, (char)(_sudokuBoard[row, i]+48));
                    }
                }
            }
        }
        // This method is used to validate if there is duplicate values in one of the columns.
        private static void ValidateColumns()
        {
            for (int column = 0; column < _boardSize; column++)
            {
                ValidateColumn(column);
            }
        }
        // This method is used to validate if there is duplicate values in the column that is passed as a parameter.
        private static void ValidateColumn(int column)
        {
            //check if there is duplicate values in the column that is passed as a parameter
            for (int i = 0; i < _boardSize; i++)
            {
                for (int j = i + 1; j < _boardSize; j++)
                {
                    if (_sudokuBoard[i, column] !=0 && _sudokuBoard[i, column] == _sudokuBoard[j, column])
                    {
                        throw new DuplicateValueInColumnException(column + 1, (char)(_sudokuBoard[i, column]+48));
                    }
                }
            }
        }
        // This method is used to validate if there is duplicate values in one of the grids.
        private static void ValidateGrid()
        {
            int gridSize = (int)Math.Sqrt(_boardSize);
            for (int grid = 0; grid < _boardSize; grid++)
            {
                ValidateGrid(grid, gridSize);
            }

        }
        // This method is used to validate if there is duplicate values in the grid that is passed as a parameter.
        private static void ValidateGrid(int grid,int gridSize)
        {
            
            int row = (grid / gridSize) * gridSize;
            int column = (grid % gridSize) * gridSize;
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    for (int k = i; k < gridSize; k++)
                    {
                        for (int l = (k == i) ? j + 1 : 0; l < gridSize; l++)
                        {
                            if (_sudokuBoard[row + i, column + j]!=0 && _sudokuBoard[row + i, column + j] == _sudokuBoard[row + k, column + l])
                            {
                                throw new DuplicateValueInGridException(grid + 1, (char)(_sudokuBoard[row + i, column + j]+48));
                            }
                        }
                    }
                }
            }
        }


    }
}
