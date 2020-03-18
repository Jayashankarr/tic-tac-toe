using System.Collections;
using System.Collections.Generic;
using TicTakToe.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace TicTakToe.Ui
{
    public class LoginButton : MonoBehaviour
    {
        public Button fbButton;

        public void OnFbButtonClick ()
        {
            if (!GameManager.Instance.FacebookManagerGO.activeInHierarchy)
            {
                GameManager.Instance.FacebookManagerGO.SetActive (true);
                fbButton.interactable = false;
            }
            else
            {
                GameManager.Instance.FacebookManagerGO.GetComponent<FacebookManager>().CallFBLogin ();
            }
        }

        public void OnSkipButtonClicked ()
        {
            GameManager.Instance.FacebookManagerGO.SetActive (false);
            GameManager.Instance.ShowMenuScreen ();
        }
    }
}
