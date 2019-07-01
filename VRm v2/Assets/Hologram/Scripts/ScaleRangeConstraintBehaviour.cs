using UnityEngine;
using UnityEditor;
[ExecuteInEditMode]
public class ScaleRangeConstraintBehaviour : MonoBehaviour
{
    public Vector3 MinScale;
    public Vector3 MaxScale;
    public bool AllowKBCtrl = false;

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.x > MaxScale.x)
            transform.localScale = new Vector3(MaxScale.x, transform.localScale.y, transform.localScale.z);
        if (transform.localScale.y > MaxScale.y)
            transform.localScale = new Vector3(transform.localScale.x, MaxScale.y, transform.localScale.z);
        if (transform.localScale.z > MaxScale.z)
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, MaxScale.z);
        if (transform.localScale.x < MinScale.x)
            transform.localScale = new Vector3(MinScale.x, transform.localScale.y, transform.localScale.z);
        if (transform.localScale.y < MinScale.y)
            transform.localScale = new Vector3(transform.localScale.x, MinScale.y, transform.localScale.z);
        if (transform.localScale.z < MinScale.z)
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, MinScale.z);
        if (Input.GetKey("w") && AllowKBCtrl)
            transform.localScale = Vector3.Lerp(transform.localScale, MaxScale, 0.1f);
        if (Input.GetKey("s") && AllowKBCtrl)
            transform.localScale = Vector3.Lerp(transform.localScale, MinScale, 0.1f);
    }
}
#if UNITY_EDITOR
[CustomEditor(typeof(ScaleRangeConstraintBehaviour))]
public class AZProceduralAudioManagerEditor : Editor
{
    bool defaultGUI = true;
    public override void OnInspectorGUI()
    {
        defaultGUI = (GUILayout.Button("Default Inspector")) ? !defaultGUI : defaultGUI;
        if (defaultGUI == true)
            base.OnInspectorGUI();

        ScaleRangeConstraintBehaviour scaleCtrl = (ScaleRangeConstraintBehaviour)target;
        if (GUILayout.Button("Set Min Scale"))
            scaleCtrl.MinScale = scaleCtrl.gameObject.transform.localScale;
        if (GUILayout.Button("Set Max Scale"))
            scaleCtrl.MaxScale = scaleCtrl.gameObject.transform.localScale;
    }
}
#endif