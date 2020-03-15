using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

//using UnityEngine.UIElements;
//using static System.Net.Mime.MediaTypeNames;

public class GridCell : MonoBehaviour
{
    [SerializeField]
    private Image x;

    [SerializeField]
    private Image o;

    [SerializeField]
    private Button button = null;

    [SerializeField]
    private string playerTxt = null;

    [SerializeField]
    private Text buttonText = null;

    public void setCell ()
    {
        Debug.Log ("button pressed");
        buttonText.text = playerTxt;
        button.interactable = false;
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
