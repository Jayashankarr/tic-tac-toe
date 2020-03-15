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
    private GameObject x;

    [SerializeField]
    private GameObject o;

    [SerializeField]
    private Button button = null;

    public void setCell ()
    {
        Debug.Log ("button pressed");
        x.SetActive (true);
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
