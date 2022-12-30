using System;
using System.Diagnostics;

namespace Sudoku_Solver
{
    public class BitWiseSudokuSolver
    {
        private int[] _rowsCandidates;// rows candidates
        private int[] _colsCandidates;// columns candidates
        private int[] _boxesCandidates;// boxes candidates
        private int[,] _sudokuBoard;// sudoku board to solve
        private int _size;// size of the sudoku board
        private int _gridSize;// size of the grid
        public BitWiseSudokuSolver(int[,] sudokuBoard)
        {
            _sudokuBoard = sudokuBoard;
            _size = sudokuBoard.GetLength(0);
            _gridSize = (int)Math.Sqrt(_size);
            _rowsCandidates = new int[_size];
            _colsCandidates = new int[_size];
            _boxesCandidates = new int[_size];
            InitializeCandidates();
            UpdateCandidates();
        }
        public bool BitWiseSolver()
        {
            return Solve();
        }
        // initialize the candidates for each row, column and box
        private void InitializeCandidates()
        {
            for (int i = 0; i < _size; i++)
            {
                _rowsCandidates[i] = _colsCandidates[i] = _boxesCandidates[i] = 0;
            }
        }

        // Update the candidates for each row, column and box
        private void UpdateCandidates()
        {
            for (int row = 0; row < _size; row++)
            {
                for (int col = 0; col < _size; col++)
                {
                    if (_sudokuBoard[row, col] != 0)
                    {
                        int value = _sudokuBoard[row, col];
                        int box = GetBox(row, col);
                        SetCandidate( row, col, value,true);
                    }
                }
            }
        }
        // get the box number for a given row and column
        private int GetBox(int row, int col)
        {
            return (row / _gridSize) * _gridSize + (col / _gridSize);
        }
        
        // Count the number of candidates for a given cell
        private int CountCandidates(int row, int column)
        {
            int box = GetBox(row, column);
            int candidates = _rowsCandidates[row] | _colsCandidates[column] | _boxesCandidates[box];
            int count = 0;
            for (int i = 1; i <= _size; i++)
            {
                if ((candidates & (1 << i)) == 0)
                    count++;
            }
            return count;
        }
        // Check if a value is a candidate for a given cell
        private bool CanPlace(int row, int col, int value)
        {
            int flag = 1 << (value - 1);
            return (_rowsCandidates[row] & flag) == 0 && (_colsCandidates[col] & flag) == 0 && (_boxesCandidates[(row / _gridSize) * _gridSize + (col / _gridSize)] & flag) == 0;
        }
        // Set or unset a candidate for a given cell
        private void SetCandidate(int row, int col, int value, bool set)
        {
            int flag = 1 << (value - 1);
            if (set)
            {
                _rowsCandidates[row] |= flag;
                _colsCandidates[col] |= flag;
                _boxesCandidates[(row / _gridSize) * _gridSize + (col / _gridSize)] |= flag;
            }
            else
            {
                _rowsCandidates[row] &= ~flag;
                _colsCandidates[col] &= ~flag;
                _boxesCandidates[(row / _gridSize) * _gridSize + (col / _gridSize)] &= ~flag;
            }
        }
        
        // Solve the sudoku
        private bool Solve()
        {
            // find the cell with the least number of candidates
            int minCandidates = _size + 1;
            int minRow = -1;
            int minCol = -1;
            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    if (_sudokuBoard[i, j] == 0)
                    {
                        int count = CountCandidates(i, j);
                        if (count < minCandidates)
                        {
                            minCandidates = count;
                            minRow = i;
                            minCol = j;
                        }
                    }
                }
            }
            if (minCandidates == _size + 1)
            {
                // No empty cell found, so we are done
                return true;
            }
            if (minCandidates == 0)
                // No candidate found for the cell, so we are done
                return false;
            // Try each value for the cell with the least number of candidates
            for (int value = 1; value <= _size; value++)
            {
                if (CanPlace( minRow, minCol, value))
                {
                    // Set the value
                    _sudokuBoard[minRow, minCol] = value;
                    SetCandidate(minRow, minCol, value, true);
                    // Recursively solve the rest of the board
                    if (Solve())
                        return true;
                    // Undo the value
                    _sudokuBoard[minRow, minCol] = 0;
                    SetCandidate(minRow, minCol, value, false);
                }
            }
            return false;
        }
    }
}