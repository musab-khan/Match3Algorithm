using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraverseGrid : MonoBehaviour
{
    
    //Declare Bounds
    private int m_RowCount = 0;
    private int m_ColCount = 0;


    //Initialize direction vectors
    private int[] m_DRow = { 0, 1, 0, -1 };
    private int[] m_DCol = { -1, 0, 1, 0 };

    private void Start()
    {
        m_RowCount = 3;
        m_ColCount = 3;
        string[,] grid = { { "x", "o", "x" },
                           { "o", "x", "o" },
                           { "o", "x", "x" } };

        bool[,] vis = new bool[m_RowCount, m_ColCount];
        for (int i = 0; i < m_RowCount; i++)
        {
            for (int j = 0; j < m_ColCount; j++)
            {
                vis[i, j] = false;
            }
        }

        // Function call
        DFS(0, 0, grid, vis);
    }


    public void Traverse()
    {

    }

    bool isValid(bool[,] vis, int row, int col)
    {

        // If cell is out of bounds
        if (row < 0 || col < 0 ||
            row >= m_RowCount || col >= m_ColCount)
            return false;

        // If the cell is already visited
        if (vis[row, col])
            return false;

        // Otherwise, it can be visited
        return true;
    }

    void DFS(int row, int col,
                string[,] grid, bool[,] vis)
    {

        // Initialize a stack of pairs and
        // push the starting cell into it
        Stack st = new Stack();
        st.Push(new Tuple<int, int>(row, col));

        // Iterate until the
        // stack is not empty
        while (st.Count > 0)
        {
            // Pop the top pair
            Tuple<int, int> curr = (Tuple<int, int>)st.Peek();
            st.Pop();

            row = curr.Item1;
            col = curr.Item2;

            // Check if the current popped
            // cell is a valid cell or not
            if (!isValid(vis, row, col))
                continue;

            // Mark the current
            // cell as visited
            vis[row, col] = true;

            // Print the element at
            // the current top cell
            Debug.Log(grid[row, col] + " ");

            // Push all the adjacent cells
            for (int i = 0; i < 4; i++)
            {
                int adjx = row + m_DRow[i];
                int adjy = col + m_DCol[i];
                st.Push(new Tuple<int, int>(adjx, adjy));
            }
        }
    }
}
