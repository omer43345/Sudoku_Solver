﻿using System;
using System.Xml.Schema;

namespace Sudoku_Solver
{
    public static class Program
    {
        static void Main(string[] args)
        {
            startProgram();
        }

        private static void startProgram()
        {
            Console.Write(MenuItems.MENU);
            string choice = Console.ReadLine();
            if (choice == "1")
                Console.WriteLine(MenuItems.CONSOLE);
            else if (choice == "2")
                Console.WriteLine(MenuItems.FILE);
            else if (choice == "3")
            {
                Console.WriteLine(MenuItems.EXIT);
                Environment.Exit(0);
            }
            string boardString="";
            int[,] board= null;
            try
            {
                boardString = SudokuReader.ReadSudoku(choice);
                board = SudokuBoardBuilder.BoardBuilder(boardString);
                SudokuValidator.Validate(board);
            }
            catch (Exception e)
            {
                Console.WriteLine("\n"+e.Message+"\n");
                startProgram();
            }
            DateTime start = DateTime.Now;
            Solver.Solve(board);
            Console.WriteLine("Solved in: " + (DateTime.Now - start).TotalMilliseconds + "ms");
            startProgram();
        }
    }
    
}