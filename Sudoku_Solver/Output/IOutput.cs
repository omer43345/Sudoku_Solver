using System;

namespace Sudoku_Solver.Output
{
    public interface IOutput
    {
        void WriteToConsole(Object obj);
        void WriteToFile(Object obj, string path);
    }
}