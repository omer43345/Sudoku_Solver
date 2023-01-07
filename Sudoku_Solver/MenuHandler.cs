using System;
using Sudoku_Solver.Output;

namespace Sudoku_Solver
{
    public static class MenuHandler
    {
        private static int _choice = 0;

        public static void StartMenu()
        {
            Console.Write(MenuItems.Menu);
            try
            {
                _choice = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine(MenuItems.Output[_choice]);
            }
            catch (Exception)
            {
                Console.WriteLine(MenuItems.INVALID_CHOICE);
                Program.StartProgram();
            }

            if (MenuItems.Output[_choice] == MenuItems.Exit)
                Environment.Exit(0);
            MenuItems.InputMethods[_choice]();
        }

        // this method prints the board to the console and the file
        public static void PrintSudokuBoard(byte[,] board)
        {
            BoardWriter boardWriter = new BoardWriter();
            boardWriter.WriteToConsole(board);
            boardWriter.WriteToFile(board, MenuItems.SolutionPath);
        }
    }
}