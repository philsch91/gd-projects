using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineObject : MonoBehaviour {

    public BezierSpline spline;
    public SplineMoveMode mode;
    public float duration;
    public bool lookForward;
    public Material material;

    private float progress;
    private bool moveForward = true;    // moveForward = false is backwards
    private Color color = Color.green;

    // Start is called before the first frame update
    void Start() {
        Renderer renderer = this.GetComponent<Renderer>();
        this.material = renderer.material;
        //this.material = Resources.Load("materials/red", typeof(Material)) as Material;
        this.material.SetColor(UnityConstants.UNITY_MATERIAL_COLOR, this.color);
        //this.material.SetTexture("_Texture",Texture.)
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

    private void OnCollisionEnter(Collision collision) {
        Debug.Log(string.Format("{0} collision with object {1}", this.gameObject.name, collision.gameObject.name));
    }

    private void OnMouseOver() {
        this.material.color = Color.yellow;
    }

    private void OnMouseExit() {
        this.material.color = this.color;
    }
}
