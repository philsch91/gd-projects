using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour {

    public int sizeX, sizeZ;
    public MazeCell cellPrefab;
    public float generationStepDelay;
    private MazeCell[,] cells;
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void Generate() {
        this.cells = new MazeCell[this.sizeX, this.sizeZ];
        for (int x = 0; x < this.sizeX; x++) {
            for (int z = 0; z < this.sizeZ; z++) {
                this.CreateCell(x, z);
            }
        }
    }

    public IEnumerator GenerateWithDelay() {
        WaitForSeconds delay = new WaitForSeconds(this.generationStepDelay);
        this.cells = new MazeCell[this.sizeX, this.sizeZ];
        for (int x = 0; x < this.sizeX; x++) {
            for (int z = 0; z < this.sizeZ; z++) {
                yield return delay;
                this.CreateCell(x, z);
            }
        }
    }

    private void CreateCell(int x, int z) {
        MazeCell cell = Instantiate(cellPrefab) as MazeCell;
        this.cells[x, z] = cell;
        cell.name = "Maze Cell " + x + ", " + z;
        cell.transform.parent = transform;
        cell.transform.localPosition = new Vector3(x - this.sizeX * 0.5f + 0.5f, 0f, z - this.sizeZ * 0.5f + 0.5f);
    }
}
