using System;

namespace Sudoku_Solver
{
    public class BitWiseSudokuSolver
    {
        
        private int[,] _sudokuBoard;// sudoku board to solve
        private int _size;// size of the sudoku board
        private int[,] _candidates;// candidates for each cell
        private int _gridSize;// size of the grid
        public BitWiseSudokuSolver(int[,] sudokuBoard)
        {
            _sudokuBoard = sudokuBoard;
            _size = sudokuBoard.GetLength(0);
            _candidates = new int[_size, _size];
            _gridSize = (int)Math.Sqrt(_size);
            InitializeCandidates();
            UpdateCandidates();
            Solve();
            printBoard();
        }
        // temp method to print the board
        private void printBoard()
        {
            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    Console.Write(" "+_sudokuBoard[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
        // initialize the candidates array
        private void InitializeCandidates()
        {
            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    _candidates[i, j] = 0;
                }
            }
        }

        // Update the candidate array based on the initial values on the board
        private void UpdateCandidates()
        {
            for (int row = 0; row < _size; row++)
            {
                for (int col = 0; col < _size; col++)
                {
                    if (_sudokuBoard[row, col] != 0)
                    {
                        UpdateOtherCandidates( row, col, _sudokuBoard[row, col]);
                        _candidates[row, col] = 0;
                    }
                }
            }
        }
        // Count the number of candidates for a given cell
        private int CountCandidates(int row, int column)
        {
            int count = 0;
            int candidate = _candidates[row, column];
            for (int value = 1; value <= _size; value++)
            {
                if (IsCandidate(candidate, value))
                {
                    count++;
                }

            }
            return count;
        }
        // Check if a value is a candidate for a given cell
        private bool IsCandidate(int candidate, int value)
        {
            int flag = 1 << (value - 1);
            return (candidate & flag) == 0;
        }
        // Update the candidates for a given cell
        private void SetCandidate(int row, int col, int value, bool isCandidate)
        {
               int flag = 1 << (value - 1);
                if (isCandidate)
                {
                 _candidates[row, col] |= flag;
                }
                else
                {
                 _candidates[row, col] &= ~flag;
                }
        }
        // Update the candidates for in the same row, column and grid of a given cell
        private void UpdateOtherCandidates(int row, int col, int value)
        {
            // Update the row
            for (int j = 0; j < _size; j++)
            {
                SetCandidate(row, j, value, true);
            }
            // Update the column
            for (int i = 0; i < _size; i++)
            {
                SetCandidate(i, col, value, true);
            }
            // Update the grid
            int gridRow = row / _gridSize;
            int gridCol = col / _gridSize;
            for (int i = gridRow * _gridSize; i < (gridRow + 1) * _gridSize; i++)
            {
                for (int j = gridCol * _gridSize; j < (gridCol + 1) * _gridSize; j++)
                {
                    SetCandidate(i, j, value, true);
                }
            }
        }

        // Solve the sudoku
        public bool Solve()
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
            // Try each value for the cell with the least number of candidates
            for (int value = 1; value <= _size; value++)
            {
                if (IsCandidate(_candidates[minRow, minCol], value))
                {
                    // Try this value
                    _sudokuBoard[minRow, minCol] = value;
                    UpdateOtherCandidates(minRow, minCol, value);
                    if (Solve())
                    {
                        return true;
                    }
                    // Undo the assignment
                    _sudokuBoard[minRow, minCol] = 0;
                    InitializeCandidates();
                    UpdateCandidates();
                }
            }
            return false;
        }
    }
}