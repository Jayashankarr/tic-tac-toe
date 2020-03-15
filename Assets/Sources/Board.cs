using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    private GridCell[] cells = null;

    public GridCell[] Cells {get {return cells;}}

    private void Start ()
    {
        cells = GetComponentsInChildren<GridCell>();
    }
}
