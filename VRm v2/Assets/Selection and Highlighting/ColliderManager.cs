using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderManager : MonoBehaviour
{
    public GameObject highlightPrefab;
    public GameObject parentJoint;
    public GameObject positionJoint;
    public Transform resetReference;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {/*
            GameObject selectable = transform.GetChild(i).gameObject;

            //GameObject obj = Instantiate(highlightPrefab, parentJoint.transform);
            GameObject obj = Instantiate(highlightPrefab);
            
            obj.transform.parent = parentJoint.transform;
            obj.transform.position = positionJoint.transform.localPosition;
            obj.GetComponent<SkinnedMeshRenderer>().sharedMesh = selectable.gameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh;
            obj.GetComponent<MeshCollider>().sharedMesh = selectable.gameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh;
            obj.GetComponent<SelectableObject>().resetReference = resetReference;
            obj.GetComponent<SkinnedMeshRenderer>().rootBone = parentJoint.transform;
            */
        }
    }
}
