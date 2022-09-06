using System.Threading.Tasks;
using UnityEngine;
using System.Diagnostics;
using System;
namespace Pathfinding
{
    public class PathfinderMediator:MonoBehaviour
    {
        [SerializeField]private Graph graph;
        public static PathfinderMediator Instance { private set; get;}
      
        private void Awake()
        {
            if (Instance == null){
                Instance = this;
                GraphConstructor graphConstructor = FindObjectOfType<GraphConstructor>();
                graph = graphConstructor.NewGraph();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void SetinstanceTest(Graph graph)
        {
            Instance = this;
            this.graph = graph;
        }

        public void PathResquest(Navigator navigator, Vector2 target)
        {
            Vector2 navigatorPosition = navigator.transform.position;
            Task.Factory.StartNew(() => SendPath(navigator, target, navigatorPosition));
        }
  

        private void SendPath(Navigator navigator, Vector2 targetPosition,Vector2 navigatorPosition)
        {
            Path path = Pathfinder.GetPath(navigatorPosition, targetPosition, graph, navigator.Transform);
            navigator.SetPath(path);
        }    
       
    }
}
