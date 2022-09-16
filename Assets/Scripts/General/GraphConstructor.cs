using UnityEditor;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

namespace Pathfinding
{
 
    [ExecuteInEditMode]
    public class GraphConstructor : MonoBehaviour
    {
        public string Path;
       
        private void CreateEdges(NodeGameobject targetNodeGameobject, NodeGameobject[] nodeGameobjects)
        {                      
            float connectionDistance = 2;
            foreach(NodeGameobject nodeGameobject in nodeGameobjects)
            {
                
                if (nodeGameobject != targetNodeGameobject)
                {
                    if (Vector2.Distance(nodeGameobject.transform.position, targetNodeGameobject.transform.position) <= connectionDistance + 0.1f)
                    {
                        EdgeData edgeData = new EdgeData();
                        edgeData.NodeConnected = nodeGameobject;
                        edgeData.EdgeType = EdgeType.Move;
                        edgeData.Weight = 2;
                        targetNodeGameobject.EdgesData.Add(edgeData);
                    }
                }
            }
           
        }

        public void GenerateNodeGameobjects()
        {
            GenerateGroundNodeGameObjects();
        }

        public void GenerateEdges2()
        {
            NodeGameobject[] nodeGameobjects = GetComponentsInChildren<NodeGameobject>();
            int index = 0;
            foreach (NodeGameobject nodeGameobject in nodeGameobjects)
            {
                index++;

                CreateEdges(nodeGameobject, nodeGameobjects);

            }
        }
        private void GenerateGroundNodeGameObjects()
        {
            Tilemap tilemap = GameObject.FindObjectOfType<Tilemap>();
            Vector2[] positions = tilemap.GroundedTilePos();
            NodeGameobject[] nodeGameobjects = new NodeGameobject[positions.Length];
            GameObject emptyGO = new GameObject();
            for (int i = 0; i < positions.Length; i++)
            {
                GameObject nodeGameobjectPrefab = Instantiate(emptyGO, positions[i], Quaternion.identity, transform);
                NodeGameobject nodeGameobject = nodeGameobjectPrefab.AddComponent<NodeGameobject>();
                nodeGameobjects[i] = nodeGameobject;
                nodeGameobjectPrefab.name = "PathfindNode " + positions[i];
            }
            DestroyImmediate(emptyGO);
        }

        private PathNode[] GeneratePathNodes()
        {
            Dictionary<NodeGameobject, PathNode> nodeDictionary = new Dictionary<NodeGameobject, PathNode>();           
            NodeGameobject[] nodeGameobjects = GetComponentsInChildren<NodeGameobject>();
            PathNode[] nodes = new PathNode[nodeGameobjects.Length];

            for (int i = 0; i < nodeGameobjects.Length; i++)
            {
                Vector2 position = nodeGameobjects[i].transform.position;
                Edge[] edges = new Edge[nodeGameobjects[i].EdgesData.Count];
                for (int j = 0; j < edges.Length; j++)
                {
                    if (nodeGameobjects[i].EdgesData[j].EdgeType == EdgeType.Jump)
                    {
                        int destinationNodeHashCode = PositionToHash(nodeGameobjects[i].EdgesData[j].NodeConnected.transform.position);
                        BezierCurve bezierCurve = nodeGameobjects[i].GetCurve(nodeGameobjects[i].EdgesData[j]);
                        bool hasCurve = true;
                        int weight = nodeGameobjects[i].EdgesData[j].Weight;
                        Edge newEdge = new Edge(destinationNodeHashCode, bezierCurve, hasCurve, weight);
                        edges[j] = newEdge;
                    }
                    else
                    {
                        int destinationNodeHashCode = PositionToHash(nodeGameobjects[i].EdgesData[j].NodeConnected.transform.position);
                        bool hasCurve = false;
                        int weight = nodeGameobjects[i].EdgesData[j].Weight;
                        Edge newEdge = new Edge(destinationNodeHashCode, hasCurve, weight);
                        edges[j] = newEdge;
                    }
                }

                nodes[i] = new PathNode(position, edges);
                
            }
     
            return nodes;
        }
        public void GenerateGraph()
        {
            Graph graph = ScriptableObject.CreateInstance<Graph>();
            PathNode[] serializablePathnodes = GeneratePathNodes();
            graph.SerializeGraph(serializablePathnodes);
            AssetDatabase.CreateAsset(graph, Path);
            AssetDatabase.SaveAssets();
        }
      
        private int PositionToHash(Vector2 position)
        {
            string stringID = position.x.ToString() + position.y.ToString();
            return -425505606 + stringID.GetHashCode()+position.GetHashCode();
        }

    }
}
