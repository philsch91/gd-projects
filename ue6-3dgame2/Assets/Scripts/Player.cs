using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float forwardForce = 2000f;
    public float sidewaysForce = 500f;
    #pragma warning disable CS0108 // Element blendet vererbte Element aus; fehlendes 'new'-Schlüsselwort
    private Rigidbody rigidbody;
    #pragma warning restore CS0108 // Element blendet vererbte Element aus; fehlendes 'new'-Schlüsselwort
    
    // Start is called before the first frame update
    void Start() {
        this.rigidbody = GetComponent<Rigidbody>();
        //this.rigidbody.AddForce(0, 200, 500);
    }

    // Update is called once per frame
    void Update() {
        //
    }

    private void FixedUpdate() {
        this.rigidbody.AddForce(0, 0, this.forwardForce * Time.deltaTime);

        if (Input.GetKey("d")) {
            this.rigidbody.AddForce(this.sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }

        if (Input.GetKey("a")) {
            this.rigidbody.AddForce(-this.sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }

        if (this.rigidbody.position.y < -1f) {
            GameManager gameManager = FindObjectOfType<GameManager>();
            gameManager.EndGame();
        }
    }
}
