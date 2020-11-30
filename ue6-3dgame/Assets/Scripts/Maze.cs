using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour {

    //public int sizeX, sizeZ;
    public IntVector2 size;
    public MazeCell cellPrefab;
    public MazePassage passagePrefab;
    public MazeWall wallPrefab;
    public float generationStepDelay;
    private MazeCell[,] cells;

    public IntVector2 RandomCoordinates {
        get {
            return new IntVector2(Random.Range(0, this.size.x), Random.Range(0, this.size.z));
        }
    }

    public bool ContainsCoordinates (IntVector2 coordinate) {
        return coordinate.x >= 0 && coordinate.x < this.size.x && coordinate.z >= 0 && coordinate.z < this.size.z;
    }

    // Start is called before the first frame update
    void Start() {
        //
    }

    // Update is called once per frame
    void Update() {
        //
    }

    public MazeCell GetCell(IntVector2 coordinates) {
        return this.cells[coordinates.x, coordinates.z];
    }

    public void Generate() {
        this.cells = new MazeCell[this.size.x, this.size.z];
        for (int x = 0; x < this.size.x; x++) {
            for (int z = 0; z < this.size.z; z++) {
                this.CreateCell(x, z);
            }
        }
    }

    public IEnumerator GenerateWithDelay() {
        WaitForSeconds delay = new WaitForSeconds(this.generationStepDelay);
        this.cells = new MazeCell[this.size.x, this.size.z];
        /*
        for (int x = 0; x < this.size.x; x++) {
            for (int z = 0; z < this.size.z; z++) {
                yield return delay;
                this.CreateCell(x, z);
            }
        } */

        List<MazeCell> activeCells = new List<MazeCell>();
        this.DoFirstGenerationStep(activeCells);
        while (activeCells.Count > 0) {
            yield return delay;
            this.DoNextGenerationStep(activeCells);
        }
        /*
        IntVector2 coordinates = this.RandomCoordinates;

        while (this.ContainsCoordinates(coordinates) && this.GetCell(coordinates) == null) {
            yield return delay;
            this.CreateCell(coordinates);
            coordinates += MazeDirections.RandomValue.ConvertToIntVector2();
        }
        */
    }

    private void DoFirstGenerationStep(List<MazeCell> activeCells) {
        MazeCell randomCell = this.CreateCell(RandomCoordinates);
        activeCells.Add(randomCell);
    }

    private void DoNextGenerationStep(List<MazeCell> activeCells) {
        int index = activeCells.Count - 1;
        MazeCell cell = activeCells[index];

        if (cell.IsFullyInitialized) {
            activeCells.RemoveAt(index);
            return;
        }

        //MazeDirection direction = MazeDirections.RandomValue;
        MazeDirection direction = cell.RandomUnitializedDirection;
        IntVector2 coordinates = cell.coordinates + direction.ConvertToIntVector2();
        
        if (this.ContainsCoordinates(coordinates) /*&& this.GetCell(coordinates) == null*/) {
            //activeCells.Add(this.CreateCell(coordinates));
            MazeCell neighbor = this.GetCell(coordinates);
            if (neighbor == null) {
                neighbor = this.CreateCell(coordinates);
                this.CreatePassage(cell, neighbor, direction);
                activeCells.Add(neighbor);
                
            } else {
                this.CreateWall(cell, neighbor, direction);
                //activeCells.RemoveAt(index);  //remove not needed
            }
            
        } else {
            this.CreateWall(cell, null, direction);
            //activeCells.RemoveAt(index);  //remote not needed
        }
    }

    private MazeCell CreateCell(int x, int z) {
        MazeCell cell = Instantiate(cellPrefab) as MazeCell;
        this.cells[x, z] = cell;
        cell.name = "Maze Cell " + x + ", " + z;
        cell.transform.parent = transform;
        cell.transform.localPosition = new Vector3(x - this.size.x * 0.5f + 0.5f, 0f, z - this.size.z * 0.5f + 0.5f);
        return cell;
    }

    private MazeCell CreateCell(IntVector2 coordinates) {
        MazeCell cell = Instantiate(cellPrefab) as MazeCell;
        this.cells[coordinates.x, coordinates.z] = cell;
        cell.coordinates = coordinates;
        cell.name = "Maze Cell " + coordinates.x + ", " + coordinates.z;
        cell.transform.parent = transform;
        cell.transform.localPosition = new Vector3(coordinates.x - this.size.x * 0.5f + 0.5f, 0f, coordinates.z - this.size.z * 0.5f + 0.5f);
        return cell;
    }

    private void CreatePassage(MazeCell cell, MazeCell otherCell, MazeDirection direction) {
        MazePassage passage = Instantiate(passagePrefab) as MazePassage;
        passage.Initialize(cell, otherCell, direction);
        passage = Instantiate(passagePrefab) as MazePassage;
        passage.Initialize(otherCell, cell, direction.GetOpposite());
    }

    private void CreateWall(MazeCell cell, MazeCell otherCell, MazeDirection direction) {
        MazeWall wall = Instantiate(wallPrefab) as MazeWall;
        wall.Initialize(cell, otherCell, direction);
        
        if (otherCell != null) {
            wall = Instantiate(wallPrefab) as MazeWall;
            wall.Initialize(otherCell, cell, direction.GetOpposite());
        }
    }
}
