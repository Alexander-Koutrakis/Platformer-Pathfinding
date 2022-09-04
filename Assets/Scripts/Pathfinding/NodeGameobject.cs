using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class NodeGameobject : MonoBehaviour
{
    public List<EdgeData> EdgesData=new List<EdgeData>();
    public BezierCurve GetCurve(EdgeData edgeData)
    {
        if (edgeData.VisualCurve != null)
        {

            if (edgeData.ReverseVisualCurve)
            {
                return edgeData.VisualCurve.ToBezierCurve(true);
            }
            else
            {
                return edgeData.VisualCurve.ToBezierCurve();
            }
            
        }

        Vector2 startPos= transform.position;
        Vector2 secondControl;
        Vector2 thirdControl;
        Vector2 endPos = edgeData.NodeConnected.transform.position;
        if (startPos.y> endPos.y)
        {
            float posX1 =startPos.x;
            float posY1 = startPos.y + 1.5f;

            float posX2 = endPos.x;
            float posY2= startPos.y + 2f;

            secondControl = new Vector2(posX1, posY1);
            thirdControl = new Vector2(posX2, posY2);
        }
        else if(startPos.y < endPos.y)
        {
            float posX1 = Mathf.Lerp(startPos.x, endPos.x, 0.25f);
            float posY1 = endPos.y + 1;

            float posX2 = Mathf.Lerp(startPos.x, endPos.x, 0.75f);
            float posY2 = endPos.y + 2;

            secondControl = new Vector2(posX1, posY1);
            thirdControl = new Vector2(posX2, posY2);
        }
        else
        {
            float posX1 = startPos.x;
            float posY1 = startPos.y + 2;

            float posX2 = endPos.x;
            float posY2 = endPos.y + 2;

            secondControl = new Vector2(posX1, posY1);
            thirdControl = new Vector2(posX2, posY2);
        }
        BezierCurve bezierCurve = new BezierCurve(new Vector2[] { transform.position, secondControl, thirdControl, edgeData.NodeConnected.transform.position });
        return bezierCurve;
    }
    
    public void AddVisualCurveEdge()
    {
        GameObject visualCurve = new GameObject();        
        GameObject controlPoint0 = new GameObject();
        GameObject controlPoint1 = new GameObject();
        GameObject controlPoint2 = new GameObject();
       

        controlPoint0.transform.SetParent(visualCurve.transform);
        controlPoint1.transform.SetParent(visualCurve.transform);
        controlPoint2.transform.SetParent(visualCurve.transform);
        visualCurve.transform.SetParent(transform);

        visualCurve.transform.position = transform.position;
        controlPoint0.transform.position = transform.position;
        controlPoint1.transform.position = transform.position;
        controlPoint2.transform.position = transform.position;

        VisualCurve curve = visualCurve.AddComponent<VisualCurve>();
        curve.CurveControlPoints[0] = visualCurve.transform;
        curve.CurveControlPoints[1] = controlPoint0.transform;
        curve.CurveControlPoints[2] = controlPoint1.transform;
        curve.CurveControlPoints[3] = controlPoint2.transform;

        visualCurve.gameObject.name = "Visual Curve Start";
        controlPoint0.gameObject.name = "controlPoint0";
        controlPoint1.gameObject.name = "controlPoint1";
        controlPoint2.gameObject.name = "Visual Curve End";
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = new Color(0.5f, 0.35f, 0.35f);
        if (EdgesData != null)
        {
            for (int i = 0; i < EdgesData.Count; i++)
            {
                if (EdgesData[i].EdgeType == EdgeType.Move)
                {
                    Gizmos.DrawLine(transform.position, EdgesData[i].NodeConnected.transform.position);
                }
                else if (EdgesData[i].EdgeType == EdgeType.Jump)
                {
                    for (float j = 0; j < 1; j += 0.01f)
                    {
                        Gizmos.DrawSphere(GetCurve(EdgesData[i]).BezierCurvePoint(j), 0.1f);
                    }
                }
            }
        }
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.5f);
    }

}

[System.Serializable]
public struct EdgeData {
    public NodeGameobject NodeConnected;
    public int Weight;
    public EdgeType EdgeType;
    public VisualCurve VisualCurve;
    public bool ReverseVisualCurve;
}

public enum EdgeType { Move,Jump}


