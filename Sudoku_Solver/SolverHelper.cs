using System.Collections.Generic;

namespace Sudoku_Solver
{
    public static class PreSolver
    {
        private static int[,] _sudoku;
        private static int _size;
        private static BitWiseSudokuSolver _solver;

        public static void OptimizeBoard(int[,] sudoku, BitWiseSudokuSolver solver)
        {
            _solver = solver;
            _sudoku = sudoku;
            _size = sudoku.GetLength(0);
            HiddenSingles();

        }
        private static void HiddenSingles()
        {
            bool changed = false;
            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    if (_sudoku[i, j] == 0)
                    {
                        int count = _solver.CountCandidates(i, j);
                        if (count == 1)
                        {
                            for (int value = 1; value <= _size; value++)
                                if (_solver.CanPlace(i, j, value))
                                {
                                    _sudoku[i, j] = value;
                                    _solver.SetCandidate( i, j, value, true);
                                }
                            changed = true;
                        }
                    }
                }
            }
            if (changed)
                HiddenSingles();
        }
        private static bool HiddenPairs()
        {
            bool changed = false;
            // this algorithm reduce the number of candidates for each cell
            // by removing the candidates that are not in a pair
            
            return changed;
        }
        




    }
}