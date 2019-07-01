using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Eraser : MonoBehaviour {

    public GameObject EraserPanel;
    public GameObject pen;
    private MarkerDrawing markerDrawing;

	// Use this for initialization
	void Start ()
    {
        markerDrawing = pen.GetComponent<MarkerDrawing>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    // method for when the user clicks on the eraser object
    private void OnMouseDown()
    {
        EraserPanel.SetActive(true);
    }

    // if yes is pressed
    public void Erase()
    {
        // erase the lines
        foreach(GameObject line in markerDrawing.boardDrawingPlanes)
        {
            //markerDrawing.boardDrawingPlanes.Remove(line);
            Destroy(line);
        }
        EraserPanel.SetActive(false);
    }

    // if no is pressed
    public void DontErase()
    {
        EraserPanel.SetActive(false);
    }
}
