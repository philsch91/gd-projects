using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCell : MonoBehaviour {

    public IntVector2 coordinates;
    private MazeCellEdge[] edges = new MazeCellEdge[MazeDirections.Count];
    private int initializedEdgeCount;

    public bool IsFullyInitialized {
        get {
            return initializedEdgeCount == MazeDirections.Count;
        }
    }

    public MazeDirection RandomUnitializedDirection {
        get {
            int skips = Random.Range(0, MazeDirections.Count - this.initializedEdgeCount);
            for (int i = 0; i < MazeDirections.Count; i++) {
                if (this.edges[i] == null) {
                    if (skips == 0) {
                        return (MazeDirection)i;
                    }
                    skips--;
                }
            }
            throw new System.InvalidOperationException("MazeCell has no unitialized directions left");
        }
    }

    public MazeCellEdge GetEdge (MazeDirection direction) {
        return this.edges[(int)direction];
    }

    public void SetEdge(MazeDirection direction, MazeCellEdge edge) {
        this.edges[(int)direction] = edge;
        this.initializedEdgeCount++;
    }

    // Start is called before the first frame update
    void Start() {
        //
    }

    // Update is called once per frame
    void Update() {
        //
    }
}
