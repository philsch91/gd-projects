using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineObject : MonoBehaviour {

    public BezierSpline spline;
    public SplineMoveMode mode;
    public float duration;
    public bool lookForward;

    private float progress;
    private bool moveForward = true;    // moveForward = false is backwards

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    private void Update() {
        if (this.moveForward) {
            this.progress += Time.deltaTime / this.duration;
            
            if (this.progress > 1f) {
                if (this.mode == SplineMoveMode.Once) {
                    this.progress = 1f;
                } else if (this.mode == SplineMoveMode.Loop) {
                    this.progress -= 1f;
                } else {
                    this.progress = 2f - progress;
                    this.moveForward = false;
                }
            }
        } else {
            // move backwards
            this.progress -= Time.deltaTime / this.duration;
            
            if (this.progress < 0f) {
                this.progress = -this.progress;
                this.moveForward = true;
            }
        }

        Vector3 position = this.spline.GetPoint(this.progress);
        this.transform.localPosition = position;
        
        if (this.lookForward) {
            this.transform.LookAt(position + this.spline.GetDirection(this.progress));
        }
    }
}
