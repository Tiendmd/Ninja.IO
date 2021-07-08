using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        FieldOfView fow = (FieldOfView)target;
        Handles.color = Color.yellow;
        Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.viewRadius);
        Vector3 viewAngleA = fow.DirFromAngle(-fow.viewAngle / 2, false);
        Vector3 viewAngleB = fow.DirFromAngle(fow.viewAngle / 2, false);
        if (fow.view1)
        {
            Handles.DrawLine(fow.transform.position - new Vector3(0.3f, 0, 0), fow.transform.position - new Vector3(0.3f, 0.2f, 0) + fow.transform.forward * fow.viewRadius);
            Handles.DrawLine(fow.transform.position + new Vector3(0.3f, 0, 0), fow.transform.position + new Vector3(0.3f, -0.2f, 0) + fow.transform.forward * fow.viewRadius);
            foreach (Transform visibleTarget in fow.visibleTargets)
            {
                Handles.DrawLine(fow.transform.position, visibleTarget.position);
            }
        }
        else if (fow.view2)
        {
            Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA * fow.viewRadius);
            Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB * fow.viewRadius);
            Handles.color = Color.red;
            foreach (Transform visibleTarget in fow.visibleTargets)
            {
                Handles.DrawLine(fow.transform.position, visibleTarget.position);
            }
        }
    }

}
