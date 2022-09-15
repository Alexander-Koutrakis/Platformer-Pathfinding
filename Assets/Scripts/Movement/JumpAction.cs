using UnityEngine;

namespace Pathfinding
{
    public class JumpAction : MovementAction
    {
        private float waitToJumpTimer = 0.2f;
        private BezierCurve bezierCurve;
        private float timer = 0;


        public JumpAction( Edge edge,Vector2 targetPosition) : base( edge, targetPosition)
        {
            bezierCurve = edge.BezierCurve;
        }

        protected override void End(Movement movement)
        {            
            movement.RigidBodyIsKinematic(false);
            movement.StopMovement();
            Completed = true;
        }

        protected override void Move(Movement movement)
        {
            if (waitToJumpTimer <= 0)
            {
                movement.FollowCurve(bezierCurve, timer);
                timer += Time.deltaTime;
                timer = Mathf.Clamp(timer, 0, 1);
            }
            else
            {
                waitToJumpTimer -= Time.deltaTime;
            }
        }

        public override void Start(Movement movement)
        {
            movement.FaceTarget(targetPosition);
            movement.StopMovement();
            movement.RigidBodyIsKinematic(true);
            Completed = false;
            started = true;
        }
    }
}
