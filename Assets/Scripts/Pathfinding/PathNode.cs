using System;
using UnityEngine;

namespace Pathfinding
{
    [System.Serializable]
    public struct PathNode : IEquatable<PathNode>
    {
        public Vector2 Position { private set; get; }
        public Edge[] Edges { private set; get; }
        public int ID { private set; get; }

        public PathNode(Vector2 position,Edge[] edges)
        {
            Position = position;
            Edges = edges;

            string stringID = Position.x.ToString() + Position.y.ToString();
            ID = -425505606 + stringID.GetHashCode() + Position.GetHashCode(); ;
        }

        public override bool Equals(object obj)
        {
            return obj is PathNode node && Equals(node);
        }
        public bool Equals(PathNode other)
        {
            return Position.Equals(other.Position);
        }

        public override int GetHashCode()
        {
            string stringID = Position.x.ToString() + Position.y.ToString();
            return -425505606 + stringID.GetHashCode()+Position.GetHashCode();
        }


    }
    [System.Serializable]
    public struct Edge
    {
        public int DestinationNodeHashCode { private set; get; }
        public BezierCurve BezierCurve { private set; get; }
        public bool HasCurve { private set; get; }
        public int Weight { private set; get; }

        public Edge(int destinationNodeHashCode, BezierCurve bezierCurve, bool hasCurve,int weight)
        {
            DestinationNodeHashCode = destinationNodeHashCode;
            BezierCurve = bezierCurve;
            HasCurve = hasCurve;
            Weight = weight;
        }
        public Edge(int destinationNodeHashCode, bool hasCurve, int weight)
        {
            DestinationNodeHashCode = destinationNodeHashCode;
            Vector2[] zeroPositions = new Vector2[0];
            BezierCurve = new BezierCurve(zeroPositions);
            HasCurve = hasCurve;
            Weight = weight;
        }
    }

}

