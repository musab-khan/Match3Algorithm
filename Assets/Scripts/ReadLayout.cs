using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ReadLayout : MonoBehaviour
{
    [SerializeField] private TextAsset m_layoutFile;

    // Start is called before the first frame update
    void Start()
    {
        ReadFile(m_layoutFile.text);
    }

    public static string[,] ReadFile(string content)
    {
        string output = "";

        //Reading rows in the file
        string[] rows = content.Split('\n');

        //Getting row count for the grid
        int rowCount = rows.Length;

        //Getting column count for the grid
        string[] columns = rows[0].Split(',');
        int columnCount = columns.Length;

        //Declaration of grid
        string[,] grid = new string[rowCount, columnCount];

        //Assigning the grid
        for (int i = 0; i < rowCount; i++)
        {
            string[] cells = rows[i].Split(',');
            for (int j = 0; j < columnCount; j++)
            {
                grid[i, j] = cells[j];
                output += grid[i, j] + '\t';
            }

            output += '\n';
        }

        Debug.Log(output);

        Debug.Log($"Size : {grid.GetLength(0)}  ,  {grid.GetLength(1)}");

        return grid;
    }
}
