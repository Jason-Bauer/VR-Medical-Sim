using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class SphericalCoordRotationController : MonoBehaviour
{
    //public Quaternion Rot;
    public Camera Cam;
    public bool AllowKBCtrl = false;
    public GameObject Center;
    public float RotationMultiplier = 3.0f;
    public List<string> ValidFingerNames;

    public Vector3 PastProjectionVectorDirection;
    public Vector3 CurrentProjectionVectorDirection;
    public SteamVR_Action_Boolean GrabSteamVRInput;

    public GameObject FingerTip;
    public GameObject lFinger;
    public GameObject rFinger;
    public bool collidingWithAFinger;
    public bool collidingWithLFinger;
    public bool collidingWithRFinger;

    public bool rightHasValidCollider = false;
    public bool leftHasValidCollider = false;
    public bool activeFingerisRight = false;
    public bool leftIsRenamed = false;

    public float ClosestDist = Mathf.Infinity;

    private void Awake()
    {
        if (Cam == null)
            Cam = Camera.current;
    }

    // Update is called once per frame
    void Update()
    {
        if (Cam == null)
            Cam = Camera.current;
        if (AllowKBCtrl)
            KBCtrl();

        if (!rightHasValidCollider && leftIsRenamed)
        {
            rFinger = GameObject.Find("finger_index_r_end");
            if (rFinger != null)
            {
                Debug.Log("Initialized RIGHT FINGER!");
                if (rFinger.gameObject.GetComponent<Collider>() == null)
                    rFinger.AddComponent<SphereCollider>();
                if (rFinger.gameObject.GetComponent<Rigidbody>() == null)
                    rFinger.AddComponent<Rigidbody>();
                rFinger.GetComponent<Rigidbody>().isKinematic = true;
                rightHasValidCollider = true;
                rFinger.GetComponent<SphereCollider>().transform.localScale *= .0025f;
                rFinger.layer = 9;
            }
        }

        if (!leftHasValidCollider)
        {
            GameObject leftHand = GameObject.Find("slim_l");
            if (leftHand != null)
            {
                leftIsRenamed = true;
                RecursiveChildNameReplace(leftHand, "_r", "_l");
                Debug.Log("RENAMED LEFT FINGERS!");
                lFinger = GameObject.Find("finger_index_l_end");
                if (lFinger != null)
                {
                    Debug.Log("Initialized LEFT FINGER!");
                    if (lFinger.gameObject.GetComponent<Collider>() == null)
                        lFinger.AddComponent<SphereCollider>();
                    if (lFinger.gameObject.GetComponent<Rigidbody>() == null)
                        lFinger.AddComponent<Rigidbody>();
                    lFinger.GetComponent<Rigidbody>().isKinematic = true;
                    leftHasValidCollider = true;
                    lFinger.GetComponent<SphereCollider>().transform.localScale *= .0025f;
                    lFinger.layer = 9;
                }
            }
        }
    }

    public void RecursiveChildNameReplace(GameObject g, string toReplace, string replacer)
    {
        if (g.transform.childCount > 0)
            foreach (Transform gChild in g.GetComponentInChildren<Transform>())
                RecursiveChildNameReplace(gChild.gameObject, toReplace, replacer);

        g.name = g.name.Replace(toReplace, replacer);
    }

    public void onButtonRelease()
    {
        collidingWithAFinger = collidingWithLFinger || collidingWithRFinger;
        if (lFinger != null)
            CheckTrigger(lFinger.GetComponent<Collider>(), collidingWithLFinger, collidingWithRFinger, activeFingerisRight, 'l');
        if (rFinger != null)
            CheckTrigger(rFinger.GetComponent<Collider>(), collidingWithRFinger, collidingWithLFinger, activeFingerisRight, 'r');
    }

    public void OnTriggerEnter(Collider col)
    {
        CheckForHit(col);
    }

    public void CheckForHit(Collider col)
    {
        collidingWithAFinger = collidingWithLFinger || collidingWithRFinger;
        CheckTrigger(col, collidingWithRFinger, collidingWithLFinger, activeFingerisRight, 'r');
        CheckTrigger(col, collidingWithLFinger, collidingWithRFinger, activeFingerisRight, 'l');
        collidingWithAFinger = collidingWithLFinger || collidingWithRFinger;
    }

    public void CheckTrigger(Collider col, bool fingerIsAlreadyColliding, bool otherFingerIsAlreadyColliding, bool currentTarget, char side)
    {
        if (fingerIsAlreadyColliding)
            return;

        bool sideBool = side == 'r';

        bool validFinger = ValidateFingerName(col.gameObject.name);
        if (validFinger)
        {
            if (GrabSteamVRInput.state)
            {
                Debug.Log("Valid Fingerhit: " + col.gameObject.name);
                FingerTip = col.gameObject;
                if (col.gameObject.name == ValidFingerNames[1])
                {
                    collidingWithRFinger = true;
                    activeFingerisRight = true;
                }
                else
                {
                    collidingWithLFinger = true;
                    activeFingerisRight = false;
                }
                if (sideBool == currentTarget)
                {
                    CurrentProjectionVectorDirection = FingerTip.gameObject.transform.position - Center.transform.position;
                    PastProjectionVectorDirection = CurrentProjectionVectorDirection;
                }
            }
        }
        collidingWithAFinger = collidingWithLFinger || collidingWithRFinger;
    }

    public bool ValidateFingerName(string s)
    {
        return ValidFingerNames.Contains(s);
    }

    public void OnTriggerStay(Collider col)
    {
        CheckForHit(col);
        if (!collidingWithAFinger)
            return;

        if (col.gameObject == FingerTip)
        {
            Debug.Log("Valid FingerStay: " + gameObject.name);
            PastProjectionVectorDirection = CurrentProjectionVectorDirection;
            CurrentProjectionVectorDirection = FingerTip.gameObject.transform.position - Center.transform.position;

            Quaternion rot = Quaternion.FromToRotation(PastProjectionVectorDirection, CurrentProjectionVectorDirection);
            rot = Quaternion.SlerpUnclamped(Quaternion.identity, rot, RotationMultiplier);
            transform.rotation = rot * transform.rotation;
        }
        collidingWithAFinger = collidingWithLFinger || collidingWithRFinger;

        if (!GrabSteamVRInput.state)
        {
            if (ValidateFingerName(col.name))
            {
                DisconnectFinger(col);
            }
            if (col.gameObject == lFinger)
            {
                if (collidingWithRFinger)
                {
                    if (rFinger != null)
                    {
                        DisconnectFinger(rFinger.GetComponent<Collider>());
                        {
                            SetFingerTip(null, false); ClosestDist = Mathf.Infinity;
                        }
                    }
                    else
                    {
                        SetFingerTip(null, false); ClosestDist = Mathf.Infinity;
                    }
                }
                else
                {
                    SetFingerTip(null, false); ClosestDist = Mathf.Infinity;
                }
            }
            else if (col.gameObject == rFinger)
            {
                if (collidingWithLFinger)
                {
                    if (lFinger != null)
                    {
                        DisconnectFinger(lFinger.GetComponent<Collider>());
                        {
                            SetFingerTip(null, false); ClosestDist = Mathf.Infinity;
                        }
                    }
                    else
                    {
                        SetFingerTip(null, false); ClosestDist = Mathf.Infinity;
                    }
                }
                else
                {
                    SetFingerTip(null, false); ClosestDist = Mathf.Infinity;
                }
            }
            else
            {
                SetFingerTip(null, false); ClosestDist = Mathf.Infinity;
            }
        }
        else
        {
            if (activeFingerisRight) {
                ClosestDist = (rFinger.transform.position - transform.position).magnitude;
                if ((lFinger.transform.position - transform.position).magnitude < ClosestDist)
                {
                    SetFingerTip(lFinger, false);
                    collidingWithAFinger = collidingWithLFinger || collidingWithRFinger;
                }
            }
            else
            {
                ClosestDist = (lFinger.transform.position - transform.position).magnitude;
                if ((rFinger.transform.position - transform.position).magnitude < ClosestDist)
                {
                    SetFingerTip(rFinger, false);
                    collidingWithAFinger = collidingWithLFinger || collidingWithRFinger;
                }
            }
        }

        collidingWithAFinger = collidingWithLFinger || collidingWithRFinger;
    }

    public void OnTriggerExit(Collider col)
    {
        if (!collidingWithAFinger)
            return;
        if (ValidateFingerName(col.name))
            DisconnectFinger(col);
        collidingWithAFinger = collidingWithLFinger || collidingWithRFinger;
    }

    private void DisconnectFinger(Collider col)
    {
        if (ValidFingerNames.Contains(col.gameObject.name))
        {
            Debug.Log("Valid Fingerleave: " + gameObject.name);
            if (col.gameObject.name == ValidFingerNames[1])
                collidingWithRFinger = false;
            else
                collidingWithLFinger = false;

            collidingWithAFinger = collidingWithLFinger || collidingWithRFinger;
            SetNewFingerTip(FingerTip);
        }
    }

    public void SetNewFingerTip(GameObject fingerTipLost)
    {
        if (fingerTipLost == lFinger)
        {
            if (collidingWithRFinger)
            {
                if (rFinger != null)
                {
                    SetFingerTip(rFinger, true);
                    activeFingerisRight = true;
                }
                else
                {
                    SetFingerTip(null, false); ClosestDist = Mathf.Infinity;
                }
            }
            else
            {
                SetFingerTip(null, false); ClosestDist = Mathf.Infinity;
            }
        }
        else if (fingerTipLost == rFinger)
        {
            if (collidingWithLFinger)
            {
                if (lFinger != null)
                {
                    SetFingerTip(lFinger, false);
                    activeFingerisRight = false;
                }
                else
                {
                    SetFingerTip(null, false); ClosestDist = Mathf.Infinity;
                }
            }
            else
            {
                SetFingerTip(null, false); ClosestDist = Mathf.Infinity;
            }
        }
        else
        {
            ClosestDist = Mathf.Infinity;
            SetFingerTip(null, false);
        }
    }

    public void SetFingerTip(GameObject newFinger, bool setActiveRight)
    {
        FingerTip = newFinger;
        if (FingerTip == null)
        {
            PastProjectionVectorDirection = Vector3.zero;
            CurrentProjectionVectorDirection = Vector3.zero;
        }
        else
        {
            CurrentProjectionVectorDirection = FingerTip.gameObject.transform.position - Center.transform.position;
            PastProjectionVectorDirection = CurrentProjectionVectorDirection;
            if (setActiveRight)
                activeFingerisRight = true;
            else
                activeFingerisRight = false;
        }
    }

    private void KBCtrl()
    {
        if (Input.GetKey("up"))
        {
            transform.rotation = Quaternion.AngleAxis(5, Cam.transform.right) * transform.rotation;
        }

        if (Input.GetKey("down"))
        {
            transform.rotation = Quaternion.AngleAxis(-5, Cam.transform.right) * transform.rotation;
        }

        if (Input.GetKey("left"))
        {
            transform.rotation = Quaternion.AngleAxis(5, Cam.transform.up) * transform.rotation;
        }

        if (Input.GetKey("right"))
        {
            transform.rotation = Quaternion.AngleAxis(-5, Cam.transform.up) * transform.rotation;
        }
    }
}



