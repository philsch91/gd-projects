using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsText : MonoBehaviour {
    
    public Text pointsText;

    public void SetPoints(int points) {
        this.pointsText.text = points.ToString();
    }
}
