using System.Collections.Generic;

public class DFSTraversal
{
    // No of rows
    // and columns
    private int m_Rows = 0, m_Cols = 0;

    //The size of a block
    private int m_Size = 0;

    //Largest/Optimal block size
    private int m_LargestBlock = 0;

    //List to hold the coordinates of the largest block
    private List<Coordinates> m_FinalBlock;

    //List to hold each block coordinates
    private List<Coordinates> m_TempBlock;


    //Neighbouring elements of a cell in a grid horizontally and vertically 
    int[] rowNbr
            = new int[] { 0, 1, 0, -1 };
    int[] colNbr
        = new int[] { -1, 0, 1, 0 };

    //Value to find 
    private PieceType m_SearchVal;

    public List<Coordinates> GenerateOptimalHint(GridElement[,] grid, List<PieceType> PiecesToFind)
    {
        m_Rows = grid.GetLength(0);
        m_Cols = grid.GetLength(1);

        m_TempBlock = new List<Coordinates>();
        m_FinalBlock = new List<Coordinates>();

        for (int i = 0; i < PiecesToFind.Count; i++)
        {
            m_SearchVal = PiecesToFind[i];
            FindBlocks(grid);
        }
        return m_FinalBlock;
    }

   //Function to check if a given cell can be traversed via DFS
    bool isValid(GridElement[,] M, int row, int col,
                       bool[,] visited)
    {
        return (row >= 0) && (row < m_Rows) && (col >= 0)
            && (col < m_Cols)
            && (M[row, col].Type == m_SearchVal && !visited[row, col]);
    }


    void FindBlocks(GridElement[,] M)
    {
        bool[,] visited = new bool[m_Rows, m_Cols];

        for (int i = 0; i < m_Rows; ++i)
        {
            for (int j = 0; j < m_Cols; ++j)
            {
                if (!visited[i, j])
                {
                    int blockSize = GroupSize(M, i, j, visited);
                    if (blockSize > 3)
                    {
                        if (blockSize > m_LargestBlock)
                        {
                            m_LargestBlock = blockSize;
                            m_FinalBlock.Clear();
                            foreach (var cord in m_TempBlock)
                            {
                                m_FinalBlock.Add(cord);
                            }
                        }
                    }

                    m_TempBlock.Clear();
                }
            }
        }
    }

    int GroupSize(GridElement[,] M, int row, int col, bool[,] visited)
    {
        m_Size = 0;

        if (isValid(M, row, col, visited))
        {
            visited[row, col] = true;
            m_Size = 1;

            Coordinates cord = new Coordinates();
            cord.rowVal = row;
            cord.colVal = col;
            m_TempBlock.Add(cord);

            for (int k = 0; k < 4; ++k)
            {
                m_Size += GroupSize(M, row + rowNbr[k], col + colNbr[k], visited);
            }
        }

        return m_Size;
    }
}

public struct Coordinates
{
    public int rowVal;
    public int colVal;
}
