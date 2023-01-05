﻿using System;
using Sudoku_Solver.DancingLinksAlgorithm;

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
            
            byte[,] board= null;
            
            try
            {
                var boardString="";
                boardString = SudokuReader.ReadSudoku(choice);
                board = SudokuBoardBuilder.BoardBuilder(boardString);
                SudokuValidator.Validate(board);
            }
            catch (Exception e)
            {
                Console.WriteLine("\n"+e.Message+"\n");
                startProgram();
            }
            /*DateTime start = DateTime.Now;
            DancingLinksUtils dancingLinksUtils = new DancingLinksUtils(board);
            byte[,] coverMatrix = dancingLinksUtils.BuildCoverMatrix();
            DLX dlx = new DLX(coverMatrix);
            DateTime end = DateTime.Now;
            TimeSpan time = end - start;
            SudokuBoardPrinter.PrintBoard(board);
            Console.WriteLine("\nSolved in " + time.TotalMilliseconds + "ms\n");*/
            SudokuValidator.Validate(board);
            /*Console.WriteLine("\nCould not solve in " + time.TotalMilliseconds + "ms\n");*/
            startProgram();
        }
    }
    
}