using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour {

    public int points = 0;
    public float movementSpeed = 10f;
    public PointsText pointsText;
    public GameManager gameManager;
    
    private Rigidbody2D rigidBody;
    private float movement = 0f;
    private SpriteRenderer spriteRenderer;

    private void Awake() {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start() {
        this.rigidBody = GetComponent<Rigidbody2D>();
        this.gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update() {
        this.movement = Input.GetAxis("Horizontal") * this.movementSpeed;
        //Debug.Log(this.movement);
        if (this.movement < 0) {
            this.spriteRenderer.flipX = true;
        } else if (this.movement > 0) {
            this.spriteRenderer.flipX = false;
        }
    }

    private void FixedUpdate() {
        Vector2 velocity = this.rigidBody.velocity;
        velocity.x = movement;
        this.rigidBody.velocity = velocity;

        int posY = (int)transform.position.y;
        if (posY > this.points) {
            this.points = posY;
            pointsText.SetPoints(this.points);
        }

        //GameManager gameManager = FindObjectOfType<GameManager>();
    }
}
