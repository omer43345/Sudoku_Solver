using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Solver
{
    public class SudokuSolverHelper
    {
        private int _size;
        private int _gridSize;
        private char[,] _puzzle;
        private char[,] _values;
        private bool[,] _rows;
        private bool[,] _columns;
        private bool[,] _subgrids;

        public SudokuSolverHelper(char[,] puzzle)
        {
            _puzzle = puzzle;
            _size = puzzle.GetLength(0);
            _gridSize = (int)Math.Sqrt(_size);
            _values = new char[_size, _size];
            _rows = new bool[_size, _size];
            _columns = new bool[_size, _size];
            _subgrids = new bool[_size, _size];

            // Initialize the rows, columns, and subgrids
            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    _rows[i, j] = false;
                    _columns[i, j] = false;
                    _subgrids[i, j] = false;
                }
            }

            // Set the initial values for the cells
            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    SetValue(i, j, puzzle[i, j]);
                }
            }
        }
        private void SetValue(int row, int column, char value)
        {
            if (value == '0')
            {
                return;
            }
            _rows[row,value-'0'-1] = true;
            _columns[column,value-'0'-1] = true;
            _subgrids[GetSubgridIndex(row, column),value - '0'-1] = true;
            _values[row, column] = value;
        }
        private int GetSubgridIndex(int row, int column)
        {
            return (row /_gridSize) * _gridSize + column / _gridSize;
        }

        // Return a BitArray of the possible values for a cell at the given row and column
        public bool[] GetPossibleValues(int row, int column)
        {
            bool[] possibleValues = new bool[_size];
            for (int i = 0; i < _size; i++)
            {
                possibleValues[i] = !_rows[row, i] && !_columns[column, i] && !_subgrids[GetSubgridIndex(row, column), i];
            }
            return possibleValues;
        }
    }
}
