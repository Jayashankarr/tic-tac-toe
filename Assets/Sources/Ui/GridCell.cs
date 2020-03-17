using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TicTakToe.Utils;
using TicTakToe.Manager;

namespace TicTakToe.Ui
{
    public class GridCell : MonoBehaviour
    {
        [SerializeField]
        private GameObject x;

        [SerializeField]
        private GameObject o;

        [SerializeField]
        private Button button = null;

        private CellSymbol cellAssignedSymbol = CellSymbol.EMPTY;

        public Button Button
        {
            get {return button;}
        }

        public CellSymbol CellAssignedSymbol
        {
            set {cellAssignedSymbol = value;}
            
            get {return cellAssignedSymbol;}
        }

        public Vector2 Cell2DIndex 
        {
            set {cell2DIndex = value;}

            get {return cell2DIndex;}
        }

        public int Index
        {
            set {index = value;}

            get {return index;}
        }

        private int index;

        private Vector2 cell2DIndex;

        public void setCell ()
        {
            if (GameManager.Instance.GameController.CurrentTurn == PlayerSymbol.X)
            {
                x.SetActive (true);

                cellAssignedSymbol = CellSymbol.X;


            }
            else
            {
                o.SetActive (true);

                cellAssignedSymbol = CellSymbol.O;
            }

            button.interactable = false;

            GameManager.Instance.GameController.UpdateTurn (this);
        }

        public void ResetCell ()
        {
            cellAssignedSymbol = CellSymbol.EMPTY;

            x.SetActive (false);

            o.SetActive (false);
        }
    }
}
