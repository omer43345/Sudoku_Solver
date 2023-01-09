using System.Collections.Generic;
using Sudoku_Solver.DancingLinksAlgorithm;

namespace Sudoku_Solver.Solvers
{
    public static class DancingLinksSolver
    {
        public static byte[,] Solve(byte[,] sudoku)
        {
            DancingLinksUtils dancingLinksUtils = new DancingLinksUtils(sudoku);
            int[] coverArray = dancingLinksUtils.BuildCoverArray();
            DLX dlx = new DLX(coverArray, sudoku.GetLength(0));
            Stack<DancingLinksNode> solution = dlx.GetSolution();
            if (solution.Count == 0)
                return null;
            return dancingLinksUtils.ConvertDlxResultToSudoku(solution);
        }
    }
}