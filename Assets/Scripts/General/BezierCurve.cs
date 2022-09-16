using UnityEngine;

[System.Serializable]
public struct BezierCurve {
    private Vector2[] ControlPositions;   
    public BezierCurve(Vector2[] controlPositions)
    {
        this.ControlPositions = controlPositions;
    }

    public Vector2 BezierCurvePoint(float t)
    {
       Vector2 position=Mathf.Pow(1 - t, 3) * ControlPositions[0] + 
                        3 * Mathf.Pow(1 - t, 2) * t * ControlPositions[1] + 
                        3 * (1 - t) * Mathf.Pow(t, 2) * ControlPositions[2] + 
                        Mathf.Pow(t, 3) * ControlPositions[3];
        return position;
    }

}
  
