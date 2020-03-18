using System.Collections;
using System.Collections.Generic;
using TicTakToe.Ui;
using UnityEngine;
using UnityEngine.UI;

namespace TicTakToe.Player
{
    public class GamePlayer : MonoBehaviour
    {
        public string Name;

        public int CheckedValue = 0;

        public Sprite Picture;

        public WinType WinType = WinType.LOSE;

        public string ScoreKey = "";

        public void onPlayerClick (GridCell cell)
        {
            //GameManager.Instance.GameController.UpdateTurn (cell);
        }

        public void SetPlayerData (string name, Sprite image)
        {
            Name = name;

            if(image != null)
            {
                Picture = image;
            }

            if (name != null)
            {
                ScoreKey = name + "_score";
            }
        }
    }
}