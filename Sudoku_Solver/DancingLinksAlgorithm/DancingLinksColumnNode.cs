using System;

namespace Sudoku_Solver.DancingLinksAlgorithm
{
    /*
     * This class represents a column node in the Dancing Links algorithm.
     * This class is used to represent the columns of the matrix and to cover and uncover them from the matrix.
     * It also contains the column header node and the number of nodes in the column.
     */
    public class DancingLinksColumnNode : DancingLinksNode
    {
        public int Size; // number of nodes in this column
        public readonly String ColumnName; // name of the column that this node represents

        public DancingLinksColumnNode(String columnName) : base()
        {
            Size = 0;
            ColumnName = columnName;
            Column = this;
        }

        /// <summary>
        /// Cover this column from the matrix by removing the links to him from all the nodes that connect to him.
        /// </summary>
        public void Cover()
        {
            UnlinkLeftRight(); // unlink the connections to the left and right to this column
            // iterate through all the nodes in this column
            for (DancingLinksNode row = Down; row != this; row = row.Down)
            {
                // iterate through all the nodes in the row of this node
                for (DancingLinksNode column = row.Right; column != row; column = column.Right)
                {
                    // unlink the connections to the up and down to this node and decrease the size of the column
                    column.UnlinkUpDown();
                    column.Column.Size--;
                }
            }
        }

        /// <summary>
        /// Uncover this column from the matrix by relinking the links to him from all the nodes that connect to him.
        /// </summary>
        public void Uncover()
        {
            // iterate through all the nodes in this column
            for (DancingLinksNode row = Up; row != this; row = row.Up)
            {
                // iterate through all the nodes in the row of this node
                for (DancingLinksNode column = row.Left; column != row; column = column.Left)
                {
                    // relink the connections to the up and down to this node and increase the size of the column
                    column.Column.Size++;
                    column.RelinkUpDown();
                }
            }

            // relink the connections to the left and right to this column
            RelinkLeftRight();
        }
    }
}