using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour {

    public int points = 0;
    public float movementSpeed = 10f;
    public PointsText pointsText;
    
    private Rigidbody2D rigidBody;
    private float movement = 0f;

    // Start is called before the first frame update
    void Start() {
        this.rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        this.movement = Input.GetAxis("Horizontal") * this.movementSpeed;
    }

    private void FixedUpdate() {
        Vector2 velocity = this.rigidBody.velocity;
        velocity.x = movement;
        this.rigidBody.velocity = velocity;

        float posY = transform.position.y;
        pointsText.SetPoints((int)posY);
    }
}
