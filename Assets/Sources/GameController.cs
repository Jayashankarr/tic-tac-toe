using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Board board = null;

    private GridCell[] cellList = null;

    private GridCell[,] cellMatrix = null;
    
    void Start()
    {
        cellList = board.Cells;
        
        cellMatrix = new GridCell[3,3];

        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                cellMatrix[i, j] = cellList[i * 3 + j];
            }
        }
        
    }
}
