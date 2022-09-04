using UnityEditor;
using UnityEngine;

namespace Pathfinding
{
    [CustomEditor(typeof(GraphConstructor))]
    public class GraphConstructorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GraphConstructor graphConstructor = (GraphConstructor)target;
            if (GUILayout.Button("Create Node GameObjects"))
            {
                graphConstructor.GenerateNodeGameobjects();
            }
            if (GUILayout.Button("Create Node Edges"))
            {
                graphConstructor.GenerateEdges2();
            }
            if (GUILayout.Button("Generate Graph"))
            {
                graphConstructor.GenerateGraph();
            }
        }
    }

    [CustomEditor(typeof(NodeGameobject))]
    public class NodeGameobjectEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            NodeGameobject nodeGameobject = (NodeGameobject)target;
            if (GUILayout.Button("Create Visual Curve"))
            {
                nodeGameobject.AddVisualCurveEdge();
            }
        }
    }

    [CustomEditor(typeof(PathfindTesting))]
    public class PathfindTestEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            PathfindTesting pathfindTesting = (PathfindTesting)target;
            if (GUILayout.Button("GetRandomPath"))
            {
                pathfindTesting.Test();
            }
           
        }
    }

}
