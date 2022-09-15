using UnityEngine;
namespace Pathfinding
{
    public class GroundMoveAction : MovementAction
    {

        public GroundMoveAction(Edge edge, Vector2 targetPosition) : base(edge, targetPosition) { }
        
        protected override void Move(Movement movement)
        {
            movement.Move();
        }

        public override void Start(Movement movement)
        {
            movement.FaceTarget(targetPosition);
            Completed = false;
            started = true;
        }

        protected override void End(Movement movement)
        {
            Completed = true;
        }
    }
}
