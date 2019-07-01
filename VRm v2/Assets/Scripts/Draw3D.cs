using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Draw3D : MonoBehaviour
{
    public SteamVR_ActionSet set;
    public SteamVR_Action_Boolean draw; 
    public GameObject lineObject3D; // prefab to instantiate when drawing a new line
    public GameObject lineObjectParent; // parent for the instantiated gameobjects
    LineRenderer lines3D; // current instance of the line being drawn in 3D space

    int numLinePositions;
    public bool drawLine = false;
    public bool firstframe = true;

    public List<GameObject> linePlanes3D;

    // Start is called before the first frame update
    void Start()
    {
        linePlanes3D = new List<GameObject>();
        numLinePositions = 0;
    }

    // method to turn on 

    // Update is called once per frame
    void Update()
    {
        // check for drawing button with VR controllers
        if (draw.state)
        {
            Debug.Log("aaaaaaaaaaaaaaaaaaaa");
            drawLine = true;
        }
        else
        {
            drawLine = false;
            firstframe = true;
        }

        // check for the drawing button on PC
        if (Input.GetKeyDown(KeyCode.Space))
        {
            drawLine = true;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            drawLine = false;
            firstframe = true;
        }

        // start drawing
        if(drawLine)
        {
            // instantiate the new line object
            if(firstframe)
            {
                GameObject tempLine3D = Instantiate(lineObject3D, gameObject.transform.position, Quaternion.identity);
                lines3D = tempLine3D.GetComponent<LineRenderer>();
                lines3D.useWorldSpace = true;

                // set the parent
                tempLine3D.transform.parent = lineObjectParent.transform;
                linePlanes3D.Add(tempLine3D);

                // reset variables
                firstframe = false;
                numLinePositions = 0;
            }
            else
            {
                lines3D.positionCount = numLinePositions + 1;
                lines3D.SetPosition(numLinePositions, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z));
                numLinePositions++;
            }
        }
    }
}
