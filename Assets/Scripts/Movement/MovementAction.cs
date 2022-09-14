using UnityEngine;
namespace Pathfinding
{
    public abstract class MovementAction
    {
        protected Transform movingTransform;
        protected Vector2 targetPosition;
        protected const float REACH_DISTANCE = 0.3f;

        public bool Completed { protected set; get; }
        public bool Started { protected set; get; }
        public abstract void Start();
        protected abstract void Move();
        protected abstract void End();
        private bool TargetReached()
        {
            if (Vector2.Distance(movingTransform.position, targetPosition) < REACH_DISTANCE)
            {
                return true;
            }

            return false;
        }

        public MovementAction(Transform movingTransform, Edge edge,Graph graph)
        {

            this.movingTransform = movingTransform;
            this.targetPosition = graph.PathNodes[edge.DestinationNodeHashCode].Position;
        }

       
        public void Update()
        {
            if (!TargetReached())
            {
                Move();
            }
            else
            {
                End();
            }
        }
         
    }
}
