using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerDrawing : MonoBehaviour
{
    public GameObject boardLineObject; // prefab to be instantiated when drawing a new line
    public GameObject lineObjectParent; // parent for the instantiated gameobjects 
    LineRenderer boardLines; // current instance of the line on the board being drawn
    LineRenderer markerLine; // the tip of the marker

    int numLinePositions;
    public Camera cam;
    public bool drawLine = false;
    public bool inBox = false;
    private bool firstFrame = true;

    public List<GameObject> boardDrawingPlanes;

    // Use this for initialization
    void Start()
    {
        boardDrawingPlanes = new List<GameObject>();
        cam = GetComponentInParent<Camera>();
        numLinePositions = 0;
        markerLine = gameObject.GetComponent<LineRenderer>();
        markerLine.startWidth = 0.01f;
        markerLine.endWidth = 0.01f;
        markerLine.material.color = Color.red;
        markerLine.endColor = Color.red;
        numLinePositions = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (drawLine)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Camera.main.transform.forward, out hit, 0.1f)) // need to make the raycast finite somehow
            {
                if (inBox)
                {
                    //Debug.Log("Hit WhiteBoard");
                    boardLines.positionCount = numLinePositions + 1;

                    //boardLines.SetPosition(numLinePositions, new Vector2(hit.normal.x, hit.normal.y));
                    boardLines.SetPosition(numLinePositions, new Vector3(hit.point.x, hit.point.y, hit.point.z));     //Collision points don't necessarily have to be on the physical object (looks weird)
                    numLinePositions++;
                }
            }
        }
    }

    // make a method to be called by a SteamVR script upon the object being picked up
    public void OnPickup()
    {
        drawLine = true;
        markerLine.SetPosition(0, Vector3.zero);
        markerLine.SetPosition(1, transform.forward.normalized * -0.1f);
        gameObject.GetComponentInParent<Rigidbody>().isKinematic = false;
        gameObject.GetComponentInParent<Rigidbody>().useGravity = false;
    }

    public void OnDrop()
    {
        drawLine = false;
        markerLine.SetPosition(0, Vector3.zero);
        markerLine.SetPosition(1, Vector3.zero);
        gameObject.GetComponentInParent<Rigidbody>().isKinematic = false;
        gameObject.GetComponentInParent<Rigidbody>().useGravity = true;
    }


    // On Trigger Enter and Exit methods determine when to create a new LineRenderer Gameobject
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "WhiteBoard")
        {
            inBox = true;
            if (firstFrame)
            {
                Debug.Log("FoundWhiteBoard");
                GameObject tempBoard = Instantiate(boardLineObject, boardLineObject.transform.position, Quaternion.identity);
                tempBoard.GetComponent<MeshRenderer>().enabled = false;
                boardLines = tempBoard.GetComponent<LineRenderer>();
                boardLines.useWorldSpace = true;
                boardDrawingPlanes.Add(tempBoard);
                firstFrame = false;
                numLinePositions = 0;
                tempBoard.transform.SetParent(lineObjectParent.transform);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "WhiteBoard")
        {
            inBox = false;
            firstFrame = true;
        }
    }
}