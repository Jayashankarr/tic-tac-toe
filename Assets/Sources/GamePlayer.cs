using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayer : MonoBehaviour
{
    public string name;

    public int CheckedValue = 0;

    public Image picture;

    public void onPlayerClick (GridCell cell)
    {
        //GameManager.Instance.GameController.UpdateTurn (cell);
    }
}
