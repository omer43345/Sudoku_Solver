namespace Sudoku_Solver
{
    public static class Solver
    {
        public static void Solve(int[,] sudoku)
        {

            while (PreSolver.OptimizeBoard(sudoku)) { }
            BitWiseSudokuSolver solver = new BitWiseSudokuSolver(sudoku);
            solver.BitWiseSolver();
        }
        
    }
}