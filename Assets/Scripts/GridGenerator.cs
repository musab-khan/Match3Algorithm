using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GridGenerator : MonoBehaviour
{
    [Serializable]
    private struct PieceInfo
    {
        public PieceType type;
        public Sprite pieceSprite;
    }

    [SerializeField] private HintViewer viewer;
    [SerializeField] private TextAsset m_CsvFile;
    [SerializeField] private PieceInfo[] m_PieceInfo;
    [SerializeField] private GameObject m_CellPrefab;
    [SerializeField] private Transform m_GridHolder;

    private int m_SizeX;
    private int m_SizeY;

    private string[,] m_layoutArray;
    private GridElement[,] m_Grid;
    private List<PieceType> m_TypesInGrid;

    private void Start()
    {
        m_layoutArray = ReadLayout.ReadFile(m_CsvFile.text);

        m_SizeX = m_layoutArray.GetLength(0);
        m_SizeY = m_layoutArray.GetLength(1);

        GenerateGrid(m_layoutArray);
    }

    private void GenerateGrid(string[,] layout)
    {
        m_Grid = new GridElement[m_SizeX , m_SizeY];
        m_GridHolder.GetComponent<GridLayoutGroup>().constraintCount = m_SizeY;


        bool[] isPresent = new bool[m_PieceInfo.Length];
        for (int i = 0; i < isPresent.Length; i++)
        {
            isPresent[i] = false;
        }

        for (int i = 0; i < m_SizeX; i++)
        {
            for (int j = 0; j < m_SizeY; j++)
            {
                GameObject obj = Instantiate(m_CellPrefab, m_GridHolder);
                GridElement cell = obj.GetComponent<GridElement>();

                switch (layout[i,j])
                {
                    case "0":
                        cell.DisplayElement(PieceType.None);
                        m_Grid[i, j] = cell;
                        break;

                    case "1":
                        int randValue = UnityEngine.Random.Range(0, m_PieceInfo.Length);

                        isPresent[randValue] = true;

                        cell.DisplayElement(m_PieceInfo[randValue].type, m_PieceInfo[randValue].pieceSprite);
                        m_Grid[i, j] = cell;
                        break;
                }
            }
        }

        m_TypesInGrid = new List<PieceType>();

        for (int i = 0; i < isPresent.Length; i++)
        {
            if(isPresent[i])
            {
                m_TypesInGrid.Add(m_PieceInfo[i].type);
            }
        }

        viewer.PopulateData(m_Grid, m_TypesInGrid);
    }

    public void Reload()
    {
        SceneManager.LoadScene(0);
    }
}
