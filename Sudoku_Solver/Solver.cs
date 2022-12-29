namespace Sudoku_Solver
{
    public class Solver
    {
        private int[,] _sudoku;
        public Solver(int[,] sudoku)
        {
            _sudoku = sudoku;
        }

        public void Solve()
        {
            PreSolver preSolver = new PreSolver(_sudoku);
            while (preSolver.OptimizeBoard()) { }
            BitWiseSudokuSolver solver = new BitWiseSudokuSolver(_sudoku);
            solver.BitWiseSolver();
        }
        
    }
}