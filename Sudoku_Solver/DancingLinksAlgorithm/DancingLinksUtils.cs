using System;
using System.Collections.Generic;

namespace Sudoku_Solver.DancingLinksAlgorithm
{
    /*
     * This class is used to build the cover array for the dancing links algorithm and also to convert the exact cover result to a sudoku grid
     */
    public class DancingLinksUtils
    {
        private readonly byte _sudokuBoardSize; // sudoku board size
        private readonly byte _sudokuBoxSize; // sudoku box size
        private readonly int _sizeSquared; // sudoku board size squared
        private readonly byte[,] _sudokuBoard; // sudoku board

        public DancingLinksUtils(byte[,] sudokuBoard)
        {
            _sudokuBoard = sudokuBoard;
            _sudokuBoardSize = (byte)sudokuBoard.GetLength(0);
            _sudokuBoxSize = (byte)Math.Sqrt(_sudokuBoardSize);
            _sizeSquared = _sudokuBoardSize * _sudokuBoardSize;
        }


        // get the index of a column for a node in the chained list for a given row and column, in the Cell Constraint
        private int GetCellColumnIndex(int row, int column)
        {
            return row * _sudokuBoardSize + column;
        }

        // get the index of a column for a node in the chained list for a given row, column and value, in the Row Constraint
        private int GetRowColumnIndex(int row, int value)
        {
            return _sizeSquared + row * _sudokuBoardSize + value;
        }

        // get the index of a column for a node in the chained list for a given column, row and value, in the Column Constraint
        private int GetColumnColumnIndex(int column, int value)
        {
            return _sizeSquared * 2 + column * _sudokuBoardSize + value;
        }

        // get the index of a column for a node in the chained list for a given box, row and value, in the Box Constraint
        private int GetBoxColumnIndex(int row, int column, int value)
        {
            return _sizeSquared * 3 +
                   (row / _sudokuBoxSize * _sudokuBoxSize + column / _sudokuBoxSize) * _sudokuBoardSize + value;
        }


        /// <summary>
        /// Create an Array that contains all the nodes in the chained list. this Array represents the cover problem to solve.
        /// </summary>
        /// <returns> returns cover array that contain all the nodes in the chained list</returns>
        public int[] BuildCoverArray()
        {
            List<int> coverArrayList = new List<int>();
            for (int row = 0; row < _sudokuBoardSize; row++)
            {
                for (int column = 0; column < _sudokuBoardSize; column++)
                {
                    for (int value = 0; value < _sudokuBoardSize; value++)
                    {
                        if (_sudokuBoard[row, column] == 0 || _sudokuBoard[row, column] == value + 1)
                        {
                            // add the index of the column in the chained list for the Cell Constraint to the cover array
                            coverArrayList.Add(GetCellColumnIndex(row, column));
                            // add the index of the column in the chained list for the Row Constraint to the cover array
                            coverArrayList.Add(GetRowColumnIndex(row, value));
                            // add the index of the column in the chained list for the Column Constraint to the cover array
                            coverArrayList.Add(GetColumnColumnIndex(column, value));
                            // add the index of the column in the chained list for the Box Constraint to the cover array
                            coverArrayList.Add(GetBoxColumnIndex(row, column, value));
                        }
                    }
                }
            }

            return coverArrayList.ToArray();
        }

        /// <summary>
        /// Convert the dlx cover problem result to a solved sudoku board
        /// </summary>
        /// <param name="answer">contains the nodes that represent the solution for the cover problem</param>
        /// <returns> Solved sudoku board as byte[,] matrix</returns>
        public byte[,] ConvertDlxResultToSudoku(Stack<DancingLinksNode> answer)
        {
            byte[,] solvedSudoku = new byte[_sudokuBoardSize, _sudokuBoardSize]; // the solved sudoku
            while (answer.Count > 0) // while there are nodes in the answer stack
            {
                DancingLinksNode cellIndexNode = answer.Pop(); // get the node from the answer stack
                int min = int.Parse(cellIndexNode.Column.ColumnName);
                // iterate over the nodes in the row of the node and get the minimum value of the column name
                for (DancingLinksNode tempNode = cellIndexNode.Right;
                     tempNode != cellIndexNode;
                     tempNode = tempNode.Right)
                {
                    int val = int.Parse(tempNode.Column.ColumnName);
                    if (val < min) // updating the minimum column name
                    {
                        min = val;
                        cellIndexNode = tempNode;
                    }
                }

                int cellIndex = int.Parse(cellIndexNode.Column.ColumnName); // the minimum column name is the cell index
                int row = cellIndex / _sudokuBoardSize;
                int col = cellIndex % _sudokuBoardSize;
                // calculate the value of the cell by the column name of the node next to the node with the minimum column name
                int value = int.Parse(cellIndexNode.Right.Column.ColumnName) % _sudokuBoardSize + 1;
                solvedSudoku[row, col] = (byte)value;
            }

            return solvedSudoku;
        }
    }
}