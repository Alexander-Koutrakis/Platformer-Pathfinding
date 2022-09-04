using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BezierCurve {
    private Vector2[] controlPositions;
    public Vector2[] ControlPositions { get { return this.controlPositions; } }
    public BezierCurve(Vector2[] controlPositions)
    {
        this.controlPositions = controlPositions;
    }

    public Vector2 BezierCurvePoint(float t)
    {
       Vector2 position=Mathf.Pow(1 - t, 3) * controlPositions[0] + 
                        3 * Mathf.Pow(1 - t, 2) * t * controlPositions[1] + 
                        3 * (1 - t) * Mathf.Pow(t, 2) * controlPositions[2] + 
                        Mathf.Pow(t, 3) * controlPositions[3];
        return position;
    }

}
  
