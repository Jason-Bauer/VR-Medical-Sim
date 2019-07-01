using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.Extras;

public class HighlightingControls : MonoBehaviour {


    [Header("Object References")]
    public Transform rightController;
    public Transform leftController;

    public SteamVR_LaserPointer rightLaser;
    public SteamVR_LaserPointer leftLaser;

    [Header("Input Variables")]
    public SteamVR_ActionSet actionSet;
    public SteamVR_Action_Boolean rightHighlight;
    public SteamVR_Action_Boolean rightSelect;
    public SteamVR_Action_Boolean leftHighlight;
    public SteamVR_Action_Boolean leftSelect;

    public GameObject selectedObject;
    [HideInInspector]
    public bool selected;

    // Use this for initialization
    void Start () {
        actionSet.Activate();
	}
	
	// Update is called once per frame
	void Update () {

        RaycastHit hit;
        GameObject other;

        if (rightHighlight.state)
        {
            rightLaser.thickness = 0.0025f;
            rightLaser.color = Color.black;

           
            // highlight object that is being pointed at
            if (Physics.Raycast(rightController.position, rightController.forward, out hit))
            {
                other = hit.collider.gameObject;

                if (other.GetComponentInParent<SelectableObject>())
                {
                    other.GetComponentInParent<SelectableObject>().Highlight();
                }

                if (rightSelect.state)
                {
                    rightLaser.color = Color.red;

                    if (other.GetComponentInParent<SelectableObject>())
                    {
                        other.GetComponentInParent<SelectableObject>().Select();
                        selectedObject = other;
                    }
                }

            }

        }
        else
        {
            rightLaser.thickness = 0.0f;
        }

        if (leftHighlight.state)
        {
            leftLaser.thickness = 0.0025f;
            leftLaser.color = Color.black;

            // highlight object that is being pointed at
            if (Physics.Raycast(leftController.position, leftController.forward, out hit))
            {
                other = hit.collider.gameObject;

                if (other.GetComponentInParent<SelectableObject>())
                {
                    other.GetComponentInParent<SelectableObject>().Highlight();
                }

                if (leftSelect.state)
                {
                    leftLaser.color = Color.red;

                    if (other.GetComponentInParent<SelectableObject>())
                    {
                        other.GetComponentInParent<SelectableObject>().Select();
                        selectedObject = other;
                    }
                }

            }
        }
        else
        {
            leftLaser.thickness = 0.0f;
        }
    }
}
