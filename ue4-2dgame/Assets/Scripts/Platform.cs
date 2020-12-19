using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

    public float jumpForce = 10f;
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.relativeVelocity.y > 0f) {
            return;
        }
        
        Rigidbody2D rigidBody = collision.collider.GetComponent<Rigidbody2D>();
        
        if (rigidBody == null) {
            return;
        }

        Vector2 velocity = rigidBody.velocity;
        velocity.y = jumpForce;
        rigidBody.velocity = velocity;
    }
}
