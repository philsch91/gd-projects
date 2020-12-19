using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    //public GameEnvironment environmentPrefab;
    //private GameEnvironment environmentInstance;

    // Start is called before the first frame update
    void Start() {
        this.BeginGame();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            this.RestartGame();
        }
    }

    private void BeginGame() {
        //this.environmentInstance = Instantiate(environmentPrefab) as GameEnvironment;
    }

    private void RestartGame() {
        //Destroy(environmentInstance.gameObject);
        this.BeginGame();
    }
}
