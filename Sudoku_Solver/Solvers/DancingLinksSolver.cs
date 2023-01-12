using System.Collections.Generic;
using Sudoku_Solver.DancingLinksAlgorithm;
using Sudoku_Solver.SudokuBoardConvertors;

namespace Sudoku_Solver.Solvers
{
    public static class DancingLinksSolver
    {
        public static string Solve(byte[,] sudoku)
        {
            DancingLinksUtils dancingLinksUtils = new DancingLinksUtils(sudoku);
            int[] coverArray = dancingLinksUtils.BuildCoverArray();
            DLX dlx = new DLX(coverArray, sudoku.GetLength(0));
            Stack<DancingLinksNode> solution = dlx.GetSolution();
            if (solution.Count == 0)
                return ConvertBoardToString.ConvertToString(sudoku);
            return ConvertBoardToString.ConvertToString(dancingLinksUtils.ConvertDlxResultToSudoku(solution));
        }
    }
}