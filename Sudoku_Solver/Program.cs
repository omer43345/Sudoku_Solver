using System;
using Sudoku_Solver.Input;

namespace Sudoku_Solver
{
    public static class Program
    {
        static void Main(string[] args)
        {
            StartProgram();
        }

        public static void StartProgram()
        {
            MenuHandler.StartMenu();
            SudokuReader sudokuReader = SudokuReader.GetInstance();
            byte[,] board = null;

            try
            {
                string boardString = sudokuReader.GetInput();
                board = SudokuBoardBuilder.BoardBuilder(boardString);
                SudokuValidator.Validate(board);
            }
            catch (Exception e)
            {
                Console.WriteLine("\n" + e.Message + "\n");
                StartProgram();
            }

            DateTime start = DateTime.Now;
            byte[,] solvedBoard = Solver.Solve(board);
            TimeSpan time = DateTime.Now - start;
            if (solvedBoard == null)
            {
                MenuHandler.PrintSudokuBoard(board);
                Console.WriteLine("No solution found in " + time.TotalMilliseconds + " ms");
            }
            else
            {
                MenuHandler.PrintSudokuBoard(solvedBoard);
                Console.WriteLine("\nSolved in " + time.TotalMilliseconds + "ms\n");
                SudokuValidator.Validate(solvedBoard);
            }

            StartProgram();
        }
    }
}