/// DEPRECATED CODE
/// public void OnTriggerEnter(Collider col)
//    {
//        if (collidingWithFinger)
//            return;

//        if (ValidFingerNames.Contains(col.gameObject.name))
//        {
//            Debug.Log("Valid Fingerhit: " + col.gameObject.name);
//            FingerTip = col.gameObject;
//            collidingWithFinger = true;
//            CurrentProjectionVectorDirection = FingerTip.gameObject.transform.position-Center.transform.position;
//            PastProjectionVectorDirection = CurrentProjectionVectorDirection;
//        }
//    }

//    public void OnTriggerStay(Collider col)
//{
//    if (!collidingWithFinger)
//        return;

//    if (col.gameObject == FingerTip)
//    {
//        Debug.Log("Valid FingerStay: " + gameObject.name);
//        PastProjectionVectorDirection = CurrentProjectionVectorDirection;
//        CurrentProjectionVectorDirection = FingerTip.gameObject.transform.position - Center.transform.position;

//        Quaternion rot = Quaternion.FromToRotation(PastProjectionVectorDirection, CurrentProjectionVectorDirection);
//        rot = Quaternion.SlerpUnclamped(Quaternion.identity, rot, RotationMultiplier);
//        transform.rotation = rot * transform.rotation;
//    }

//    // If !grabbing button --> DisconnectFinger();  will also need to check if other finger is in collision and grabbing
//}

//public void OnTriggerExit(Collider col)
//{
//    if (!collidingWithFinger)
//        return;
//    DisconnectFinger(col);
//}

//private void DisconnectFinger(Collider col)
//{
//    if (ValidFingerNames.Contains(col.gameObject.name))
//    {
//        Debug.Log("Valid Fingerleave: " + gameObject.name);
//        collidingWithFinger = false;
//        FingerTip = null;
//        PastProjectionVectorDirection = Vector3.zero;
//        CurrentProjectionVectorDirection = Vector3.zero;
//    }
//}