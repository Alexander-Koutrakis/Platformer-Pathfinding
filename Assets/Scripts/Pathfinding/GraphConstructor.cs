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
            // EditorCoroutines.Execute(GenerateEdges());
            NodeGameobject[] nodeGameobjects = GetComponentsInChildren<NodeGameobject>();
            int index = 0;
            foreach (NodeGameobject nodeGameobject in nodeGameobjects)
            {
                index++;

                Debug.Log("Creating Edges " + index + " / " + nodeGameobjects.Length);
                CreateEdges(nodeGameobject, nodeGameobjects);

            }
        }

        private IEnumerator GenerateEdges()
        {
            NodeGameobject[] nodeGameobjects = GetComponentsInChildren<NodeGameobject>();
            int index = 0;
            foreach (NodeGameobject nodeGameobject in nodeGameobjects)
            {
                index++;

                Debug.Log("Creating Edges " + index+" / "+ nodeGameobjects.Length);
                CreateEdges(nodeGameobject, nodeGameobjects);
                yield return null;
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

        private void GenerateLargeNodeGameobject()
        {
            GameObject emptyGO = new GameObject();
            List<NodeGameobject> nodeGameobjects = new List<NodeGameobject>();
            for (int x = 0; x <= 150; x += 2)
            {
                for(int y = 0; y <= 150; y += 2)
                {
                    Vector2 position = new Vector2(x, y);
                    GameObject nodeGameobjectPrefab = Instantiate(emptyGO, position, Quaternion.identity, transform);
                    NodeGameobject nodeGameobject = nodeGameobjectPrefab.AddComponent<NodeGameobject>();
                    nodeGameobjects.Add(nodeGameobject);
                    nodeGameobjectPrefab.name = "PathfindNode " + nodeGameobjectPrefab.transform.position;
                }
            }
        }
        private List<PathNode> GeneratePathNodes()
        {
            Dictionary<NodeGameobject, PathNode> nodeDictionary = new Dictionary<NodeGameobject, PathNode>();
            List<PathNode> nodes = new List<PathNode>();
            NodeGameobject[] nodeGameobjects = GetComponentsInChildren<NodeGameobject>();
            //Create PathfindNodes
            for (int i = 0; i < nodeGameobjects.Length; i++)
            {
                PathNode newNode = new PathNode();
                newNode.Position = nodeGameobjects[i].transform.position;
                nodeDictionary.Add(nodeGameobjects[i], newNode);
            }

            foreach(NodeGameobject nodeGameobject in nodeDictionary.Keys)
            {
                PathNode newNode = nodeDictionary[nodeGameobject];
                newNode.Edges = new Edge[nodeGameobject.EdgesData.Count];
                for(int i = 0; i < newNode.Edges.Length; i++)
                {
                    if (nodeGameobject.EdgesData[i].EdgeType == EdgeType.Jump)
                    {
                        Edge newEdge = new Edge();
                        newEdge.BezierCurve= nodeGameobject.GetCurve(nodeGameobject.EdgesData[i]);
                        newEdge.DestinationNodeHashCode= PositionToHash(nodeGameobject.EdgesData[i].NodeConnected.transform.position);
                        newEdge.HasCurve = true;
                        newEdge.Weight = 3;

                        newNode.Edges[i] = newEdge;
                    }
                    else
                    {
                        Edge newEdge = new Edge();
                        newEdge.DestinationNodeHashCode = PositionToHash(nodeGameobject.EdgesData[i].NodeConnected.transform.position);
                        newEdge.HasCurve = false;
                        newEdge.Weight = 2;

                        newNode.Edges[i] = newEdge;
                    }

                }
                nodes.Add(newNode);
            }

            return nodes;
        }
        public void GenerateGraph()
        {
            Graph graph = ScriptableObject.CreateInstance<Graph>();
            List<PathNode> pathNodes = GeneratePathNodes();
            for(int i = 0; i < pathNodes.Count; i++)
            {
                PathNode pathNode = pathNodes[i];
                pathNode.ID = pathNodes[i].GetHashCode();
                graph.PathNodes.Add(pathNode.ID, pathNode);
            }
            
            AssetDatabase.CreateAsset(graph, Path);
            AssetDatabase.SaveAssets();
        }

        public Graph NewGraph()
        {
            Graph graph = ScriptableObject.CreateInstance<Graph>();
            List<PathNode> pathNodes = GeneratePathNodes();
            for (int i = 0; i < pathNodes.Count; i++)
            {
                PathNode pathNode = pathNodes[i];
                pathNode.ID = pathNodes[i].GetHashCode();
                graph.PathNodes.Add(pathNode.ID, pathNode);
            }
            return graph;
        }

        private int PositionToHash(Vector2 position)
        {
            string stringID = position.x.ToString() + position.y.ToString();
            return -425505606 + stringID.GetHashCode()+position.GetHashCode();
        }

    }
}
