using System.Collections;
using System.Collections.Generic;
using TicTakToe.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace TicTakToe.Ui
{
    public class FbButton : MonoBehaviour
    {
        public Button Button;

        public void OnFbButtonClick ()
        {
            if (!GameManager.Instance.FacebookManagerGO.activeInHierarchy)
            {
                GameManager.Instance.FacebookManagerGO.SetActive (true);
                Button.interactable = false;
            }
            else
            {
                GameManager.Instance.FacebookManagerGO.GetComponent<FacebookManager>().CallFBLogin ();
            }
        }
    }
}
