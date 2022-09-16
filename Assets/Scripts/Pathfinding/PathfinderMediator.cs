using System.Threading.Tasks;
using UnityEngine;


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
            }
            else
            {
                Destroy(gameObject);
            }

            graph.Initialize();
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
