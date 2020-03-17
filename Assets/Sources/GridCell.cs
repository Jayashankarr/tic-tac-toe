using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System;

//using UnityEngine.UIElements;
//using static System.Net.Mime.MediaTypeNames;

public class GridCell : MonoBehaviour
{
    [SerializeField]
    private GameObject x;

    [SerializeField]
    private GameObject o;

    [SerializeField]
    private Button button = null;

    private CellEnum currentState = CellEnum.EMPTY;

    public Button Button
    {
        get {return button;}
    }

    public CellEnum CurrentState
    {
        set {currentState = value;}
        
        get {return currentState;}
    }

    public Vector2 CellIndex 
    {
        set {cellIndex = value;}

        get {return cellIndex;}
    }

    public int Index
    {
        set {index = value;}

        get {return index;}
    }

    private int index;

    private Vector2 cellIndex;

    public void setCell ()
    {
        if (GameManager.Instance.GameController.CurrentTurn == PlayerEnum.X)
        {
            x.SetActive (true);

            currentState = CellEnum.X;


        }
        else
        {
            o.SetActive (true);

            currentState = CellEnum.O;
        }

        button.interactable = false;

       GameManager.Instance.GameController.UpdateTurn (this);
    }

    public void ResetCell ()
    {
        currentState = CellEnum.EMPTY;

        x.SetActive (false);

        o.SetActive (false);
    }
}
