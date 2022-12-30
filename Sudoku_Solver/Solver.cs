using System;

namespace Sudoku_Solver
{
    public static class Solver
    {
        public static bool Solve(int[,] sudoku)
        {

            while (PreSolver.OptimizeBoard(sudoku)) { }
            BitWiseSudokuSolver solver = new BitWiseSudokuSolver(sudoku);
            return solver.BitWiseSolver();
        }

    }
}