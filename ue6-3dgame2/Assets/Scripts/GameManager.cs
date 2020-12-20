using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public bool autoRestart = true;
    public float restartDelay = 1f;
    private bool gameHasEnded = false;

    // Start is called before the first frame update
    void Start() {
        //
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            this.RestartGame();
        }
    }

    private void StartGame() {
        //
    }

    public void RestartGame() {
        Debug.Log("RestartGame");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        this.StartGame();
    }

    public void EndGame() {
        if (this.gameHasEnded) {
            return;
        }

        this.gameHasEnded = true;
        Debug.Log("Game Over");

        if (this.autoRestart) {
            Invoke("RestartGame", this.restartDelay);
        }
    }


}
