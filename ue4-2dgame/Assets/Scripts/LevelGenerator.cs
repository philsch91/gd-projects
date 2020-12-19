using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    public GameObject platformPrefab;
    public GameObject superPlatformPrefab;
    public int numberOfPlatforms = 200;
    public float levelWidth = 3f;
    public float minY = 0.2f;
    public float maxY = 1.5f;

    // Start is called before the first frame update
    void Start() {
        Vector3 spawnPosition = new Vector3();
        for (int i = 0; i < this.numberOfPlatforms; i++) {
            GameObject gameObject = platformPrefab;
            if ((i % 10) == 0) {
                gameObject = superPlatformPrefab;
            }

            Vector3 objectSize = gameObject.GetComponent<Renderer>().bounds.size;

            spawnPosition.x = Random.Range(-this.levelWidth, this.levelWidth);
            //spawnPosition.y += Random.Range(minY, maxY);
            spawnPosition.y += Random.Range(objectSize.y, maxY);

            Instantiate(gameObject, spawnPosition, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update() {
        //
    }
}
