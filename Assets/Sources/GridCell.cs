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

    public Text buttonText = null;

    private CellEnum currentState = CellEnum.EMPTY;

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
        if (GameController.Controller.CurrentTurn == PlayerEnum.X)
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

       GameController.Controller.UpdateTurn (this);
    }
}
