using System;
using System.Diagnostics;
using UnityEngine;
using System.Collections.Generic;

namespace Pathfinding
{
    public class PathfindTesting:MonoBehaviour
    {
        [SerializeField]private Graph graph;
        private float averagePathLength=0;
        [SerializeField]private GraphConstructor graphConstructor;
        public GameObject navigatorTransform;
        public Transform targetTransform;

        public void Test()
        {
            graph = graphConstructor.NewGraph();
            PathfinderMediator pathfinderMediator = new PathfinderMediator(graph);
            CreateNavigators();
            MultipleNavigatortest();
        }

        private void CreateNavigators()
        {          
            for(int i = 0; i < 10; i++)
            {
                GameObject newNavigator = Instantiate(navigatorTransform, transform);
                newNavigator.name = "Navigator " + i;
            }
        }

        private void MultipleNavigatortest()
        {
            Navigator[] navigators = GetComponentsInChildren<Navigator>();
            foreach(Navigator navigator in navigators)
            {
                navigator.transform.position = RandomPosition();
                navigator.SetTarget(targetTransform);
            }
        }

        private void MultipleTests()
        {
            averagePathLength = 0;
            double total = 0;
            int intervals = 10000;
            for (int i = 0; i < intervals; i++)
            {
                total += TestDuration();
            }
            total = total / intervals;
            averagePathLength = averagePathLength / intervals;
            UnityEngine.Debug.Log("Average Test " + total+" from Total "+intervals);
            UnityEngine.Debug.Log("Average Path length " + averagePathLength);
            UnityEngine.Debug.Log("Average Path per node " + total / averagePathLength);
        }
    
        private double TestDuration()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            navigatorTransform.transform.position = RandomPosition();
            targetTransform.position = RandomPosition();            
            Path path = Pathfinder.GetPath(navigatorTransform.transform.position, targetTransform.position, graph, navigatorTransform.transform);
            averagePathLength += path.Length;
            sw.Stop();
            TimeSpan ts = sw.Elapsed;
            return ts.TotalMilliseconds;
        }

       
        private Vector2 RandomPosition()
        {
            List<Vector2> positions = new List<Vector2>();
            foreach(PathNode pathNode in graph.PathNodes.Values)
            {
                positions.Add(pathNode.Position);
            }
            int randomIndex = UnityEngine.Random.Range(0, positions.Count-1);
            return positions[randomIndex];
        }
    }
}
