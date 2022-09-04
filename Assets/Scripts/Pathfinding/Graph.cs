using UnityEngine;
using System.Collections.Generic;
namespace Pathfinding
{
    [CreateAssetMenu]
    public class Graph:ScriptableObject
    {
        public Dictionary<int, PathNode> PathNodes = new Dictionary<int, PathNode>();
        public PathNode NearestNode(Vector2 position)
        {
            float minDistance = float.MaxValue;
            PathNode currentNode=new PathNode();
            foreach (PathNode pathNode in PathNodes.Values)
            {
                float distance = Vector2.Distance(pathNode.Position, position);
                if (distance < minDistance)
                {
                    currentNode = pathNode;
                    minDistance = distance;
                }
            }

            return currentNode;
        }

        public void DebugGraph()
        {
            foreach(PathNode pathNode in PathNodes.Values)
            {
                foreach(Edge edge in pathNode.Edges)
                {
                    Debug.DrawLine(pathNode.Position, PathNodes[edge.DestinationNodeHashCode].Position, Color.red, 30);
                }
                Debug.DrawRay(pathNode.Position, Vector2.up, Color.blue, 30);
            }
        }
    }


}
