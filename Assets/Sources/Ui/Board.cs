using System.Collections;
using System.Collections.Generic;
using TicTakToe.Utils;
using UnityEngine;

namespace TicTakToe.Ui
{
    public class Board : MonoBehaviour
    {
        private GridCell[] cells = null;

        public GridCell[] Cells {get {return cells;}}

        private void Awake ()
        {
            cells = GetComponentsInChildren<GridCell>();

            for (int i = 0; i < cells.Length; i++)
            {
                cells[i].Index = i;
            }
        }
    }
}
