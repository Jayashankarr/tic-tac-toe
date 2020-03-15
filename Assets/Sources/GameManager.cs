using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenu;

    [SerializeField]
    private GameObject gameBoard;

    [SerializeField]
    private GameObject gameControllerGameObj;

    public GameController GameController;

    public static GameManager Instance;

    private GamePlayer player_x;

    private GamePlayer player_o;

    private PlayerAi player_Ai;

    private GameType gameType;

    public GameType GameType
    {
        get {return gameType;}
    }

    private GridCell lastSelectedCell = null;


    void Start()
    {
        Instance = this;

        GameController = gameControllerGameObj.GetComponent<GameController>();
    }

    public void SinglePlayerSelected ()
    {
        gameType = GameType.SINGLE_PLAYER;

        player_x = new Player ();

        player_o = new PlayerAi ();

        startGame ();
    }

    public void MultiPlayerSelected ()
    {
        gameType = GameType.MULTI_PLAYER;

        player_x = new Player ();

        player_o = new Player ();

        startGame ();
    }

    private void startGame ()
    {
        mainMenu.SetActive (false);

        gameBoard.SetActive (true);
    }

    public void OnCellClicked (GridCell cell)
    {
        if (GameController.CurrentTurn == PlayerEnum.X)
        {
            player_x.onPlayerClick (cell);
        }
        else
        {
            player_o.onPlayerClick (cell);
        }
        
        lastSelectedCell = cell;
    }
}
