using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    public GameObject obstaclePrefab;
    public int numberOfObstacles = 1000;
    public float groundWidth = 15f;
    public float minZ = 30.0f;
    public float maxZ = 60.0f;

    // Start is called before the first frame update
    void Start() {
        //Vector3 position = new Vector3();
        Vector3 position = obstaclePrefab.transform.position;
        //Vector3 objectSize = obstaclePrefab.GetComponent<Renderer>().bounds.size;
        float maxX = this.groundWidth / 2;
        for (int i = 0; i < this.numberOfObstacles; i++) {
            GameObject obstacle = obstaclePrefab;
            Vector3 obstacleSize = obstacle.GetComponent<Renderer>().bounds.size;

            position.x = Random.Range(-maxX, maxX);
            position.y = 1f;
            position.z += Random.Range(obstacleSize.z * 3, this.maxZ);

            GameObject newObstacle = Instantiate(obstacle, position, Quaternion.identity);
            Vector3 obstacleScale = newObstacle.transform.localScale;
            obstacleScale.x += Random.Range(-0.5f, 0.5f);
            newObstacle.transform.localScale = obstacleScale;
        }
    }

    // Update is called once per frame
    void Update() {
        //
    }
}
