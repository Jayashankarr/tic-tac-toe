using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    
    [SerializeField]
    private GameObject startScreen; 

    [SerializeField]
    private GameObject mainMenu;

    [SerializeField]
    private GameObject gameBoard;

    [SerializeField]
    private GameObject gameControllerGo;

    [SerializeField]
    private GameObject facebookManagerGo;

    [SerializeField]
    private GameObject playerInfo;

    [SerializeField]
    private Text playerxName;

    [SerializeField]
    private Text playeroName;

    [SerializeField]
    private Image playerXImage;

    [SerializeField]
    private Image playerOImage;

    [SerializeField]
    private GameObject AiGO;

    public Button FbBtn;   

    public GameController GameController;

    public static GameManager Instance;

    public GamePlayer player_x;

    public GamePlayer player_o;

    private PlayerAi player_Ai;

    private GameType gameType;

    public GameType GameType
    {
        get {return gameType;}
    }

    public GameObject FacebookManagerGO
    {
        get {return facebookManagerGo;}
    }

    private GridCell lastSelectedCell = null;


    private void Start()
    {
        Instance = this;

        GameController = gameControllerGo.GetComponent<GameController>();
    }

    public void ShowStartScreen ()
    {

    }

    public void ShowMenuScreen ()
    {
        startScreen.SetActive (false);

        mainMenu.SetActive (true);
    }

    public void ShowResultScreen ()
    {

    }

    public void SinglePlayerSelected ()
    {
        gameType = GameType.SINGLE_PLAYER;

        AiGO.SetActive (true);

        player_x = new Player ();

        player_o = AiGO.GetComponent<PlayerAi>();

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

        playerInfo.SetActive (true);
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

    public void EnableFacebookManagerGameobject ()
    {
        if (!facebookManagerGo.activeInHierarchy)
        {
            facebookManagerGo.SetActive(true);
        }
    }
}
