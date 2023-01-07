using System;
using System.Collections.Generic;

namespace Sudoku_Solver.DancingLinksAlgorithm
{
    public class DLX
    {
        private readonly DancingLinksColumnNode _root;
        private readonly Stack<DancingLinksNode> _solution;
        private readonly int _coverMatrixColCount;
        private readonly int _coverMatrixRowCount;

        public DLX(byte[,] coverMatrix)
        {
            _coverMatrixColCount = coverMatrix.GetLength(1);
            _coverMatrixRowCount = coverMatrix.GetLength(0);
            _solution = new Stack<DancingLinksNode>();
            _root = BuildDLXList(coverMatrix);
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

        // Create the row nodes and add them to the column nodes
        private void CreateRowNodes(List<DancingLinksColumnNode> columnNodes, byte[,] coverMatrix)
        {
            for (int row = 0; row < _coverMatrixRowCount; row++)
            {
                DancingLinksNode previousNode = null;
                for (int column = 0; column < _coverMatrixColCount; column++)
                {
                    if (coverMatrix[row, column] == 1)
                    {
                        // If in the cover matrix, there is a 1 in this position then add a node to the column
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
        }

        // Build the DLX list from the cover matrix
        private DancingLinksColumnNode BuildDLXList(byte[,] coverMatrix)
        {
            DancingLinksColumnNode rootNode = new DancingLinksColumnNode("root");
            List<DancingLinksColumnNode> columnNodes = new List<DancingLinksColumnNode>();

            CreateColumnNode(rootNode, columnNodes);
            // Create the row nodes and add them to the column nodes
            CreateRowNodes(columnNodes, coverMatrix);
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