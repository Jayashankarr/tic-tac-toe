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
        get {return currentState;}
    }

    public Vector2 CellIndex 
    {
        set {cellIndex = value;}

        get {return cellIndex;}
    }

    public int Index
    {
        set {index = value; buttonText.text = value.ToString();}

        get {return index;}
    }

    private int index;

    private Vector2 cellIndex;

    public void setCell ()
    {
        Debug.Log ("button pressed");
        x.SetActive (true);
        button.interactable = false;

        GameController.Controller.player_x |= Index;

        Debug.Log (Convert.ToString(GameController.Controller.player_x,2));

        currentState = CellEnum.X;

        GameController.Controller.UpdateTurn (this);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
