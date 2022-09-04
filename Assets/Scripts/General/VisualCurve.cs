using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class VisualCurve : MonoBehaviour
    {

        public Transform[] CurveControlPoints = new Transform[4];

        private void OnValidate()
        {
            CurveControlPoints = GetComponentsInChildren<Transform>();
        }
        private Vector2[] CurveControlPositions()
        {
            Vector2[] positions = new Vector2[CurveControlPoints.Length];
            for (int i = 0; i < CurveControlPoints.Length; i++)
            {
                positions[i] = CurveControlPoints[i].position;
            }

            return positions;
        }

        private Vector2[] CurveControlPositions(bool reversed)
        {
            Vector2[] positions = new Vector2[CurveControlPoints.Length];
            int index = 0;
            for (int i = CurveControlPoints.Length - 1; i >= 0; i--)
            {
                positions[i] = CurveControlPoints[index].position;
                index++;
            }

            return positions;
        }

        public BezierCurve ToBezierCurve()
        {
            BezierCurve bezierCurve = new BezierCurve(CurveControlPositions());
            return bezierCurve;
        }

        public BezierCurve ToBezierCurve(bool reversed)
        {
            BezierCurve bezierCurve = new BezierCurve(CurveControlPositions(true));
            return bezierCurve;
        }
        public void OnDrawGizmos()
        {
        Gizmos.color = Color.yellow;
        for (float t = 0; t <= 1; t += 0.05f)
        {
            Vector2 position = Mathf.Pow(1 - t, 3) * CurveControlPoints[0].position +
                           3 * Mathf.Pow(1 - t, 2) * t * CurveControlPoints[1].position +
                           3 * (1 - t) * Mathf.Pow(t, 2) * CurveControlPoints[2].position +
                           Mathf.Pow(t, 3) * CurveControlPoints[3].position;

            Gizmos.DrawSphere(position, 0.1f);
        }
        }
    }

