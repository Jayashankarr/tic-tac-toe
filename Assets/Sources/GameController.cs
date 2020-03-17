using System.Collections;
using System.Collections.Generic;
using TicTakToe.Manager;
using TicTakToe.Player;
using TicTakToe.Ui;
using TicTakToe.Utils;
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
    private GameObject playerXInfoGO;

    [SerializeField]
    private GameObject playerOInfoGO;

    [SerializeField]
    private WinLine line = null;

    [SerializeField]
    private PlayerAi ai;

    private GridCell[] cellList = null;

    private GridCell[,] cellMatrix = null;

    private int numberOfTurns = 0;

    private GridCell lastClickedCell = null;

    private GameState currentGameState = GameState.RESUME;

    private PlayerSymbol currentPlayerSymbol = PlayerSymbol.X;

    public PlayerSymbol CurrentTurn
    {
        get {return currentPlayerSymbol;}
    }
    
    
    private void OnEnable()
    {   
        cellList = board.Cells;

        cellMatrix = new GridCell[3,3];

        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                cellMatrix[i, j] = cellList[i * 3 + j];

                cellMatrix[i , j].Cell2DIndex = new Vector2 (i ,j);
            }
        }
    }

    public void UpdateTurn(GridCell lastCell)
    {
        numberOfTurns++;

        lastClickedCell = lastCell;

        int weight = 1 << lastCell.Index;

        if (lastCell.CellAssignedSymbol == CellSymbol.X)
        {
            GameManager.Instance.player_x.CheckedValue |= weight;
        }
        else
        {
            GameManager.Instance.player_o.CheckedValue |= weight;
        }

        if (numberOfTurns >= 5)
        {
            checkForWin(lastCell);
        }

        switchPlayer ();
    }

    private void switchPlayer ()
    {
        if (currentPlayerSymbol == PlayerSymbol.X)
        {
            currentPlayerSymbol = PlayerSymbol.O;

            if (GameManager.Instance.GameType == GameType.SINGLE_PLAYER)
            {
                AIturn ();

                EnableCellInputs (false);
            }
        }
        else
        {
            EnableCellInputs (true);

            currentPlayerSymbol = PlayerSymbol.X;
        }
    }

    private void checkForWin (GridCell cell)
    {
        LineType type;
        
        if (lastClickedCell.CellAssignedSymbol == CellSymbol.X)
        {
            

            if ( ( (GameManager.Instance.player_x.CheckedValue & (int)WinCombo.R_1) == (int)WinCombo.R_1) ||
               ( (GameManager.Instance.player_x.CheckedValue & (int)WinCombo.R_2) == (int)WinCombo.R_2) ||
               ( (GameManager.Instance.player_x.CheckedValue & (int)WinCombo.R_3) == (int)WinCombo.R_3) )
            {
                type = LineType.HORIZONTAL;

                line.gameObject.GetComponent<Image>().color = xColor;

                GenerateWinLine (type);
            }

            if ( ( (GameManager.Instance.player_x.CheckedValue & (int)WinCombo.C_1) == (int)WinCombo.C_1) ||
               ( (GameManager.Instance.player_x.CheckedValue & (int)WinCombo.C_2) == (int)WinCombo.C_2) ||
               ( (GameManager.Instance.player_x.CheckedValue & (int)WinCombo.C_3) == (int)WinCombo.C_3) )
            {
                type = LineType.VERTICAL;

                line.gameObject.GetComponent<Image>().color = xColor;

                GenerateWinLine (type);
            }

            if ( (GameManager.Instance.player_x.CheckedValue & (int)WinCombo.D_1) == (int)WinCombo.D_1 )
            {
                type = LineType.DIAGONAL_R;

                line.gameObject.GetComponent<Image>().color = xColor;

                GenerateWinLine (type);
            }

            if ( (GameManager.Instance.player_x.CheckedValue & (int)WinCombo.D_2) == (int)WinCombo.D_2 )
            {
                type = LineType.DIAGONAL_L;

                line.gameObject.GetComponent<Image>().color = xColor;

                GenerateWinLine (type);
            }
        }
        else
        {
            int temp = GameManager.Instance.player_o.CheckedValue & (int)WinCombo.D_2;

            if ( ( (GameManager.Instance.player_o.CheckedValue & (int)WinCombo.R_1) == (int)WinCombo.R_1) ||
               ( (GameManager.Instance.player_o.CheckedValue & (int)WinCombo.R_2) == (int)WinCombo.R_2) ||
               ( (GameManager.Instance.player_o.CheckedValue & (int)WinCombo.R_3) == (int)WinCombo.R_3) )
            {
                type = LineType.HORIZONTAL;

                line.gameObject.GetComponent<Image>().color = oColor;

                GenerateWinLine (type);
            }

            if ( ( (GameManager.Instance.player_o.CheckedValue & (int)WinCombo.C_1) == (int)WinCombo.C_1) ||
               ( (GameManager.Instance.player_o.CheckedValue & (int)WinCombo.C_2) == (int)WinCombo.C_2) ||
               ( (GameManager.Instance.player_o.CheckedValue & (int)WinCombo.C_3) == (int)WinCombo.C_3) )
            {
                type = LineType.VERTICAL;

                line.gameObject.GetComponent<Image>().color = oColor;

                GenerateWinLine (type);
            }

            if ( (GameManager.Instance.player_o.CheckedValue & (int)WinCombo.D_1) == (int)WinCombo.D_1 )
            {
                type = LineType.DIAGONAL_R;

                line.gameObject.GetComponent<Image>().color = oColor;

                GenerateWinLine (type);
            }

            if ( (GameManager.Instance.player_o.CheckedValue & (int)WinCombo.D_2) == (int)WinCombo.D_2 )
            {
                type = LineType.DIAGONAL_L;

                line.gameObject.GetComponent<Image>().color = oColor;

                GenerateWinLine (type);
            }

            if (numberOfTurns == 9)
            {
                gameOver ();
            }
        }
    }

    private void gameOver ()
    {
        numberOfTurns = 0;

        lastClickedCell = null;
    }

    public void EnableCellInputs (bool status)
    {
        for (int i = 0; i < cellList.Length; i++)
        {
            cellList[i].Button.interactable = status;
        }
    }

    private void GenerateWinLine(LineType type)
    {
        Debug.Log ("Generate line");
        GridCell lineOrigin;

        Vector2 lastCellIndex = lastClickedCell.Cell2DIndex;

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
            case PlayerSymbol.X:
            line.GetComponent<Image>().color = xColor;;
            break;

            case PlayerSymbol.O:
            line.GetComponent<Image>().color = oColor;
            break;

        }

        line.transform.position = lineOrigin.transform.position;

        gameOver ();
    }

    public void AIturn()
    {
        ai.ActivateForTurn();
    }

    public GridCell GetLastClickedCell()
    {
        return lastClickedCell;
    }

    public GridCell[] GetCellList()
    {
        return cellList;
    }

    public GridCell[,] GetCellMatrix()
    {
        return cellMatrix;
    }

    public void ResetGameBoard ()
    {
        gameOver ();

        for (int i = 0; i < cellList.Length; i++)
        {
            cellList[i].ResetCell ();
        }

        ResetWinLine ();

        currentPlayerSymbol = PlayerSymbol.X;

        ai.ResetAI ();
    }

    public void ResetWinLine ()
    {
        line.ResetWinLine ();

        line.gameObject.SetActive (false);
    }
}
