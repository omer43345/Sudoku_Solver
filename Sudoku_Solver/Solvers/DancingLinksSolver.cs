using System.Collections.Generic;
using Sudoku_Solver.DancingLinksAlgorithm;

namespace Sudoku_Solver.Solvers
{
    public static class DancingLinksSolver
    {
        public static byte[,] Solve(byte[,] sudoku)
        {
            DancingLinksUtils dancingLinksUtils = new DancingLinksUtils(sudoku);
            byte[,] coverMatrix = dancingLinksUtils.BuildCoverMatrix();
            DLX dlx = new DLX(coverMatrix);
            Stack<DancingLinksNode> solution = dlx.GetSolution();
            if (solution.Count==0)
                return null;
            return dancingLinksUtils.ConvertDLXResultToSudoku(solution);
        }
    }
}