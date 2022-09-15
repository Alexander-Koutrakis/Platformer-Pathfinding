using UnityEngine;
namespace Pathfinding
{
    public abstract class MovementAction
    {
        protected readonly Vector2 targetPosition;
        protected const float REACH_DISTANCE = 0.3f;
        public bool Completed { protected set; get; }
        protected bool started;
        public abstract void Start(Movement movement);
        protected abstract void Move(Movement movement);
        protected abstract void End(Movement movement);
        private bool TargetReached(Vector2 position)
        {
            if (Vector2.Distance(position, targetPosition) < REACH_DISTANCE)
            {
                return true;
            }

            return false;
        }

        public MovementAction(Edge edge,Vector2 targetPosition)
        {
            this.targetPosition = targetPosition;
        }
       
        public void Update(Movement movement)
        {
            if (!started)
            {
                Start(movement);
            }


            if (!TargetReached(movement.transform.position))
            {
                Move(movement);
            }
            else
            {
                End(movement);
            }
        }
         
    }
}
