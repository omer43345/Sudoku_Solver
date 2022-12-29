using System.Collections.Generic;

namespace Sudoku_Solver
{
    public static class PreSolver
    {
        private static int[,] _sudoku;
        private static int _size;
        

        public static bool OptimizeBoard(int[,] sudoku)
        {
            _sudoku = sudoku;
            _size = sudoku.GetLength(0);
            return HiddenSingles();

        }
        private static bool HiddenSingles()
        {
            bool changed = false;
            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    if (_sudoku[i, j] == 0)
                    {
                        List<int> possibleValues = new List<int>();
                        for (int k = 1; k <= _size; k++)
                        {
                            if (IsPossible(i, j, k))
                            {
                                possibleValues.Add(k);
                            }
                        }
                        if (possibleValues.Count == 1)
                        {
                            _sudoku[i, j] = possibleValues[0];
                            changed = true;
                        }
                    }
                }
            }
            return changed;
        }
        private static bool IsPossible(int row, int col, int value)
        {
            for (int i = 0; i < _size; i++)
            {
                if (_sudoku[row, i] == value)
                {
                    return false;
                }
            }
            for (int i = 0; i < _size; i++)
            {
                if (_sudoku[i, col] == value)
                {
                    return false;
                }
            }
            int sqrt = (int)System.Math.Sqrt(_size);
            int boxRowStart = row - row % sqrt;
            int boxColStart = col - col % sqrt;
            for (int r = boxRowStart; r < boxRowStart + sqrt; r++)
            {
                for (int c = boxColStart; c < boxColStart + sqrt; c++)
                {
                    if (_sudoku[r, c] == value)
                    {
                        return false;
                    }
                }
            }
            return true;
        }




    }
}