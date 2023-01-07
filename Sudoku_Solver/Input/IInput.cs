namespace Sudoku_Solver.Input
{
    public interface IInput
    {
        void ReadFromFile();
        void ReadFromConsole();

        string GetInput();
    }
}