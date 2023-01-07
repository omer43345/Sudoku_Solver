using System;
using System.Collections.Generic;
using Sudoku_Solver.Input;

namespace Sudoku_Solver
{
    public static class MenuItems
    {
        public const string Menu = "Welcome to Sudoku Solver by Omer Hadad\n" +
                                   "How do you want to write a sudoku, in the console you want to use a file?\n" +
                                   "1. Console\n" + "2. File\n" + "3. Exit\n" + "Your choice: ";

        public const string Console = "Write the sudoku in the console. Use 0 for empty fields.";

        public const string File = "C:\\Users\\omerh\\Documents\\hermlin\\sudoku_example.txt\n" +
                                   "C:\\Users\\omerh\\Documents\\hermlin\\Sudoku_Solver\\Sudokus.txt\n" +
                                   "Write the path to the file: ";

        public const string SolutionPath = "C:\\Users\\omerh\\Documents\\hermlin\\Sudoku_Solver\\Suodku_Solutions.txt";
        public const string Exit = "Exiting the program...";

        public static readonly Dictionary<int, string> Output = new Dictionary<int, string>
        {
            { 1, Console },
            { 2, File },
            { 3, Exit }
        };

        public static readonly Dictionary<int, Action> InputMethods = new Dictionary<int, Action>
        {
            { 1, SudokuReader.GetInstance().ReadFromConsole },
            { 2, SudokuReader.GetInstance().ReadFromFile }
        };

        public static readonly string FILE_ERROR = "The file does not exist.";
        public static readonly string INVALID_CHOICE = "You have to choose a number from the menu.";
    }
}