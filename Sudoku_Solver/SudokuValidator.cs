using Sudoku_Solver.Exceptions;

namespace Sudoku_Solver
{
    /*
    * This class is used to validate a Sudoku board.
      It is used to check if a Sudoku board is valid before solving it.
    */

    public static class SudokuValidator 
    {
        private static byte[,] _sudokuBoard; // the Sudoku board to validate
        private static int _boardSize; // size of the board
        private static SudokuBitWiseFunctions _bitWiseFunctions; // bit-wise functions used to validate the board
        public static void Validate(byte[,] sudokuBoard)
        {
            _sudokuBoard = sudokuBoard;
            _boardSize = sudokuBoard.GetLength(0);
            _bitWiseFunctions = new SudokuBitWiseFunctions(sudokuBoard);
            ValidateDuplicateValues();
        }


        /// <summary>
        /// this method is used to validate if there are duplicate values in the Sudoku board
        /// </summary>
        private static void ValidateDuplicateValues()
        {
            for (int i = 0; i <_boardSize; i++)
            {
                for (int j = 0; j < _boardSize; j++)
                {
                    byte value = _sudokuBoard[i, j];
                    if (value != 0)
                    {
                        int candidate = _bitWiseFunctions.GetCandidates(i, j);
                        if (!_bitWiseFunctions.CanPlace(candidate, value))
                            CheckWhereDuplicateValueIsFound(i, j, value);
                        _bitWiseFunctions.SetCandidate(i, j, value);
                    }
                }
            }
        }
        /// <summary>
        /// This method is used to check where the duplicate value is found.
        /// This method is called when a duplicate value is found.
        /// </summary>
        /// <param name="row">row of the duplicate value</param>
        /// <param name="col">col of the duplicate value</param>
        /// <param name="value">the duplicate value</param>
        /// <exception cref="DuplicateValueInRowException"></exception>
        /// <exception cref="DuplicateValueInColumnException"></exception>
        /// <exception cref="DuplicateValueInGridException"></exception>
        private static void CheckWhereDuplicateValueIsFound(int row, int col, byte value)
        {
            int rowCandidate = _bitWiseFunctions.GetRowCandidates(row);
            int colCandidate = _bitWiseFunctions.GetColumnCandidates(col);
            int boxCandidate = _bitWiseFunctions.GetBoxCandidates(row,col);
            if (!_bitWiseFunctions.CanPlace(rowCandidate,value))
            {
                throw new DuplicateValueInRowException(row+1, (char)(value+'0'));
            }
            if (!_bitWiseFunctions.CanPlace(colCandidate, value))
            {
                throw new DuplicateValueInColumnException(col+1, (char)(value + '0'));
            }
            if (!_bitWiseFunctions.CanPlace(boxCandidate, value))
            {
                throw new DuplicateValueInGridException(_bitWiseFunctions.GetBox(row,col)+1, (char)(value + '0'));
            }
        }

    }
}