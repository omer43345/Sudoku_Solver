using System;
using Sudoku_Solver.Exceptions;
using Sudoku_Solver.Output;

namespace Sudoku_Solver.Menu
{
    /*
     * This class handle the user input/output and the menu
     */
    public static class MenuHandler
    {
        private static int _choice;

        // Print the menu and handle the user input
        public static void StartMenu()
        {
            Console.Write(MenuItems.Menu);
            try
            {
                _choice = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                throw new InvalidChoiceException();
            }
            if (!(_choice < MenuItems.Output.Count && _choice > 0))
            {
                throw new InvalidChoiceException();
            }
            Console.WriteLine(MenuItems.Output[_choice]);
            MenuItems.InputMethods[_choice]();
        }

        // this method prints the board to console or file
        public static void PrintSudokuBoard(string board)
        {
            BoardWriter.GetInstance().SetBoard(board);
            MenuItems.OutputMethods[_choice]();
        }
    }
}