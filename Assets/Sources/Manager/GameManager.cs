using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TicTakToe.Utils;
using TicTakToe.Player;
using TicTakToe.Ui;

namespace TicTakToe.Manager
{
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
        private Text playerxName;

        [SerializeField]
        private Text playeroName;

        [SerializeField]
        private Image playerXImage;

        [SerializeField]
        private Image playerOImage;

        [SerializeField]
        private GameObject AiGO;

        [SerializeField]
        private GameObject homeBtn;

        [SerializeField]
        private GameObject resetBtn;

        [SerializeField]
        private GameObject soundBtn;

        [SerializeField]
        private GameObject scoreBtn;

        [SerializeField]
        private GameObject scoreBoard;

        [SerializeField]
        public GameObject PlayerXInfo;
        
        [SerializeField]
        public GameObject PlayerOInfo;

        [SerializeField]
        private Text victoryText;

        [SerializeField]
        private GameObject skipButton;

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

        public void ShowMenuScreen ()
        {
            startScreen.SetActive (false);
            skipButton.SetActive (false);
            mainMenu.SetActive (true);
            homeBtn.SetActive (false);
            resetBtn.SetActive (false);
            gameBoard.SetActive (false);
            GameController.gameObject.SetActive (false);
            scoreBtn.SetActive (false);
            PlayerOInfo.SetActive (false);
            PlayerXInfo.SetActive (false);
            victoryText.text = "";
        }

        public void SinglePlayerSelected ()
        {
            gameType = GameType.SINGLE_PLAYER;
            AiGO.SetActive (true);
            player_x = new User ();
            player_o = AiGO.GetComponent<PlayerAi>();

            if (facebookManagerGo.activeInHierarchy)
            {
                player_x.SetPlayerData (FacebookManagerGO.GetComponent<FacebookManager>().UserName, 
                                    FacebookManagerGO.GetComponent<FacebookManager>().ProfilePic);
            }
            else
            {
                player_x.SetPlayerData ("Player_1", null);
            }
            
            playerxName.text = player_x.Name;
            playerXImage.sprite = player_x.Picture;

            player_o.SetPlayerData ("AI", 
                                    null);
            playeroName.text = player_o.Name;
            startGame ();
        }

        public void MultiPlayerSelected ()
        {
            gameType = GameType.MULTI_PLAYER;
            player_x = new User ();
            player_o = new User ();

            if (facebookManagerGo.activeInHierarchy)
            {
                player_x.SetPlayerData (FacebookManagerGO.GetComponent<FacebookManager>().UserName, 
                                    FacebookManagerGO.GetComponent<FacebookManager>().ProfilePic);
            }
            else
            {
                player_x.SetPlayerData ("Player_1", null);
            }
            playerxName.text = player_x.Name;
            playerXImage.sprite = player_x.Picture;

            player_o.SetPlayerData ("Player_2", 
                                    null);
            playeroName.text = player_o.Name;

            startGame ();
        }

        private void startGame ()
        {
            victoryText.gameObject.SetActive (false);
            GameManager.Instance.PlayerXInfo.SetActive (true);

            mainMenu.SetActive (false);
            gameBoard.SetActive (true);
            homeBtn.SetActive (true);
            resetBtn.SetActive (true);
            GameController.gameObject.SetActive (true);
            scoreBtn.SetActive (true);
        }

        public void OnCellClicked (GridCell cell)
        {
            if (GameController.CurrentTurn == PlayerSymbol.X)
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

        private void ResetPlayerData ()
        {
            player_o.CheckedValue = 0;
            player_x.CheckedValue = 0;
        }

        public void ResetGame ()
        {
            victoryText.text = "";
            GameController.ResetGameBoard ();
            ResetPlayerData ();
            GameManager.Instance.PlayerXInfo.SetActive (true);
            GameManager.Instance.PlayerOInfo.SetActive (false);
            victoryText.gameObject.SetActive (false);
        }

        public void OnHomeButtonClicked ()
        {
            ResetGame ();
            ShowMenuScreen ();
        }

        public void SaveDataForKey (string key, int value)
        {
            value += PlayerPrefs.GetInt (key,0);
            Debug.Log (key + " : " + value);
            PlayerPrefs.SetInt (key , value);
            PlayerPrefs.Save ();
        }

        public void OnScoreBoradClicked ()
        {
            scoreBoard.SetActive (true);
        }

        public void ShowVictory ()
        {
            victoryText.text = "VICTORY";
            if (player_x.WinType == WinType.WIN)
            {
                PlayerXInfo.SetActive (true);
            }
            else if (player_o.WinType == WinType.WIN)
            {
                PlayerOInfo.SetActive (true);
            }
            else if (player_x.WinType == WinType.DRAW && player_o.WinType == WinType.DRAW)
            {
                PlayerXInfo.SetActive (true);
                PlayerOInfo.SetActive (true);

                victoryText.text = "DRAW";
            }
            victoryText.gameObject.SetActive (true);
        }

        public void OnCloseButtonClicked ()
        {
            Application.Quit ();
        }
    }
}