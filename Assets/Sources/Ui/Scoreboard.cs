using System.Collections;
using System.Collections.Generic;
using TicTakToe.Manager;
using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour
{
    [SerializeField]
    private Text player1_name;

    [SerializeField]
    private Text Player1_score;

    [SerializeField]
    private Text player2_name;

    [SerializeField]
    private Text Player2_score;

    private void OnEnable ()
    {
        player1_name.text = GameManager.Instance.player_x.Name;
        player2_name.text = GameManager.Instance.player_o.Name;

        int score1 = PlayerPrefs.GetInt (GameManager.Instance.player_x.ScoreKey);
        int Score2 = PlayerPrefs.GetInt (GameManager.Instance.player_o.ScoreKey);

        Debug.Log ("player1 :" + score1 + " " + "Player2 :" + Score2);

        Player1_score.text = PlayerPrefs.GetInt (GameManager.Instance.player_x.ScoreKey).ToString ();
        Player2_score.text = PlayerPrefs.GetInt (GameManager.Instance.player_o.ScoreKey).ToString ();
    }

    public void OnCloseButtonClicked ()
    {
        gameObject.SetActive (false);
    }
}
