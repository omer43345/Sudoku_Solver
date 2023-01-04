using System;

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

        // get the index of a row in the cover matrix for a given row, column, value
        private int GetCoverMatrixRowIndex(int row, int column, int value)
        {
            return row * _sizeSquared + column * _sudokuBoardSize + value;
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
        // Intialize the cover matrix columns length 
        private void InitializeCoverMatrixSize(byte[][] coverMatrix)
        {
            for (int i = 0; i < coverMatrix.Length; i++)
            {
                coverMatrix[i] = new byte[_constraintCount * _sizeSquared];
            }
        }
        // build the cover matrix for the sudoku board
        private byte[][] BuildCoverMatrix()
        {
            //_sizeSquared * _constraintCount
            byte[][] coverMatrix = new byte[_sudokuBoardSize * _sizeSquared][];
            InitializeCoverMatrixSize(coverMatrix);
            // for each row, column, value
            for (int row = 0; row < _sudokuBoardSize; row++) {
                for (int column = 0; column < _sudokuBoardSize; column++) {
                    for (int value = 0; value < _sudokuBoardSize; value++) {
                        // for cell constraint
                        coverMatrix[GetCoverMatrixRowIndex(row, column, value)][GetCellColumnIndex(row, column)] = 1;
                        // for row constraint
                        coverMatrix[GetCoverMatrixRowIndex(row, column, value)][GetRowColumnIndex(row, value)] = 1;
                        // for column constraint
                        coverMatrix[GetCoverMatrixRowIndex(row, column, value)][GetColumnColumnIndex(column, value)] = 1;
                        // for box constraint
                        coverMatrix[GetCoverMatrixRowIndex(row, column, value)][GetBoxColumnIndex(row, column, value)] = 1;

                    }
                }
            }
            return coverMatrix;
        }
        // insert the values from the sudoku board into the cover matrix
        public byte[][] ConvertToCoverMatrix()
        {
            byte[][] coverMatrix = BuildCoverMatrix();
            for (int row = 0; row < _sudokuBoardSize; row++){
                for (int column = 0; column < _sudokuBoardSize; column++){
                    int boardValue = _sudokuBoard[row, column];
                    if (boardValue!= 0)
                    {
                        for (int value = 1; value <= _sudokuBoardSize; value++){
                            if (value != boardValue)
                            {
                                FillRow(coverMatrix[GetCoverMatrixRowIndex(row, column, value-1)], 0);
                            }
                        }
                    }
                }
            }
            /*PrintCoverMatrix(coverMatrix);*/
            return coverMatrix;

        }
        // fill a row with a given value
        private void FillRow(byte[] row, byte value)
        {
            for (int column = 0; column < row.Length; column++)
            {
                row[column] = value;
            }
        }
        
        // temp method to print the cover matrix
        private void PrintCoverMatrix(byte[][] coverMatrix)
        {
            
            for (int row = 0; row < coverMatrix.Length; row++)
            {
                for (int column = 0; column < coverMatrix[row].Length; column++)
                {
                    // paint the cell red if it is a 1
                    if (coverMatrix[row][column] == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    Console.Write(coverMatrix[row][column]+" ");
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        
        }
    }

}