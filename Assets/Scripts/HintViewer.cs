using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintViewer : MonoBehaviour
{
    private GridElement[,] m_Grid;
    private List<PieceType> m_GridTypes;
    private List<Coordinates> m_CordDisplay;

    public void PopulateData(GridElement[,] grid, List<PieceType> types)
    {
        m_Grid = grid;
        m_GridTypes = types;
    }

    public void TraverseGrid()
    {
        DFSTraversal traverse = new DFSTraversal();
        m_CordDisplay = traverse.GenerateOptimalHint(m_Grid, m_GridTypes);
        DisplayHint();
    }

    private void DisplayHint()
    {
        for (int i = 0; i < m_CordDisplay.Count; i++)
        {
            m_Grid[m_CordDisplay[i].rowVal, m_CordDisplay[i].colVal].EnableHint();
        }
    }

}
