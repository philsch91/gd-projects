using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BezierSpline))]
public class BezierSplineInspector : Editor {

    private BezierSpline spline;
    private Transform hTransform;
    private Quaternion hRotation;
    
    private const int lineSteps = 10;
    private const float directionScale = 0.5f;
    private const int stepsPerCurve = 10;
    private const float handleSize = 0.04f;
    private const float pickSize = 0.06f;

    private int selectedIndex = -1;

    private static Color[] modeColors = {
        Color.white,
        Color.yellow,
        Color.cyan
    };

    private void OnSceneGUI() {
        this.spline = target as BezierSpline;
        hTransform = this.spline.transform;
        hRotation = Quaternion.identity;

        if (Tools.pivotRotation == PivotRotation.Local) {
            hRotation = hTransform.rotation;
        }

        Vector3 p0 = this.ShowPoint(0);
        for (int i = 1; i < this.spline.ControlPointCount; i += 3) {
            Vector3 p1 = this.ShowPoint(i);
            Vector3 p2 = this.ShowPoint(i + 1);
            Vector3 p3 = this.ShowPoint(i + 2);

            Handles.color = Color.gray;
            Handles.DrawLine(p0, p1);
            Handles.DrawLine(p1, p2);
            Handles.DrawLine(p2, p3);

            Handles.DrawBezier(p0, p3, p1, p2, Color.white, null, 2f);
            p0 = p3;
        }

        this.ShowDirections();
    }

    private Vector3 ShowPoint(int index) {
        Vector3 point = this.hTransform.TransformPoint(this.spline.GetControlPoint(index));

        Handles.color = modeColors[(int)this.spline.GetControlPointMode(index)];
        float size = HandleUtility.GetHandleSize(point);

        if (index == 0) {
            size *= 2f;
        }

        //Handles.DotCap
        if (Handles.Button(point, this.hRotation, handleSize * size, pickSize * size, Handles.DotHandleCap)) {
            this.selectedIndex = index;
            this.Repaint();
        }

        // Only show the position of the handle if the point's index matches the selected index
        if (selectedIndex != index) {
            return point;
        }

        EditorGUI.BeginChangeCheck();
        point = Handles.DoPositionHandle(point, this.hRotation);

        if (EditorGUI.EndChangeCheck()) {
            Undo.RecordObject(this.spline, "Move Point");
            EditorUtility.SetDirty(this.spline);
            this.spline.SetControlPoint(index, this.hTransform.InverseTransformPoint(point));
        }

        return point;
    }

    private void ShowDirections() {
        Handles.color = Color.green;
        Vector3 point = this.spline.GetPoint(0f);
        Handles.DrawLine(point, point + this.spline.GetDirection(0f) * directionScale);
        int steps = stepsPerCurve * this.spline.CurveCount;
        for(int i = 1; i <= steps; i++) {
            point = this.spline.GetPoint(i / (float)steps);
            Handles.DrawLine(point, point + this.spline.GetDirection(i / (float)steps) * directionScale);
        }
    }

    public override void OnInspectorGUI() {
        //base.OnInspectorGUI();
        //this.DrawDefaultInspector();
        this.spline = target as BezierSpline;

        EditorGUI.BeginChangeCheck();
        bool loop = EditorGUILayout.Toggle("Loop", this.spline.Loop);

        if (EditorGUI.EndChangeCheck()) {
            Undo.RecordObject(this.spline, "Toogle Loop");
            EditorUtility.SetDirty(this.spline);
            this.spline.Loop = loop;
        }
        
        if (this.selectedIndex >= 0 && this.selectedIndex < this.spline.ControlPointCount) {
            this.DrawSelectedPointInspector();
        }
        
        if (GUILayout.Button("Add Curve")) {
            Undo.RecordObject(this.spline, "Add Curve");
            this.spline.AddCurve();
            EditorUtility.SetDirty(this.spline);
        }
    }

    private void DrawSelectedPointInspector() {
        GUILayout.Label("Selected Point");
        EditorGUI.BeginChangeCheck();
        Vector3 point = EditorGUILayout.Vector3Field("Position", this.spline.GetControlPoint(this.selectedIndex));
        if (EditorGUI.EndChangeCheck()) {
            Undo.RecordObject(this.spline, "Move Point");
            EditorUtility.SetDirty(this.spline);
            this.spline.SetControlPoint(selectedIndex, point);
        }
        EditorGUI.BeginChangeCheck();
        BezierControlPointMode mode = (BezierControlPointMode)EditorGUILayout.EnumPopup("Mode", this.spline.GetControlPointMode(selectedIndex));
        if (EditorGUI.EndChangeCheck()) {
            Undo.RecordObject(this.spline, "Change Point Mode");
            this.spline.SetControlPointMode(selectedIndex, mode);
            EditorUtility.SetDirty(this.spline);
        }
    }

}
