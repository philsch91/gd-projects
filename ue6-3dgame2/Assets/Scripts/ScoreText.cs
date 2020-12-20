using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour {

    public Transform player;
    public Text scoreText;
    // Start is called before the first frame update
    void Start() {
        //
    }

    // Update is called once per frame
    void Update() {
        this.scoreText.text = player.position.z.ToString("0");
    }
}
