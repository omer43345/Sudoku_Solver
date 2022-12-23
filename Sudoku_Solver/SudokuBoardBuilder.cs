using Sudoku_Solver.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sudoku_Solver
{
    // This class creates a Sudoku board from a string of characters representing the board.
    public class SudokuBoardBuilder
    {
        private string _sudokuBoardString;
        private char[,] _sudokuBoard;
        public SudokuBoardBuilder(string sudokuBoardString)
        {
            _sudokuBoardString = sudokuBoardString;
            validateSize();
            _sudokuBoard = new char[(int)Math.Sqrt(_sudokuBoardString.Length), (int)Math.Sqrt(_sudokuBoardString.Length)];
            boardBuilder();
        }
/*        static void Main(string[] args)
        {
            // checking this class functions
            string sudokuString = "800000070006010053040600000000080400003000700020005038000000800004050061900002000";
            SudokuBoardBuilder builder = new SudokuBoardBuilder(sudokuString);
            char[,] board = builder.GetSudokuBoard();
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    Console.Write(" "+board[i, j]+" ");
                }
                Console.WriteLine();
            }
        }*/
        // This method is used to validate the size of the string if it is suitable for a Sudoku board.
        private void validateSize()
        {
            double rowAndColumnCount = Math.Sqrt(_sudokuBoardString.Length);
            double gridSideLength = Math.Sqrt(rowAndColumnCount);
            if (gridSideLength % 1 != 0|| rowAndColumnCount%1!=0)
            {
                throw new InvalidSudokuBoardSize(_sudokuBoardString.Length);
            }

        }
        // This method is used to build the Sudoku board from the string.
        private void boardBuilder()
        {
            int rowAndColumnCount = (int)Math.Sqrt(_sudokuBoardString.Length);
            int cellIndex = 0;
            foreach (char cell in _sudokuBoardString)
            {
                _sudokuBoard[cellIndex/rowAndColumnCount, cellIndex%rowAndColumnCount] = cell;
                cellIndex++;
            }
        }
        public char[,] GetSudokuBoard()
        {
            return _sudokuBoard;
        }
    }
}
