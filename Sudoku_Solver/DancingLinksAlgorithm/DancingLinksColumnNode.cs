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
        public readonly  String ColumnName; // name of the column

        public DancingLinksColumnNode(String columnName):base() {
            Size = 0;
            ColumnName = columnName;
            Column = this;
        }
        
        // cover this column and all rows that contain a 1 in this column, and remove all columns that contain a 1 in these rows
        public void Cover() {
            UnlinkLeftRight();
            for( DancingLinksNode i = Down; i != this; i = i.Down ) {
                for( DancingLinksNode j = i.Right; j != i; j = j.Right ) {
                    j.UnlinkUpDown();
                    j.Column.Size--;
                }
            }
        }
        // uncover this column and all rows that contain a 1 in this column, and restore all columns that contain a 1 in these rows
        public void Uncover() {
            for( DancingLinksNode i = Up; i != this; i = i.Up ) {
                for( DancingLinksNode j = i.Left; j != i; j = j.Left ) {
                    j.Column.Size++;
                    j.RelinkUpDown();
                }
            }
            RelinkLeftRight();
        }
        
        
    }
}