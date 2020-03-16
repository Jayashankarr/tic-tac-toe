using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FbButton : MonoBehaviour
{
    private Button button;
    public void OnFbButtonClick ()
    {
        if (!GameManager.Instance.FacebookManagerGO.activeInHierarchy)
        {
            GameManager.Instance.FacebookManagerGO.SetActive (true);

            button.interactable = false;
        }
    }
}
