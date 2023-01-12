using System;
using System.IO;
using Sudoku_Solver.Menu;

namespace Sudoku_Solver.Output
{
    public class BoardWriter : IOutput
    {
        private string _board; // the board to be printed
        private static BoardWriter _instance = null; // the instance of the class   

        // Get the instance of the class
        public static BoardWriter GetInstance()
        {
            if (_instance == null)
            {
                _instance = new BoardWriter();
            }

            return _instance;
        }

        // private constructor to prevent instantiation
        private BoardWriter()
        {
        }

        public void SetBoard(string board)
        {
            _board = board;
        }

        // Write the board to the console
        public void WriteToConsole()
        {
            SudokuBoardPrinter.PrintBoard(_board);
            Console.WriteLine("\n\n");
            SudokuBoardPrinter.PrintBoardString(_board);
            Console.WriteLine("\n");
        }

        // Write the board to the file
        public void WriteToFile()
        {
            FileInfo file = new FileInfo(MenuItems.SolutionPath);
            StreamWriter sw;
            if (file.Exists)
                sw = file.AppendText();
            else
                sw = new StreamWriter(MenuItems.SolutionPath);
            Console.SetOut(sw);
            sw.WriteLine("\n\n");
            //set the cursor to the end of the file so that the output is appended
            SudokuBoardPrinter.PrintBoardString(_board);
            sw.Close();
            // reset console output to console window
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
            WriteToConsole();
        }
    }
}