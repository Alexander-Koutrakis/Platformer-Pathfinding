using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using UnityEngine;
using System.Diagnostics;
using System;
namespace Pathfinding
{
    public class PathfinderMediator
    {
        private Queue<KeyValuePair<Navigator, Vector2>> pathRequests = new Queue<KeyValuePair<Navigator, Vector2>>();
        private Graph graph;
        public static PathfinderMediator Instance { private set; get;}

        private Stopwatch sw=new Stopwatch();

        public PathfinderMediator(Graph graph)
        {
            this.graph = graph;
            Instance = this;
        }

        public void PathResquest(Navigator navigator, Vector2 target)
        {
            Vector2 navigatorPosition = navigator.transform.position;
            Task.Factory.StartNew(() => SendPath(navigator, target, navigatorPosition));
        }
  

        private void SendPath(Navigator navigator, Vector2 targetPosition,Vector2 navigatorPosition)
        {
            Stopwatch newSW = new Stopwatch();
            newSW.Start();
            Path path = Pathfinder.GetPath(navigatorPosition, targetPosition, graph, navigator.Transform);
            navigator.SetPath(path);

            newSW.Stop();
            TimeSpan ts = newSW.Elapsed;
            UnityEngine.Debug.Log("Pathfind " + ts.TotalMilliseconds);
        }    
       
    }
}
