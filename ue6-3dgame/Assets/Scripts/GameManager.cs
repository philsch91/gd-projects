using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Maze mazePrefab;
    private Maze mazeInstance;
    // Start is called before the first frame update
    void Start() {
        this.beginGame();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            this.restartGame();
        }
    }

    private void beginGame() {
        Debug.Log(string.Format("beginGame()"));
        this.mazeInstance = Instantiate(mazePrefab) as Maze;
        //this.mazeInstance.Generate();
        this.StartCoroutine(this.mazeInstance.GenerateWithDelay());
    }

    private void restartGame() {
        this.StopAllCoroutines();
        Debug.Log(string.Format("restartGame()"));
        Destroy(this.mazeInstance.gameObject);
        this.beginGame();
    }
}
