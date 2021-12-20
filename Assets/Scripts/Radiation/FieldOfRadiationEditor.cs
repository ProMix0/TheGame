using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
 [CustomEditor (typeof (RadiationVisualFieldOfRadiation))]
public class FieldOfRadiationEditor : Editor
{
    void OnSceneGUI()
    {
        RadiationVisualFieldOfRadiation rad = (RadiationVisualFieldOfRadiation)target;
        Handles.color = Color.green;
        Handles.DrawWireArc(rad.transform.position, Vector3.up, Vector3.forward, 360, rad.radius);
         
    }
}
