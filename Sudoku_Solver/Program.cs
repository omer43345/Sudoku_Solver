using System;
using Sudoku_Solver.Input;
using Sudoku_Solver.Menu;
using Sudoku_Solver.SudokuBoardConvertors;
using Sudoku_Solver.Validator;

namespace Sudoku_Solver
{
    public static class Program
    {
        static void Main(string[] args)
        {
            StartProgram();
        }

        // The method that starts the program 
        public static void StartProgram()
        {
            byte[,] board = null;

            try
            {
                MenuHandler.StartMenu();
                SudokuReader sudokuReader = SudokuReader.GetInstance();
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
            string solvedBoard = Solver.Solve(board);
            TimeSpan time = DateTime.Now - start;
            MenuHandler.PrintSudokuBoard(solvedBoard);
            if (solvedBoard.Contains("0"))
            {
                Console.WriteLine("No solution found in " + time.TotalMilliseconds + " ms");
            }
            else
            {
                Console.WriteLine("\nSolved in " + time.TotalMilliseconds + "ms\n");
            }

            SudokuValidator.Validate(SudokuBoardBuilder.BoardBuilder(solvedBoard));
            Console.WriteLine("\n");
            StartProgram();
        }
    }
}