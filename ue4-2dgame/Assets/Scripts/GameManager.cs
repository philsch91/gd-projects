using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    //public GameEnvironment environmentPrefab;
    //private GameEnvironment environmentInstance;

    public bool autoRestart = true;    //false
    public float restartDelay = 1f;
    private bool gameHasEnded = false;

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
        //SceneManager.LoadScene("MainScence");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void RestartGame() {
        //Destroy(environmentInstance.gameObject);
        //SceneManager.LoadScene("MainScence");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        this.BeginGame();
    }

    public void EndGame() {
        if (this.gameHasEnded) {
            return;
        }

        this.gameHasEnded = true;
        Debug.Log("Game Over");

        if (this.autoRestart) {
            Debug.Log("RestartGame");
            //this.RestartGame();
            Invoke("RestartGame", this.restartDelay);
        }
    }
}
