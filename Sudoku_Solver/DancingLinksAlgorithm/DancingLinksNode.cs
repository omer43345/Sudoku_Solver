namespace Sudoku_Solver.DancingLinksAlgorithm
{
    /*
     * This class represents a node in the dancing links sparse matrix.
     * It is used to represent the constraints of the Sudoku puzzle.
     * It contains methods to link and unlink nodes in the matrix.
     */
    public class DancingLinksNode
    {
        public DancingLinksNode Left, Right, Up, Down; // the four pointers in the exact cover problem
        public DancingLinksColumnNode Column; // the column header this node is in

        protected DancingLinksNode()
        {
            Left = Right = Up = Down = this;
        }

        public DancingLinksNode(DancingLinksColumnNode column)
        {
            Left = Right = Up = Down = this;
            Column = column;
        }

        // links this node to the bottom of the given node
        public void LinkDown(DancingLinksNode node)
        {
            node.Down = Down;
            node.Down.Up = node;
            node.Up = this;
            Down = node;
        }

        // links this node to the right of the given node
        public DancingLinksNode LinkRight(DancingLinksNode node)
        {
            node.Right = Right;
            node.Right.Left = node;
            node.Left = this;
            Right = node;
            return node;
        }

        // unlink this node from the matrix by removing the links to its left and right
        protected void UnlinkLeftRight()
        {
            Left.Right = Right;
            Right.Left = Left;
        }

        // relink the connections to the left and right of this node in the matrix
        protected void RelinkLeftRight()
        {
            Left.Right = this;
            Right.Left = this;
        }

        // unlink this node from the matrix by removing the links to its up and down
        public void UnlinkUpDown()
        {
            Up.Down = Down;
            Down.Up = Up;
        }

        // relink the connections to the up and down of this node in the matrix
        public void RelinkUpDown()
        {
            Up.Down = this;
            Down.Up = this;
        }
    }
}