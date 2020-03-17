using System.Collections;
using System.Collections.Generic;
using TicTakToe.Manager;
using UnityEngine;

namespace TicTakToe.Ui
{
    public class MainMenu : MonoBehaviour
    {
        public void OnSinglePlayerButtonClckd ()
        {
            GameManager.Instance.SinglePlayerSelected ();
        }

        public void OnMultiPlayerButtonClckd ()
        {
            GameManager.Instance.MultiPlayerSelected ();

        }
    }
}
