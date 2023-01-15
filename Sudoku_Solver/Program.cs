using System;
using System.Diagnostics;
using Sudoku_Solver.Exceptions;
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
            byte[,] board = GetBoard(); // Get the board

            // start solving the board
            Stopwatch start = new Stopwatch();
            start.Start();
            string solvedBoard = Solver.Solve(board);
            start.Stop();
            MenuHandler.PrintSudokuBoard(solvedBoard);
            if (solvedBoard.Contains("0"))
            {
                Console.WriteLine("No solution found in " + start.ElapsedMilliseconds + " ms");
            }
            else
            {
                Console.WriteLine("\nSolved in " + start.ElapsedMilliseconds + "ms\n");
            }

            SudokuValidator.Validate(SudokuBoardBuilder.BoardBuilder(solvedBoard));
            Console.WriteLine("\n");
            StartProgram();
        }
        /// <summary>
        /// Validating the board string from the user and build the board
        /// </summary>
        /// <returns> return the board to solve</returns>
        private static byte[,] GetBoard()
        {
            byte[,] board = null;
            string errorMessage = "";
            try
            {
                MenuHandler.StartMenu();
                SudokuReader sudokuReader = SudokuReader.GetInstance();
                string boardString = sudokuReader.GetInput();
                board = SudokuBoardBuilder.BoardBuilder(boardString);
                SudokuValidator.Validate(board);
            }
            catch (SystemException e)
            {
                errorMessage= e.Message;
            }
            catch (InvalidChoiceException e)
            {
                errorMessage = e.Message;
            }
            catch (InvalidSudokuBoardSizeException e)
            {
                errorMessage = e.Message;
            }
            catch (AllowedValuesException e)
            {
                errorMessage = e.Message;
            }
            catch (DuplicateValueInRowException e)
            {
                errorMessage = e.Message;
            }
            catch (DuplicateValueInColumnException e)
            {
                errorMessage = e.Message;
            }
            catch (DuplicateValueInBoxException e)
            {
                errorMessage = e.Message;
            }
            if (errorMessage != "")
            {
                Console.WriteLine("\n"+errorMessage+"\n");
                StartProgram();
            }
            return board;
        }
    }
}