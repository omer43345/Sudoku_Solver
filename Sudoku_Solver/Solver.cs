using System;

namespace Sudoku_Solver
{
    public static class Solver
    {
        public static byte[,] Solve(byte[,] sudoku)
        {
            var solver = new BitWiseSudokuSolver(sudoku);
            solver.BitWiseSolver();
            return solver.GetSolvedSudokuBoard();
        }

    }
}