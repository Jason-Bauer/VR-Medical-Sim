using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using TMPro;

public class SelectableObject : MonoBehaviour {

    public bool isHighlighted;
    public bool isSelected;

    public Transform parentJoint;
    public Transform resetReference;

    private GameObject descriptionMonitor;
    private GameObject highlightManager;
    private Dictionary<string, string> popupStrings;

    private SkinnedMeshRenderer skinnedMR;
    public Material mat1, mat2;
	// Use this for initialization
	void Start () {
        skinnedMR = this.GetComponent<SkinnedMeshRenderer>();
        descriptionMonitor = GameObject.Find("Description_Monitor");
        highlightManager = GameObject.Find("HighlightingManager");
        popupStrings = highlightManager.GetComponent<PopUpStrings>().Strings;
        //skinnedMR.rootBone = GameObject.Find("Arm").transform;


        //parentJoint = transform.parent;
        // transform.parent = resetReference;
        // transform.position = Vector3.zero;
        /*
        transform.rotation = Quaternion.identity;

        transform.parent = parentJoint;
        transform.position = new Vector3(parentJoint.transform.position.x, parentJoint.transform.position.y - 1, parentJoint.transform.position.z);
        */

    }

    public void Highlight()
    {
        isHighlighted = true;
    }

    public void Select()
    {
        isSelected = true;
    }
    
    public void Deselect()
    {
        skinnedMR.material.shader = Shader.Find("Standard");
        // skinnedMR.material = mat1;
        // skinnedMR.enabled = false;
        isSelected = false;
    }

	// Update is called once per frame
	void Update () {

        skinnedMR.material.SetColor("_TintColor", Color.yellow);
        if (highlightManager.GetComponent<HighlightingControls>().selectedObject != gameObject)
            Deselect();
        // skinnedMR.enabled = false;
        if (isHighlighted)
        {
            skinnedMR.material.shader = Shader.Find("Valve/VR/Highlight");
            skinnedMR.material.SetColor("_TintColor", Color.yellow);
            // skinnedMR.enabled = true;
            // change color if selecting, and call popup method
        }
        if (isSelected)
        {
            // skinnedMR.material = mat2;
            skinnedMR.material.SetColor("_TintColor", Color.red);
            // call whatever is needed to bring up info
            if (popupStrings.ContainsKey(gameObject.name))
                descriptionMonitor.GetComponentInChildren<TextMeshPro>().text = popupStrings[gameObject.name];
            else
                descriptionMonitor.GetComponentInChildren<TextMeshPro>().text = "Sorry, we don't have information on that one yet. We're still working on it!";
        }
        isHighlighted = false;
    }
}
