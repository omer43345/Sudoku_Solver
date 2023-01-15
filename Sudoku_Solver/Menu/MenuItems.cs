using System;
using System.Collections.Generic;
using System.IO;
using Sudoku_Solver.Input;
using Sudoku_Solver.Output;

namespace Sudoku_Solver.Menu
{
    public static class MenuItems
    {
        public const string Menu = "Welcome to Sudoku Solver by Omer Hadad\n" +
                                   "How do you want to write a sudoku, in the console you want to use a file?\n" +
                                   "1. Console\n" + "2. File\n" + "3. Exit\n" + "Your choice: "; // The menu

        private const string Console = "Write the sudoku in the console. Use 0 for empty fields."; // console message

        private const string File = "Write the path to the file: "; // file message

        private const string Exit = "Exiting the program..."; // the exit message


        private static string currentPath = AppDomain.CurrentDomain.BaseDirectory;
        private static string newFilePath = Path.Combine(Path.GetDirectoryName(currentPath), "..","..","Solutions", "Sudoku_Solutions.txt");
        public static string SolutionPath = newFilePath; // the path to the solution file

        // This dictionary contains the output messages for each choice
        public static readonly Dictionary<int, string> Output = new Dictionary<int, string>
        {
            { 1, Console },
            { 2, File },
            { 3, Exit }
        };

        // This dictionary contains the methods that handle the user input
        public static readonly Dictionary<int, Action> InputMethods = new Dictionary<int, Action>
        {
            { 1, SudokuReader.GetInstance().ReadFromConsole },
            { 2, SudokuReader.GetInstance().ReadFromFile },
            { 3, () => Environment.Exit(0) }
        };

        // This dictionary contains the methods that handle the output
        public static readonly Dictionary<int, Action> OutputMethods = new Dictionary<int, Action>
        {
            { 1, BoardWriter.GetInstance().WriteToConsole },
            { 2, BoardWriter.GetInstance().WriteToFile }
        };
    }
}