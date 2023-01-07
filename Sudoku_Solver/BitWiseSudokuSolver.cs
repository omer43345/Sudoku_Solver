using System;

namespace Sudoku_Solver
{
    public class BitWiseSudokuSolver
    {
        private int[] _rowsCandidates; // rows candidates
        private int[] _colsCandidates; // columns candidates
        private int[] _boxesCandidates; // boxes candidates
        private readonly int[] _bitMask; // 
        private byte[,] _sudokuBoard; // sudoku board to solve
        private readonly int _size; // size of the sudoku board
        private readonly int _boxSize; // size of the grid

        public BitWiseSudokuSolver(byte[,] sudokuBoard)
        {
            _sudokuBoard = sudokuBoard;
            _size = sudokuBoard.GetLength(0);
            _boxSize = (int)Math.Sqrt(_size);
            _rowsCandidates = new int[_size];
            _colsCandidates = new int[_size];
            _boxesCandidates = new int[_size];
            _bitMask = new int[_size];
            InitializeCandidates();
            CreateBitMaskArray();
            UpdateCandidates();
        }

        private void CreateBitMaskArray()
        {
            for (var value = 1; value <= _size; value++)
            {
                _bitMask[value - 1] = 1 << (value - 1);
            }
        }

        public byte[,] GetSolvedSudokuBoard()
        {
            return _sudokuBoard;
        }

        public bool BitWiseSolver()
        {
            return Solve();
        }

        // initialize the candidates for each row, column and box
        private void InitializeCandidates()
        {
            for (var i = 0; i < _size; i++)
            {
                _bitMask[i] = _rowsCandidates[i] = _colsCandidates[i] = _boxesCandidates[i] = 0;
            }
        }

        // Update the candidates for each row, column and box
        private void UpdateCandidates()
        {
            for (var row = 0; row < _size; row++)
            {
                for (var col = 0; col < _size; col++)
                {
                    if (_sudokuBoard[row, col] == 0) continue;
                    int value = _sudokuBoard[row, col];
                    SetCandidate(row, col, value);
                }
            }
        }

        // get the box number for a given row and column
        private int GetBox(int row, int col)
        {
            return (row / _boxSize) * _boxSize + (col / _boxSize);
        }

        // Count the number of candidates for a given cell
        public int CountCandidates(int row, int column)
        {
            var box = GetBox(row, column);
            var candidates = _rowsCandidates[row] | _colsCandidates[column] | _boxesCandidates[box];
            var count = 0;
            for (var i = 0; i < _size; i++)
            {
                if ((candidates & (1 << i)) == 0)
                    count++;
            }

            return count;
        }

        // Check if a value is a candidate for a given cell
        public bool CanPlace(int candidates, int value)
        {
            return (candidates & _bitMask[value - 1]) == 0;
        }

        public int GetCandidates(int row, int col)
        {
            var box = GetBox(row, col);
            return _rowsCandidates[row] | _colsCandidates[col] | _boxesCandidates[box];
        }


        // Update the candidates arrays for a given cell and value
        public void SetCandidate(int row, int col, int value)
        {
            _rowsCandidates[row] |= _bitMask[value - 1];
            _colsCandidates[col] |= _bitMask[value - 1];
            _boxesCandidates[GetBox(row, col)] |= _bitMask[value - 1];
        }

        // Restore copies to the original matrix and candidates
        private void RestoreCopies(byte[,] boardCopy, int[] rowsCopy, int[] colsCopy, int[] boxesCopy)
        {
            _sudokuBoard = boardCopy;
            _rowsCandidates = rowsCopy;
            _colsCandidates = colsCopy;
            _boxesCandidates = boxesCopy;
        }

        // Solve the sudoku
        private bool Solve()
        {
            var solverHelper = new SolverHelper(_sudokuBoard, this);
            // make copies of the sudoku board and the candidates
            int[] rowCopy = new int[_size], colCopy = new int[_size], boxCopy = new int[_size];
            byte[,] boardCopy = solverHelper.CopyBoard(_sudokuBoard);
            Array.Copy(_rowsCandidates, rowCopy, _size);
            Array.Copy(_colsCandidates, colCopy, _size);
            Array.Copy(_boxesCandidates, boxCopy, _size);

            var cellIndex = solverHelper.FindBestEmptyCell();

            // if there is no cell with no candidates, the sudoku is solved
            if (cellIndex == -1)
                return true;
            var minRow = cellIndex / _size;
            var minCol = cellIndex % _size;

            // Try each value for the cell with the least number of candidates
            for (var value = 1; value <= _size; value--)
            {
                if (CanPlace(GetCandidates(minRow, minCol), value))
                {
                    _sudokuBoard[minRow, minCol] = (byte)value;
                    SetCandidate(minRow, minCol, value);

                    // if the sudoku is solved, return true
                    if (Solve())
                        return true;

                    // if the sudoku is not solved, restore the copies and try the next value
                    RestoreCopies(boardCopy, rowCopy, colCopy, boxCopy);
                }
            }

            return false;
        }
    }
}