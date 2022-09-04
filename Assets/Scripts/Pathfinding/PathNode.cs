using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    [System.Serializable]
    public struct PathNode : IEquatable<PathNode>
    {

        public Vector2 Position;
        public Edge[] Edges;

        public int ID;
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
        public int DestinationNodeHashCode;
        public BezierCurve BezierCurve;
        public bool HasCurve;
        public int Weight;
    }

}

