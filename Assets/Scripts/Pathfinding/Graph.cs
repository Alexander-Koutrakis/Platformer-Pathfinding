using UnityEngine;
using System.Collections.Generic;
namespace Pathfinding
{
    [System.Serializable]
    [CreateAssetMenu]
    public class Graph:ScriptableObject
    {
        public Dictionary<int, PathNode> PathNodes { private set; get; }
        private PathNode[] serializedPathNodes;
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

        public void Initialize()
        {
            PathNodes = new Dictionary<int, PathNode>();
            for (int i = 0; i < serializedPathNodes.Length; i++)
            {
                PathNodes.Add(serializedPathNodes[i].ID, serializedPathNodes[i]);
            }
        }

        public void SerializeGraph(PathNode[] pathNodesToSerialize)
        {
            serializedPathNodes = pathNodesToSerialize;
        }
      
        public void DebugGraph()
        {
            foreach(PathNode pathNode in PathNodes.Values)
            {
                foreach(Edge edge in pathNode.Edges)
                {
                    Debug.DrawLine(pathNode.Position, PathNodes[edge.DestinationNodeHashCode].Position, Color.red, 30);
                }
                Debug.DrawRay(pathNode.Position, Vector2.up, Color.blue, 10);
            }
        }
    }


}
