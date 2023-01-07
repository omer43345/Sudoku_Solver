using System;
using System.IO;

namespace Sudoku_Solver.Output
{
    public class BoardWriter : IOutput
    {
        public BoardWriter()
        {
        }

        public void WriteToConsole(Object board)
        {
            SudokuBoardPrinter.PrintBoard((byte[,])board);
        }

        public void WriteToFile(Object board, string path)
        {
            FileInfo file = new FileInfo(path);
            StreamWriter sw;
            if (file.Exists)
                sw = file.AppendText();
            else
                sw = new StreamWriter(path);
            Console.SetOut(sw);
            sw.WriteLine("\n\n");
            //set the cursor to the end of the file so that the output is appended
            SudokuBoardPrinter.PrintBoard((byte[,])board);
            sw.Close();
            // reset console output to console window
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
        }
    }
}