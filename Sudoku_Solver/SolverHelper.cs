namespace Sudoku_Solver
{
    /*
     * This class contain functions to help the binary search algorithm to find the solution of the sudoku puzzle.
     */
    public class SolverHelper
    {
        private readonly byte[,] _sudoku;
        private readonly int _size;
        private readonly BitWiseSudokuSolver _solver;

        public SolverHelper(byte[,] sudoku, BitWiseSudokuSolver solver)
        {
            _sudoku = sudoku;
            _size = sudoku.GetLength(0);
            _solver = solver;
        }

        // This method return the best empty cell to put a number in
        // Also this method doing simple elimination if it should be done
        public int FindBestEmptyCell()
        {
            var minCandidates = _size + 1;
            var cell = -1;
            for (var row = 0; row < _size; row++)
            {
                for (var col = 0; col < _size; col++)
                {
                    if (_sudoku[row, col] == 0)
                    {
                        var count = _solver.CountCandidates(row, col);
                        if (count < minCandidates)
                        {
                            // update the best cell
                            minCandidates = count;
                            cell = row * _size + col;
                        }
                        else if (count == 1)
                            // Simple elimination if there is only one candidate for a cell
                            SimpleElimination(row, col);
                    }
                }
            }

            return cell;
        }

        // The method get called when there is only one candidate for a cell and put the number in the cell and update the candidates
        private void SimpleElimination(int row, int col)
        {
            for (var value = 1; value <= _size; value++)
            {
                if (_solver.CanPlace(_solver.GetCandidates(row, col), value))
                {
                    _sudoku[row, col] = (byte)value;
                    _solver.SetCandidate(row, col, value);
                }
            }
        }

        // Copy the matrix
        public byte[,] CopyBoard(byte[,] matrix)
        {
            byte[,] copy = new byte[_size, _size];
            for (var i = 0; i < _size; i++)
            {
                for (var j = 0; j < _size; j++)
                {
                    copy[i, j] = matrix[i, j];
                }
            }

            return copy;
        }
    }
}