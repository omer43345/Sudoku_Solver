using System;
using System.Collections.Generic;
using System.Data.Common;

namespace Sudoku_Solver.DancingLinksAlgorithm
{
    public class DLX
    {
        private readonly DancingLinksColumnNode _root; // root of the chained list

        private readonly Stack<DancingLinksNode>
            _solution; // stack that contains the nodes that represent the solution for the cover problem

        private readonly int _chainedListColCount; // number of columns in the chained list
        private readonly int[] _coverArray; // cover array that contains all the nodes in the chained list
        private const int ConstraintCount = 4; // number of constraints, 4 for sudoku (row, column, box, value)

        public DLX(int[] coverArray, int sudokuBoardSize)
        {
            _solution = new Stack<DancingLinksNode>();
            _coverArray = coverArray;
            _chainedListColCount = sudokuBoardSize * sudokuBoardSize * ConstraintCount;
            _root = BuildDlxList();
        }

        /// <summary>
        /// Initialize the header nodes for each column in the chained list
        /// </summary>
        /// <param name="rootNode">the root node in the chained list</param>
        /// <param name="columnNodes">the list that we will add the columns headers</param>
        private void CreateColumnNode(DancingLinksColumnNode rootNode, List<DancingLinksColumnNode> columnNodes)
        {
            for (int column = 0; column < _chainedListColCount; column++)
            {
                DancingLinksColumnNode columnNode = new DancingLinksColumnNode(column + "");
                columnNodes.Add(columnNode);
                rootNode = (DancingLinksColumnNode)rootNode.LinkRight(columnNode);
            }
        }

        /// <summary>
        /// Create the chained list from the cover array, iterate on every row in the cover array and create a node for each column in the row
        /// </summary>
        /// <param name="columnNodes">list of the header nodes in the chained list</param>
        private void CreateRowNodes(List<DancingLinksColumnNode> columnNodes)
        {
            int numOfRows = _coverArray.Length / ConstraintCount; // number of rows in the chained list
            for (int row = 0; row < numOfRows; row++)
            {
                DancingLinksNode previousNode = null; //for checking if the node is the first node in the row
                // iterate on every column in the row, in every row there are 4 columns (one for each constraint)
                for (int constraint = 0; constraint < ConstraintCount; constraint++)
                {
                    int column =
                        _coverArray[row * ConstraintCount + constraint]; // get the column index from the cover array
                    DancingLinksNode node = new DancingLinksNode(columnNodes[column]);
                    // if the node is the first node in the row, than the previous node is the first node in the row
                    if (previousNode == null)
                    {
                        previousNode = node;
                    }

                    columnNodes[column].Up.LinkDown(node);
                    previousNode = previousNode.LinkRight(node);
                    // increase the size of the column
                    columnNodes[column].Size++;
                }
            }
        }


        /// <summary>
        /// Build the chained list from the cover array
        /// </summary>
        /// <returns></returns>
        private DancingLinksColumnNode BuildDlxList()
        {
            DancingLinksColumnNode rootNode = new DancingLinksColumnNode("root");
            List<DancingLinksColumnNode> columnNodes = new List<DancingLinksColumnNode>();
            // Create the header nodes for each column in the chained list
            CreateColumnNode(rootNode, columnNodes);
            // Create the row nodes and add them to the column nodes
            CreateRowNodes(columnNodes);
            rootNode.Size = _chainedListColCount;
            return rootNode;
        }

        /// <summary>
        /// The algorithm for solving the exact cover problem using the dancing links algorithm
        /// and adding every node that represent the solution to the solution stack
        /// </summary>
        /// <returns>return true if we found a solution and false if not</returns>
        private bool Search()
        {
            // if the root is the only node in the chained list, than we found a solution
            if (_root.Right == _root)
                return true;
            // select the column with the smallest size and cover it
            DancingLinksColumnNode bestColumn = SelectBestColumn();
            bestColumn.Cover();
            // iterate on every node in the column 
            for (DancingLinksNode row = bestColumn.Down; row != bestColumn; row = row.Down)
            {
                _solution.Push(row);
                // iterate on every node in the row and cover the column that the node is in
                for (DancingLinksNode column = row.Right; column != row; column = column.Right)
                {
                    column.Column.Cover();
                }

                // if we found a solution, return true
                if (Search())
                {
                    return true;
                }

                // If we not found a solution, then backtrack and try the next row
                row = _solution.Pop();
                // uncover the column to try the next row
                for (DancingLinksNode column = row.Left; column != row; column = column.Left)
                {
                    column.Column.Uncover();
                }
            }

            // if we not found a solution, uncover the best column and return false
            bestColumn.Uncover();
            return false;
        }

        /// <summary>
        /// Find the column with the smallest size and return it
        /// </summary>
        /// <returns>return the column with the least nodes in it</returns>
        private DancingLinksColumnNode SelectBestColumn()
        {
            bool doneSearching = false; // flag that turn when the size of the column is 1
            int minSize = int.MaxValue;
            DancingLinksColumnNode bestColumn = null; // the column with the smallest size that we will return
            DancingLinksColumnNode column = (DancingLinksColumnNode)_root.Right;
            // iterate on every column in the chained list
            while (column != _root && !doneSearching)
            {
                // Update the best column if we found a column with a smaller size(smaller number of nodes in it)
                if (column.Size < minSize)
                {
                    minSize = column.Size;
                    bestColumn = column;
                }

                // if the size of the column is 1 than this is the minimum column and we finished to search
                if (minSize == 1)
                {
                    doneSearching = true;
                }
                column = (DancingLinksColumnNode)column.Right;
            }

            return bestColumn;
        }

        public Stack<DancingLinksNode> GetSolution()
        {
            // if we not found a solution clear solution stack
            if (!Search())
                _solution.Clear();
            return _solution;
        }
    }
}