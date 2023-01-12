using Sudoku_Solver.Solvers;

namespace Sudoku_Solver
{
    public static class Solver
    {
        // The method that solves the sudoku puzzle
        public static string Solve(byte[,] sudoku)
        {
            return DancingLinksSolver.Solve(sudoku);
        }
    }
}