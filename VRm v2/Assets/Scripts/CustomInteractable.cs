using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class CustomInteractable : MonoBehaviour {

    public CustomRotation m_ActiveHand = null;
    public float maxXLimit, minXLimit, xRotSpeed;
    public float maxYLimit, minYLimit, yRotSpeed;

}

