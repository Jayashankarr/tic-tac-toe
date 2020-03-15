using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Board board = null;

    private GridCell[] cellList = null;

    private GridCell[,] cellMatrix = null;

    private int numberOfTurns = 0;

    private GridCell lastClickedCell = null;

    public static GameController Controller = null;

    public int player_x = 0;

    public int player_o = 0;
    
    
    private void Start()
    {
        Controller = this;
        
        cellList = board.Cells;

        cellMatrix = new GridCell[3,3];

        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                cellMatrix[i, j] = cellList[i * 3 + j];

                cellMatrix[i , j].CellIndex = new Vector2 (i ,j);
            }
        }

        // for (int i = 0; i < cellList.Length ; i++)
        // {
        //     cellList[i].Index = i;
        // }
        
    }

    public void UpdateTurn(GridCell lastCell)
    {
        numberOfTurns++;

        lastClickedCell = lastCell;

        //if (numberOfTurns >= 5)
        {
            checkForWin(lastCell);
        } 
    }

    private void checkForWin (GridCell cell)
    {
        int xRowScore = 0, oRowScore = 0, xColScore = 0, oColScore = 0;
        int xMainDiagScore = 0, oMainDiagScore = 0, xSecDiagScore = 0, oSecDiagScore = 0;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                //rows
                if (cellMatrix[i, j].CurrentState == CellEnum.X) xRowScore++;
                else if (cellMatrix[i, j].CurrentState == CellEnum.O) oRowScore++;

                //columns
                if (cellMatrix[j, i].CurrentState == CellEnum.X) xColScore++;
                else if (cellMatrix[j, i].CurrentState == CellEnum.O) oColScore++;

                //main diag
                if (i == j)
                {
                    if (cellMatrix[i, j].CurrentState == CellEnum.X) xMainDiagScore++;
                    else if (cellMatrix[i, j].CurrentState == CellEnum.O) oMainDiagScore++;
                }

                //secondary diag
                if (i + j == 2)
                {
                    if (cellMatrix[i, j].CurrentState == CellEnum.X) xSecDiagScore++;
                    else if (cellMatrix[i, j].CurrentState == CellEnum.O) oSecDiagScore++;
                }

            }//end of the inner loop

            if (xRowScore == 3 || xColScore == 3 || xMainDiagScore == 3 || xSecDiagScore == 3)
            {
                Debug.Log ("X wins");

                break;
            }
            else if (oRowScore == 3 || oColScore == 3 || oMainDiagScore == 3 || oSecDiagScore == 3)
            {
                Debug.Log("O wins");

                break;
            }

            xRowScore = 0;
            oRowScore = 0;
            xColScore = 0;
            oColScore = 0;
        }
    }

    
}
