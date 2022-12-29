using System;
using System.Runtime.InteropServices;

namespace Sudoku_Solver
{
    public static class SudokuReader
    {
        public static string ReadSudoku(string choice)
        {
            if (choice=="1")
                return Console.ReadLine();
            if (choice == "2")
            {
                string path = Console.ReadLine();
                try
                {
                    return System.IO.File.ReadAllText(path);
                }
                catch (SystemException e)
                {
                    throw new SystemException(MenuItems.FILE_ERROR); 
                }
            }
            throw new Exception(MenuItems.INVALID_CHOICE);
        }
    }
}