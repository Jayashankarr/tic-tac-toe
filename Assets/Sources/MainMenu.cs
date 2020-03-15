using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
