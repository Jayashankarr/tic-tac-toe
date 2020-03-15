using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayer : MonoBehaviour
{
    public void onPlayerClick (GridCell cell)
    {
        GameManager.Instance.GameController.UpdateTurn (cell);
    }
}
