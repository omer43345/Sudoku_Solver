namespace Sudoku_Solver.Output
{
    // Interface for output classes
    public interface IOutput
    {
        void WriteToConsole(); // Write to console
        void WriteToFile(); // Write to file
    }
}