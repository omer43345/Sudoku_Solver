using System;
using System.Collections.Generic;

namespace Sudoku_Solver.DancingLinksAlgorithm
{
    public class DLX
    {
        private readonly DancingLinksColumnNode _root;
        private readonly Stack<DancingLinksNode> _solution;
        private readonly int _coverMatrixColCount;
        private readonly int[] _coverArray;
        private readonly int _constraintCount = 4;

        public DLX( int[] coverArray,int sudokuBoardSize)
        {
            _solution = new Stack<DancingLinksNode>();
            _coverArray = coverArray;
            _coverMatrixColCount = sudokuBoardSize * sudokuBoardSize * _constraintCount;
            _root = BuildDlxList();
        }

        // Create the column nodes and add them to the root node
        private void CreateColumnNode(DancingLinksColumnNode rootNode, List<DancingLinksColumnNode> columnNodes)
        {
            for (int column = 0; column < _coverMatrixColCount; column++)
            {
                DancingLinksColumnNode columnNode = new DancingLinksColumnNode(column + "");
                columnNodes.Add(columnNode);
                rootNode = (DancingLinksColumnNode)rootNode.LinkRight(columnNode);
            }
        }
        // Create the nodes in the rows according to the columns index in the cover array
        private void CreateRowNodes(List<DancingLinksColumnNode> columnNodes)
        {
            int numOfRows = _coverArray.Length/ _constraintCount;
            for (int row = 0; row < numOfRows; row++)
            {
                DancingLinksNode previousNode = null;
                for (int constraint = 0; constraint < 4; constraint++)
                {
                    int column = _coverArray[row * 4 + constraint];
                    DancingLinksNode node = new DancingLinksNode(columnNodes[column]);
                    if (previousNode == null)
                    {
                        previousNode = node;
                    }
                    columnNodes[column].Up.LinkDown(node);
                    previousNode = previousNode.LinkRight(node);
                    columnNodes[column].Size++;
                    
                }
            }
        }


        // Build the DLX list from the cover matrix
        private DancingLinksColumnNode BuildDlxList()
        {
            DancingLinksColumnNode rootNode = new DancingLinksColumnNode("root");
            List<DancingLinksColumnNode> columnNodes = new List<DancingLinksColumnNode>();

            CreateColumnNode(rootNode, columnNodes);
            // Create the row nodes and add them to the column nodes
            CreateRowNodes(columnNodes);
            rootNode.Size = _coverMatrixColCount;
            return rootNode;
        }

        private bool Search()
        {
            if (_root.Right == _root)
                return true;
            DancingLinksColumnNode bestColumn = SelectBestColumn();
            bestColumn.Cover();
            for (DancingLinksNode row = bestColumn.Down; row != bestColumn; row = row.Down)
            {
                _solution.Push(row);
                for (DancingLinksNode column = row.Right; column != row; column = column.Right)
                {
                    column.Column.Cover();
                }

                if (Search())
                {
                    return true;
                }

                // If we not found a solution, then backtrack and try the next row
                row = _solution.Pop();
                bestColumn = row.Column;
                for (DancingLinksNode column = row.Left; column != row; column = column.Left)
                {
                    column.Column.Uncover();
                }
            }

            bestColumn.Uncover();
            return false;
        }

        // Select the column node with the smallest size
        private DancingLinksColumnNode SelectBestColumn()
        {
            int minSize = int.MaxValue;
            DancingLinksColumnNode bestColumn = null;
            for (DancingLinksColumnNode column = (DancingLinksColumnNode)_root.Right;
                 column != _root;
                 column = (DancingLinksColumnNode)column.Right)
            {
                if (column.Size < minSize)
                {
                    minSize = column.Size;
                    bestColumn = column;
                }
            }

            return bestColumn;
        }

        public Stack<DancingLinksNode> GetSolution()
        {
            if (!Search())
                _solution.Clear();
            return _solution;
        }
    }
}