using System;
using System.IO;

namespace Sudoku_Solver.Input
{
    /*
     * This class is used to read the input from the file or the console.
     * it is a singleton class because we only need one instance of it.
     */
    public class SudokuReader : IInput
    {
        private static SudokuReader _instance = null;
        private string _boardString;
        public SudokuReader()
        {
            _boardString = "";
        }
        public static SudokuReader GetInstance()
        {
            if (_instance == null)
            {
                _instance = new SudokuReader();
            }
            return _instance;
        }
        // Read the sudoku string from the console and update the _boardString
        public void ReadFromConsole()
        {
            Console.SetIn(new StreamReader(Console.OpenStandardInput(4096)));
            _boardString= Console.ReadLine();
        }
        // Read sudoku string  from file and update the _boardString
        public void ReadFromFile()
        {
            string path = Console.ReadLine();
            try
            {
                _boardString= File.ReadAllText(path);
            }
            catch (SystemException e)
            {
                throw new SystemException(MenuItems.FILE_ERROR); 
            }
        }
        // Return the _boardString
        public string GetInput()
        {
            return _boardString;
        }
    }
}