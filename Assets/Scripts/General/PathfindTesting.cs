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
        [SerializeField] private PathfinderMediator pathfinderMediator;
        public GameObject navigatorTransform;
        public Transform targetTransform;

    }
}
