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
        private bool calculatingPath = false;
        private bool answeringRequests = false;
        public static PathfinderMediator Instance { private set; get;}
        private Thread pathfindingThread;

        private Stopwatch sw=new Stopwatch();

        public PathfinderMediator(Graph graph)
        {
            this.graph = graph;
            Instance = this;
            pathfindingThread = new Thread(new ThreadStart(AnswerRequests2));
        }

        public void PathResquest(Navigator navigator, Vector2 target)
        {
            KeyValuePair<Navigator, Vector2> request = new KeyValuePair<Navigator, Vector2>(navigator,target);
            if (!pathRequests.Contains(request))
            {
                pathRequests.Enqueue(request);
                if (!answeringRequests)
                {
                    AnswerRequests2();
                }
            }
        }
        private async Task SendPath(Navigator navigator,Vector2 targetPosition)
        {
            Stopwatch spSW = new Stopwatch();
            spSW.Start();

            calculatingPath = true;           
            Vector2 position = navigator.Transform.position;
            Path path = await Task.Run(() => Pathfinder.GetPath(position, targetPosition, graph, navigator.Transform));            
            navigator.SetPath(path);
            calculatingPath = false;

            spSW.Stop();
            TimeSpan timeSpan = spSW.Elapsed;
            UnityEngine.Debug.Log("Get path for Navigator "+navigator.name+" is " + timeSpan.TotalMilliseconds);
           

        }

        private void Sendpath(Navigator navigator, Vector2 targetPosition,Vector2 navigatorPosition)
        {
            Stopwatch spSW = new Stopwatch();
            spSW.Start();

            calculatingPath = true;

            Path path = Pathfinder.GetPath(navigatorPosition, targetPosition, graph, navigator.Transform);
            navigator.SetPath(path);

            spSW.Stop();
            TimeSpan timeSpan = spSW.Elapsed;
            UnityEngine.Debug.Log("Timing " + timeSpan.TotalMilliseconds);
        }

        private async void AnswerRequests2()
        {
            answeringRequests = true;
            while (pathRequests.Count > 0)
            {
                if (!pathfindingThread.IsAlive|| pathfindingThread==null)
                {
                    KeyValuePair<Navigator, Vector2> navigatorTargetPair = pathRequests.Dequeue();
                    Navigator navigator = navigatorTargetPair.Key;
                    Vector2 targetPosition = navigatorTargetPair.Value;
                    Vector2 navigatorPosition = navigator.transform.position;
                    pathfindingThread = new Thread(() => Sendpath(navigator, targetPosition, navigatorPosition));
                    pathfindingThread.Start();
                }
                await Task.Yield();
            }
            answeringRequests = false;
        }

        private async void AnswerRequests()
        {
            if (!answeringRequests)
            {
                sw.Start();
            }
            answeringRequests = true;
            while (pathRequests.Count > 0)
            {

                if (!calculatingPath)
                {
                    KeyValuePair<Navigator, Vector2> navigatorTargetPair = pathRequests.Dequeue();
                    Navigator navigator = navigatorTargetPair.Key;
                    Vector2 targetPosition = navigatorTargetPair.Value;
                    await SendPath(navigator, targetPosition);
                }
                await Task.Yield();
            }
            answeringRequests = false;
            sw.Stop();
            TimeSpan timeSpan = sw.Elapsed;
            UnityEngine.Debug.Log("Anser Request timing "+timeSpan.TotalMilliseconds);
        }
    }
}
