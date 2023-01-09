using System;
using System.Collections.Generic;

namespace Sudoku_Solver.DancingLinksAlgorithm
{
    public class DancingLinksUtils
    {
        private readonly byte _sudokuBoardSize; // sudoku board size
        private readonly byte _sudokuBoxSize; // sudoku box size
        private readonly byte _constraintCount; // number of constraints, 4 for sudoku (row, column, box, value)
        private readonly int _sizeSquared; // sudoku board size squared
        private readonly byte[,] _sudokuBoard; // sudoku board

        public DancingLinksUtils(byte[,] sudokuBoard)
        {
            _sudokuBoard = sudokuBoard;
            _sudokuBoardSize = (byte)sudokuBoard.GetLength(0);
            _sudokuBoxSize = (byte)Math.Sqrt(_sudokuBoardSize);
            _sizeSquared = _sudokuBoardSize * _sudokuBoardSize;
            _constraintCount = 4;
        }


        // get the index of a column for a Cell constraint in the cover matrix for a given row, column, value
        private int GetCellColumnIndex(int row, int column)
        {
            return row * _sudokuBoardSize + column;
        }

        // get the index of a column for a Row constraint in the cover matrix for a given row, value
        private int GetRowColumnIndex(int row, int value)
        {
            return _sizeSquared + row * _sudokuBoardSize + value;
        }

        // get the index of a column for a Column constraint in the cover matrix for a given column, value
        private int GetColumnColumnIndex(int column, int value)
        {
            return _sizeSquared * 2 + column * _sudokuBoardSize + value;
        }

        // get the index of a column for a Box constraint in the cover matrix for a given box, value    
        private int GetBoxColumnIndex(int row, int column, int value)
        {
            return _sizeSquared * 3 +
                   (row / _sudokuBoxSize * _sudokuBoxSize + column / _sudokuBoxSize) * _sudokuBoardSize + value;
        }


        // build the cover array that contains the columns index in the chained list for every allowed value in the sudoku board
        public int[] BuildCoverArray()
        {
            int[] coverArray = new int[_sizeSquared * _sudokuBoardSize * _constraintCount];
            int coverArrayIndex=0;
            for (int row = 0; row < _sudokuBoardSize; row++)
            {
                for (int column = 0; column < _sudokuBoardSize; column++)
                {
                    for (int value = 0; value < _sudokuBoardSize; value++)
                    {
                        if (_sudokuBoard[row, column] == 0 || _sudokuBoard[row, column] == value + 1)
                        {
                            coverArray[coverArrayIndex]= GetCellColumnIndex(row, column);
                            coverArrayIndex++;
                            coverArray[coverArrayIndex] = GetRowColumnIndex(row, value);
                            coverArrayIndex++;
                            coverArray[coverArrayIndex] = GetColumnColumnIndex(column, value);
                            coverArrayIndex++;
                            coverArray[coverArrayIndex] = GetBoxColumnIndex(row, column, value);
                            coverArrayIndex++;
                        }
                    }
                }
            }
            return coverArray;
        }
        // Convert the the exact cover solution to the solved sudoku
        public byte[,] ConvertDlxResultToSudoku(Stack<DancingLinksNode> answer)
        {
            byte[,] solvedSudoku = new byte[_sudokuBoardSize, _sudokuBoardSize];
            while (answer.Count > 0)
            {
                DancingLinksNode cellIndexNode = answer.Pop();
                int min = int.Parse(cellIndexNode.Column.ColumnName);

                for (DancingLinksNode tempNode = cellIndexNode.Right; tempNode != cellIndexNode; tempNode = tempNode.Right)
                {
                    int val = int.Parse(tempNode.Column.ColumnName);
                    if (val < min)
                    {
                        min = val;
                        cellIndexNode = tempNode;
                    }
                }

                int cellIndex = int.Parse(cellIndexNode.Column.ColumnName);
                int row = cellIndex / _sudokuBoardSize;
                int col = cellIndex % _sudokuBoardSize;
                int value = int.Parse(cellIndexNode.Right.Column.ColumnName) % _sudokuBoardSize + 1;
                solvedSudoku[row, col] = (byte)value;
            }

            return solvedSudoku;
        }
    }
}