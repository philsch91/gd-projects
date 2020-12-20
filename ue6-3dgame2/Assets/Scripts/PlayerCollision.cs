using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {

    public Player player;
    // Start is called before the first frame update
    void Start() {
        //
    }

    // Update is called once per frame
    void Update() {
        //
    }

    private void OnCollisionEnter(Collision collision) {
        Debug.Log(collision.collider.name);

        if (collision.collider.tag == "Obstacle") {
            Debug.Log("Obstacle");
            this.player.enabled = false;
            GameManager gameManager = FindObjectOfType<GameManager>();
            gameManager.EndGame();
        }
    }
}
