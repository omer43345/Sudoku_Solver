namespace Sudoku_Solver.Input
{
    // Interface for input classes
    public interface IInput
    {
        void ReadFromFile(); // Read from file
        void ReadFromConsole();// Read from console

        string GetInput();// Return the input
    }
}