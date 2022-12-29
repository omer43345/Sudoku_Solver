namespace Sudoku_Solver
{
    public static class MenuItems
    {
        public static readonly string MENU = "Welcome to Sudoku Solver by Omer Hadad\n" +
                                             "How do you want to write a sudoku, in the console you want to use a file?\n" +
                                             "1. Console\n" +
                                             "2. File\n" +
                                             "3. Exit\n" +
                                             "Your choice: ";
        public static readonly string CONSOLE = "Write the sudoku in the console. Use 0 for empty fields.";
        public static readonly string FILE = "Write the path to the file: ";
        public static readonly string FILE_ERROR = "The file does not exist.";
        public static readonly string EXIT = "Exiting the program...";
        public static readonly string INVALID_CHOICE = "You have to choose a number from the menu.";

    }
}