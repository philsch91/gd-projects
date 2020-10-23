using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BezierCurve))]
public class BezierCurveInspector : Editor {

    private BezierCurve curve;
    private Transform hTransform;
    private Quaternion hRotation;
    private const int lineSteps = 10;
    private const float directionScale = 0.5f;

    private void OnSceneGUI() {
        this.curve = target as BezierCurve;
        hTransform = this.curve.transform;
        hRotation = Quaternion.identity;

        if (Tools.pivotRotation == PivotRotation.Local) {
            hRotation = hTransform.rotation;
        }

        Vector3 p0 = this.ShowPoint(0);
        Vector3 p1 = this.ShowPoint(1);
        Vector3 p2 = this.ShowPoint(2);
        Vector3 p3 = this.ShowPoint(3);

        Handles.color = Color.gray;
        Handles.DrawLine(p0, p1);
        Handles.DrawLine(p1, p2);
        Handles.DrawLine(p2, p3);

        /*
        Vector3 lineStart = this.curve.GetPoint(0f);
        Handles.color = Color.green;
        //Handles.DrawLine(lineStart, lineStart + this.curve.GetVelocity(0f));
        Handles.DrawLine(lineStart, lineStart + this.curve.GetDirection(0f));
        for (int i = 1; i <= lineSteps; i++) {
            // Draw curve
            Vector3 lineEnd = this.curve.GetPoint(i / (float)lineSteps);
            Handles.color = Color.white;
            Handles.DrawLine(lineStart, lineEnd);
            // Draw direction
            Handles.color = Color.green;
            //Handles.DrawLine(lineEnd, lineEnd + curve.GetVelocity(i / (float)lineSteps));
            Handles.DrawLine(lineEnd, lineEnd + curve.GetDirection(i / (float)lineSteps));
            lineStart = lineEnd;
        } */

        this.ShowDirections();
        Handles.DrawBezier(p0, p3, p1, p2, Color.white, null, 2f);
    }

    private Vector3 ShowPoint(int index) {
        Vector3 point = this.hTransform.TransformPoint(this.curve.points[index]);
        EditorGUI.BeginChangeCheck();
        point = Handles.DoPositionHandle(point, this.hRotation);

        if (EditorGUI.EndChangeCheck()) {
            Undo.RecordObject(this.curve, "Move Point");
            EditorUtility.SetDirty(this.curve);
            this.curve.points[index] = this.hTransform.InverseTransformPoint(point);
        }

        return point;
    }

    private void ShowDirections() {
        Handles.color = Color.green;
        Vector3 point = this.curve.GetPoint(0f);
        Handles.DrawLine(point, point + this.curve.GetDirection(0f) * directionScale);
        for(int i = 1; i <= lineSteps; i++) {
            point = curve.GetPoint(i / (float)lineSteps);
            Handles.DrawLine(point, point + this.curve.GetDirection(i / (float)lineSteps) * directionScale);
        }
    }

}
