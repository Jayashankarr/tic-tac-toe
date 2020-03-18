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

        Player1_score.text = PlayerPrefs.GetInt (GameManager.Instance.player_x.ScoreKey, 0).ToString ();
        Player2_score.text = PlayerPrefs.GetInt (GameManager.Instance.player_o.ScoreKey, 0).ToString ();
    }

    public void OnCloseButtonClicked ()
    {
        gameObject.SetActive (false);
    }
}
