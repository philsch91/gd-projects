using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeGameObject : MonoBehaviour {

    public new GameObject gameObject;

    // Start is called before the first frame update
    void Start() {
        this.gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
    }

    // Update is called once per frame
    void Update() {
        
    }
}
