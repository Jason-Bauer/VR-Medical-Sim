using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class CustomRotation : MonoBehaviour
{

    public SteamVR_Action_Boolean m_GrabAction = null;
    public SteamVR_Action_Vector2 touchPadAction;

    private SteamVR_Behaviour_Pose m_Pose = null;
    private FixedJoint m_Joint = null;

    public CustomInteractable c_Interactable = null;
    public List<CustomInteractable> l_interactables = new List<CustomInteractable>();

    private float armYRot;
    private float armXRot;
    private float xbaseSpeed = 0;
    private float ybaseSpeed = 0;

    private float timeValue = 0.0f;

    /*
    private float rotationZ = 0f;
    private float rotationX = 0f;
    private float rotationY = 0f;
    */
    //Quaternion updatedRotation = Quaternion.identity;
    //public SteamVR_Action_Vector2 trackPadAction;


    private void Awake()
    {
        m_Pose = GetComponent<SteamVR_Behaviour_Pose>();
        m_Joint = GetComponent<FixedJoint>();
    }

    // Update is called once per frame
    void Update()
    {

        //updatedRotation = transform.rotation;
        // Vector2 trackPadValue = trackPadAction.GetAxis(SteamVR_Input_Sources.Any);
        //if(trackPadValue != Vector2.zero)
        // {
        //   print(trackPadValue);
        //}

        if (m_GrabAction.GetState(m_Pose.inputSource))
        {
            print(m_Pose.inputSource + "Trigger Down");
            JointRotation();

        }

        if (m_GrabAction.GetStateUp(m_Pose.inputSource))
        {
            print(m_Pose.inputSource + "Trigger Up");
            LetGo();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Interactable"))
        {
            return;
        }

        l_interactables.Add(other.gameObject.GetComponent<CustomInteractable>());
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Interactable"))
        {
            return;
        }

        l_interactables.Remove(other.gameObject.GetComponent<CustomInteractable>());
    }

    private CustomInteractable GetNearestInteractable()
    {
        CustomInteractable nearest = null;
        float minDistance = float.MaxValue;
        float distance = 0.0f;

        foreach (CustomInteractable inter in l_interactables)
        {
            distance = (inter.transform.position - transform.position).sqrMagnitude;

            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = inter;
            }
        }

        return nearest;
    }

    public void JointRotation()
    {
        c_Interactable = GetNearestInteractable();

        //if there's nothing
        if (!c_Interactable)
        {
            return;
        }

        //Already Interacting
        if (c_Interactable.m_ActiveHand)
        {
            //c_Interactable.m_ActiveHand.LetGo();
        }

        //Rotate based on hands orientation?
        /*
        Quaternion tempRotation = transform.rotation;
        if(tempRotation != updatedRotation)
        {
            float differencex = updatedRotation.x - tempRotation.x;
            float differencey = updatedRotation.y - tempRotation.y;
            float differencez = updatedRotation.z - tempRotation.z;
            c_Interactable.transform.localEulerAngles = new Vector3(c_Interactable.transform.localEulerAngles.x - differencex,
                                                                    c_Interactable.transform.localEulerAngles.y - differencey,
                                                                    c_Interactable.transform.localEulerAngles.z - differencez);
        }
        */

        armXRot += xbaseSpeed * c_Interactable.xRotSpeed;
        armYRot += ybaseSpeed * c_Interactable.yRotSpeed;

        Vector2 touchpadValue = touchPadAction.GetAxis(SteamVR_Input_Sources.Any);

        if (touchpadValue.x >= 0.4)
        {
            xbaseSpeed = 2;
            armXRot = Mathf.Clamp(armXRot, c_Interactable.minXLimit, c_Interactable.maxXLimit);
            //c_Interactable.transform.eulerAngles = new Vector3(armXRot, c_Interactable.transform.eulerAngles.y, c_Interactable.transform.eulerAngles.z);
            c_Interactable.transform.rotation = Quaternion.Euler(armXRot, c_Interactable.transform.eulerAngles.y, c_Interactable.transform.eulerAngles.z);
        }
        if (touchpadValue.x <= -0.4)
        {
            xbaseSpeed = -2;
            armXRot = Mathf.Clamp(armXRot, c_Interactable.minXLimit, c_Interactable.maxXLimit);
            c_Interactable.transform.rotation = Quaternion.Euler(armXRot, c_Interactable.transform.eulerAngles.y, c_Interactable.transform.eulerAngles.z);
            //c_Interactable.transform.eulerAngles = new Vector3(armXRot, c_Interactable.transform.eulerAngles.y, c_Interactable.transform.eulerAngles.z);
        }

        if (touchpadValue.y >= 0.4)
        {
            ybaseSpeed = 2;
            armYRot = Mathf.Clamp(armYRot, c_Interactable.minYLimit, c_Interactable.maxYLimit);
            c_Interactable.transform.rotation = Quaternion.Euler(c_Interactable.transform.eulerAngles.x, armYRot, c_Interactable.transform.eulerAngles.z);
            //Quaternion one = Quaternion.Euler(c_Interactable.transform.eulerAngles.x, c_Interactable.minYLimit, c_Interactable.transform.eulerAngles.z);
            //Quaternion two = Quaternion.Euler(c_Interactable.transform.eulerAngles.x, c_Interactable.maxYLimit, c_Interactable.transform.eulerAngles.z);
            //c_Interactable.transform.rotation = Quaternion.Slerp(one, two, timeValue * Time.deltaTime);
        }

        if (touchpadValue.y <= -0.4)
        {
            ybaseSpeed = -2;
            armYRot = Mathf.Clamp(armYRot, c_Interactable.minYLimit, c_Interactable.maxYLimit);
            c_Interactable.transform.rotation = Quaternion.Euler(c_Interactable.transform.eulerAngles.x, armYRot, c_Interactable.transform.eulerAngles.z);
        }

        if (touchpadValue.x == 0 || touchpadValue.y == 0)
        {
            xbaseSpeed = 0;
            ybaseSpeed = 0;
        }


        //c_Interactable.transform.localEulerAngles = transform.eulerAngles;
        //c_Interactable.transform.rotation = transform.rotation;


        //limitation

        //rotationY = Mathf.Clamp(rotationY, c_Interactable.minYLimit, c_Interactable.maxYLimit);
        //rotationZ = Mathf.Clamp(rotationZ, c_Interactable.minYLimit, c_Interactable.maxYLimit);
        //c_Interactable.transform.localEulerAngles = new Vector3(-rotationX, -rotationY, -rotationZ);
        /*
        if (c_Interactable.maxXLimit != 0 || c_Interactable.minXLimit != 0) //DON'T SET NEGATIVE VALUES
        {


            /*
            Quaternion rotationMinx = Quaternion.Euler(new Vector3(c_Interactable.minXLimit, 0, 0));
            Quaternion rotationMaxx = Quaternion.Euler(new Vector3(c_Interactable.maxXLimit, 0, 0));
            Quaternion rotation = c_Interactable.transform.rotation;

            Debug.Log("1 " + rotation.x); // results in a very tiny number
            Debug.Log("2 " + rotationMinx.x);
            Debug.Log("3 " + rotationMaxx.x);

            if (rotation.x <= rotationMinx.x)
            {
                c_Interactable.transform.eulerAngles = new Vector3(c_Interactable.minXLimit, 0, 0);
            }

            if (rotation.x >= rotationMaxx.x)
            {
                c_Interactable.transform.eulerAngles = new Vector3(c_Interactable.maxXLimit, 0, 0);
            }

            /////////////////DOESN'T WORK/////////////////
            Vector3 xJoint = c_Interactable.transform.rotation.eulerAngles;
            Debug.Log("SOMETHING" + c_Interactable.transform.rotation.eulerAngles);
            xJoint.x = Mathf.Clamp(rotationX, c_Interactable.minXLimit, c_Interactable.maxXLimit);
            c_Interactable.transform.rotation = Quaternion.Euler(xJoint);
        }


        if (c_Interactable.maxYLimit != 0 || c_Interactable.minYLimit != 0)
        {
            rotationY = Mathf.Clamp(rotationY, c_Interactable.minYLimit, c_Interactable.maxYLimit);
            c_Interactable.transform.localEulerAngles = new Vector3(-rotationX, -rotationY, -rotationZ);
        }
        else
        {
            //c_Interactable.transform.localEulerAngles = new Vector3(-rotationX, c_Interactable.transform.localEulerAngles.y, -rotationZ);
        }

        if (c_Interactable.maxXLimit != 0 || c_Interactable.minXLimit != 0)
        {
            rotationZ = Mathf.Clamp(rotationZ, c_Interactable.minZLimit, c_Interactable.maxZLimit);
            c_Interactable.transform.localEulerAngles = new Vector3(-rotationX, -rotationY, -rotationZ);
        }
        else
        {
            //c_Interactable.transform.localEulerAngles = new Vector3(-rotationX, -rotationY, c_Interactable.transform.localEulerAngles.z);
        }
        */


        //Attach
        //Rigidbody targetBody = c_Interactable.GetComponent<Rigidbody>();
        //m_Joint.connectedBody = targetBody;

        c_Interactable.m_ActiveHand = this;

    }

    public void LetGo()
    {
        if (!c_Interactable)
        {
            return;
        }
        //Detach
        //m_Joint.connectedBody = null;
        xbaseSpeed = 0;
        ybaseSpeed = 0;
        c_Interactable.m_ActiveHand = null;
        c_Interactable = null;
    }
}