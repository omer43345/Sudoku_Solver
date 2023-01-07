﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Solver
{
    public class SudokuBitWiseFunctions
    {
        private readonly int[] _rowsCandidates; // rows candidates
        private readonly int[] _colsCandidates; // columns candidates
        private readonly int[] _boxesCandidates; // boxes candidates
        private readonly int[] _bitMask; // bit mask of the values 
        private byte[,] _sudokuBoard; // sudoku board to solve 
        private readonly int _size; // size of the sudoku board
        private readonly int _boxSize; // size of the grid

        public SudokuBitWiseFunctions(byte[,] sudokuBoard)
        {
            _sudokuBoard = sudokuBoard;
            _size = sudokuBoard.GetLength(0);
            _boxSize = (int) Math.Sqrt(_size);
            _rowsCandidates = new int[_size];
            _colsCandidates = new int[_size];
            _boxesCandidates = new int[_size];
            _bitMask = new int[_size];
            InitializeCandidates();
            CreateBitMaskArray();
        }
        /// <summary>
        ///  Create the bit mask array 
        /// </summary>
        private void CreateBitMaskArray()
        {
            for (var value = 1; value <= _size; value++)
            {
                _bitMask[value - 1] = 1 << (value - 1);
            }
        }
        /// <summary>
        /// Initialize the candidates for each row, column and box in 0
        /// </summary>
        private void InitializeCandidates()
        {
            for (var i = 0; i < _size; i++)
            {
                _bitMask[i] = _rowsCandidates[i] = _colsCandidates[i] = _boxesCandidates[i] = 0;
            }
        }
        public int GetBox(int row, int col)
        {
            return (row / _boxSize) * _boxSize + (col / _boxSize);
        }
        

        /// <summary>
        /// Check if a value is a candidate for a given candidates int
        /// </summary>
        /// <param name="candidates">candidates to check if a value is allowed</param>
        /// <param name="value"></param>
        /// <returns>return true is we can place the value</returns>
        public bool CanPlace(int candidates, int value)
        {
            return (candidates & _bitMask[value - 1]) == 0;
        }
        /// <summary>
        /// Set the value in the candidates arrays for the row, column and box
        /// </summary>
        /// <param name="row">row of the cell</param>
        /// <param name="col">col of the cell</param>
        /// <param name="value">value to update</param>
        public void SetCandidate(int row, int col, int value)
        {
            _rowsCandidates[row] |= _bitMask[value - 1];
            _colsCandidates[col] |= _bitMask[value - 1];
            _boxesCandidates[GetBox(row, col)] |= _bitMask[value - 1];
        }
        /// <summary>
        /// get the candidates for a cell
        /// </summary>
        /// <param name="row">row of the cell</param>
        /// <param name="col">col of the cell</param>
        /// <returns>returns candidates number of the cell</returns>
        public int GetCandidates(int row, int col)
        {
            var box = GetBox(row, col);
            return _rowsCandidates[row] | _colsCandidates[col] | _boxesCandidates[box];
        }
        public int GetRowCandidates(int row)
        {
            return _rowsCandidates[row];
        }
        public int GetColumnCandidates(int column)
        {
            return _colsCandidates[column];
        }
        public int GetBoxCandidates(int row,int col)
        {
            return _boxesCandidates[GetBox(row,col)];
        }


    }
}