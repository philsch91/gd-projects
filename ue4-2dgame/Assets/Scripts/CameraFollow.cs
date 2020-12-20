using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;    //Player
    public float smoothSpeed = 0.3f;

    private Vector3 currentVelocity;

    // Update is called once per frame
    void LateUpdate() {
        if (target.position.y > transform.position.y) {
            Vector3 newPos = new Vector3(transform.position.x, target.position.y, transform.position.z);
            //transform.position = Vector3.Lerp(transform.position, newPos, this.smoothSpeed);
            //transform.position = Vector3.SmoothDamp(transform.position, newPos, ref currentVelocity, smoothSpeed * Time.deltaTime);
            transform.position = newPos;
        }

        if (target.position.y < transform.position.y) {
            //Debug.Log("below Camera.position.y");
        }

        //Camera camera = GetComponent<Camera>();
        Camera camera = this.transform.GetComponent<Camera>();
        Vector3 screenPos = camera.WorldToScreenPoint(target.position);
        Debug.Log("target is " + screenPos.y);
        if (screenPos.y < 0) {
            //Player player = this.target.GetComponent<Player>();
            //GameManager gameManager = player.gameManager;
            GameManager gameManager = FindObjectOfType<GameManager>();
            gameManager.EndGame();
        }
    }
}
