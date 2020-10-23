using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Line))]
public class LineInspector : Editor {

    private void OnSceneGUI() {
        Line line = target as Line;

        Transform hTransform = line.transform;
        Quaternion handleRotation = Quaternion.identity;

        if (Tools.pivotRotation == PivotRotation.Local) {
            handleRotation = hTransform.rotation;
        }

        Vector3 p0 = hTransform.TransformPoint(line.p0);
        Vector3 p1 = hTransform.TransformPoint(line.p1);

        Handles.color = Color.white;
        Handles.DrawLine(line.p0, line.p1);

        EditorGUI.BeginChangeCheck();
        p0 = Handles.DoPositionHandle(p0, handleRotation);
        
        if (EditorGUI.EndChangeCheck()) {
            Undo.RecordObject(line, "Move Point");
            EditorUtility.SetDirty(line);
            line.p0 = hTransform.InverseTransformPoint(p0);
        }

        EditorGUI.BeginChangeCheck();
        p1 = Handles.DoPositionHandle(p1, handleRotation);

        if (EditorGUI.EndChangeCheck()) {
            Undo.RecordObject(line, "Move Point");
            EditorUtility.SetDirty(line);
            line.p1 = hTransform.InverseTransformPoint(p1);
        }
    }
}
