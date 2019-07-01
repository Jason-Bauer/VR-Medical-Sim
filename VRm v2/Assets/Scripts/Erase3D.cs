using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Erase3D : MonoBehaviour
{
    public GameObject DrawSpace;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // if the player puts their hand into the trigger area, erase the 3D drawing
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("InEraseBox");
        if(other.gameObject.tag == "Hand")
        {
            Debug.Log("Identified as hand");
            foreach (Transform child in DrawSpace.GetComponentsInChildren<Transform>())
            {
                Destroy(child.gameObject);
            }
        }
    }

    // maybe try this instead?
    public void OnMouseDown()
    {
        Debug.Log("Clicked on Recycling Bin");
        foreach (Transform child in DrawSpace.GetComponentsInChildren<Transform>())
        {
            Destroy(child.gameObject);
        }
    }

    // just call this on pickup or release of object I guess?
    public void Erase()
    {
        List<GameObject> linerenders = new List<GameObject>();

       foreach(GameObject child in GameObject.FindGameObjectsWithTag("LineObj"))
        {
            Destroy(child.gameObject);
        }
        /*
        foreach (Transform child in DrawSpace.GetComponentsInChildren<Transform>())
        {
            Destroy(child.gameObject);
        }
        */
    }
}
