using System.Collections.Generic;
using UnityEngine;
using Priority_Queue;

namespace Pathfinding
{
    public static class Pathfinder
    {
        public static Path GetPath(Vector2 navigatorPosition, Vector2 targetPosition, Graph graph,Transform navigatorTransform)
        {
            PathNode start = graph.NearestNode(navigatorPosition);
            PathNode goal = graph.NearestNode(targetPosition);
            Stack<Edge> edgePath = AStarPathfinding(start, goal, graph);
            Path path = new Path(edgePath, navigatorTransform, graph);
            return path;
        } 
        
        private static Stack<Edge> AStarPathfinding(PathNode start, PathNode goal, Graph graph)
        {
            Dictionary<int, int> distanceScore = new Dictionary<int, int>();
            Dictionary<int, int> heuristicScore = new Dictionary<int, int>();

            SimplePriorityQueue<int, int> toSearchQueue = new SimplePriorityQueue<int, int>();

            Dictionary<int, int> parentNodes = new Dictionary<int, int>();
            Dictionary<int, Edge> edgeNodes = new Dictionary<int, Edge>();

            heuristicScore[start.ID] = HeuristicValueEuclid(start, goal);
            distanceScore[start.ID] = 0;
            toSearchQueue.Enqueue(start.ID, heuristicScore[start.ID]);

            while (toSearchQueue.Count > 0)
            {
                int currentNodeIndex = toSearchQueue.Dequeue();
                PathNode currentNode = graph.PathNodes[currentNodeIndex];
                if (currentNodeIndex == goal.ID)
                {
                    Stack<Edge> edgePath = GetEdges(parentNodes, edgeNodes, start.ID, goal.ID);
                    return edgePath;
                }

                for (int i = 0; i < currentNode.Edges.Length; i++)
                {
                    PathNode neighborNode = graph.PathNodes[currentNode.Edges[i].DestinationNodeHashCode];
                    int destinationNodeIndex = currentNode.Edges[i].DestinationNodeHashCode;
                    if (!distanceScore.ContainsKey(destinationNodeIndex))
                        distanceScore.Add(destinationNodeIndex, int.MaxValue);

                    int currentScore = distanceScore[currentNodeIndex] + currentNode.Edges[i].Weight;
                    if (currentScore < distanceScore[destinationNodeIndex])
                    {
                        distanceScore[destinationNodeIndex] = currentScore;
                        if (parentNodes.ContainsKey(destinationNodeIndex))
                        {
                            parentNodes[destinationNodeIndex] = currentNodeIndex;
                            edgeNodes[destinationNodeIndex] = currentNode.Edges[i];
                        }
                        else
                        {
                            parentNodes.Add(destinationNodeIndex, currentNodeIndex);
                            edgeNodes.Add(destinationNodeIndex, currentNode.Edges[i]);
                        }

                        int hScore = distanceScore[destinationNodeIndex] + HeuristicValueEuclid(neighborNode, goal);
                        heuristicScore[destinationNodeIndex] = hScore;

                        if (!toSearchQueue.Contains(destinationNodeIndex))
                        {
                            toSearchQueue.Enqueue(destinationNodeIndex, hScore);
                        }
                    }
                }
            }
            Debug.Log("No Path");
            return null;
        }

   
        private static Stack<Edge> GetEdges(Dictionary<int, int> parentNodes, Dictionary<int, Edge> edgeNodes, int start, int goal)
        {
            Stack<Edge> edgePath = new Stack<Edge>();
            int currentNodeHashCode = goal;
            while (currentNodeHashCode!=start)
            {
                edgePath.Push(edgeNodes[currentNodeHashCode]);
                currentNodeHashCode = parentNodes[currentNodeHashCode];
            }
            return edgePath;
        }


        private static int HeuristicValueEuclid(PathNode startNode, PathNode goalNode)
        {
            return (int)Mathf.Sqrt(Mathf.Pow(startNode.Position.x - goalNode.Position.x, 2) + Mathf.Pow(startNode.Position.y - goalNode.Position.y, 2));
        }
       

    }
}
