using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Board board = null;

    [SerializeField]
    private Color xColor = Color.clear;

    [SerializeField]
    private Color oColor = Color.clear;

    [SerializeField]
    private WinLine line = null;

    private GridCell[] cellList = null;

    private GridCell[,] cellMatrix = null;

    private int numberOfTurns = 0;

    private GridCell lastClickedCell = null;

    private GameState currentGameState = GameState.RESUME;

    private PlayerEnum currentPlayerSymbol = PlayerEnum.X;

    //private WinLine line = null;

    public static GameController Controller = null;

    public int player_x = 0;

    public int player_o = 0;

    public PlayerEnum CurrentTurn
    {
        get {return currentPlayerSymbol;}
    }
    
    
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
    }

    public void UpdateTurn(GridCell lastCell)
    {
        numberOfTurns++;

        lastClickedCell = lastCell;

        if (numberOfTurns >= 5)
        {
            checkForWin(lastCell);
        }

        switchPlayer ();
    }

    private void switchPlayer ()
    {
        if (currentPlayerSymbol == PlayerEnum.X)
        {
            currentPlayerSymbol = PlayerEnum.O;
        }
        else
        {
            currentPlayerSymbol = PlayerEnum.X;
        }

    }

    private void checkForWin (GridCell cell)
    {
        int xRowScore = 0, oRowScore = 0, xColScore = 0, oColScore = 0;

        int xRightDiagScore = 0, oRightDiagScore = 0, xLeftDiagScore = 0, oLeftDiagScore = 0;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                //rows
                if (cellMatrix[i, j].CurrentState == CellEnum.X)
                {
                    xRowScore++;
                }
                else if (cellMatrix[i, j].CurrentState == CellEnum.O)
                {
                    oRowScore++;
                }

                //columns
                if (cellMatrix[j, i].CurrentState == CellEnum.X)
                {
                    xColScore++;
                }
                else if (cellMatrix[j, i].CurrentState == CellEnum.O)
                {
                    oColScore++;
                }

                //main diag
                if (i == j)
                {
                    if (cellMatrix[i, j].CurrentState == CellEnum.X)
                    {
                        xRightDiagScore++;
                    }
                    else if (cellMatrix[i, j].CurrentState == CellEnum.O)
                    {
                        oRightDiagScore++;
                    }
                }

                //secondary diag
                if (i + j == 2)
                {
                    if (cellMatrix[i, j].CurrentState == CellEnum.X)
                    {
                        xLeftDiagScore++;
                    }
                    else if (cellMatrix[i, j].CurrentState == CellEnum.O)
                    {
                        oLeftDiagScore++;
                    }
                }

            }//end of the inner loop

            if (xRowScore == 3 || xColScore == 3 || xRightDiagScore == 3 || xLeftDiagScore == 3)
            {
                Debug.Log ("X wins");

                currentGameState = GameState.GAME_OVER;

                break;
            }
            else if (oRowScore == 3 || oColScore == 3 || oRightDiagScore == 3 || oLeftDiagScore == 3)
            {
                Debug.Log("O wins");

                currentGameState = GameState.GAME_OVER;

                break;
            }

            xRowScore = 0;
            oRowScore = 0;
            xColScore = 0;
            oColScore = 0;
        }

            if (currentGameState == GameState.GAME_OVER) //If game is over - create a win line.
            {
                if (currentPlayerSymbol == PlayerEnum.X)
                {
                    line.GetComponent<Image>().color = xColor;

                    CheckWinLineType(xRowScore, xColScore, xRightDiagScore, xLeftDiagScore);
                }
                else
                {
                    line.GetComponent<Image>().color = oColor;

                    CheckWinLineType(oRowScore, oColScore, oRightDiagScore, oLeftDiagScore);
                }
            }
            else if (numberOfTurns == 9) //If game isn't over and field is full - tie.
            {
                Debug.Log("TIE");
            } 
    }

    private void onGameComplete ()
    {

    }

    private void CheckWinLineType(int row, int col, int d_m, int d_s)
    {
        LineType type;

        if (row == 3)
        {
            type = LineType.HORIZONTAL; 
        }
        else if (col == 3)
        {
            type = LineType.VERTICAL;
        }
        else if (d_m == 3)
        {
            type = LineType.DIAGONAL_R;
        }
        else
        {
            type = LineType.DIAGONAL_L;
        }

        GenerateWinLine (type);
    }

    private void GenerateWinLine(LineType type)
    {
        Debug.Log ("Generate line");
        GridCell lineOrigin;

        Vector2 lastCellIndex = lastClickedCell.CellIndex;

        line.gameObject.SetActive (true);

        switch (type)
        {
            case LineType.HORIZONTAL:
                lineOrigin = cellMatrix[(int)lastCellIndex.x, 0];
                line.transform.eulerAngles = Vector3.zero;
                line.SetStraight(true);
                break;

            case LineType.VERTICAL:
                lineOrigin = cellMatrix[0, (int)lastCellIndex.y];
                line.transform.eulerAngles = new Vector3(0f, 0f, -90f);
                line.SetStraight(true);
                break;

            case LineType.DIAGONAL_R:
                lineOrigin = cellMatrix[0, 0];
                line.transform.eulerAngles = new Vector3(0f, 0f, -45f);
                line.SetStraight(false);
                break;

            case LineType.DIAGONAL_L:
                lineOrigin = cellMatrix[2, 0];
                line.transform.eulerAngles = new Vector3(0f, 0f, 45f);
                line.SetStraight(false);
                break;

            default:
                lineOrigin = cellList[0];
                break;
        }

        switch (currentPlayerSymbol)
        {
            case PlayerEnum.X:
            line.GetComponent<Image>().color = xColor;;
            break;

            case PlayerEnum.O:
            line.GetComponent<Image>().color = oColor;
            break;

        }

        line.transform.position = lineOrigin.transform.position;
    }

    public GridCell GetLastClickedCell()
    {
        return lastClickedCell;
    }

    public GridCell[] GetCellList()
    {
        return cellList;
    }
}
