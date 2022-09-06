using UnityEngine;

namespace Pathfinding
{
    public class JumpAction : MovementAction
    {
        private float waitToJumpTimer = 0.5f;
        private BezierCurve bezierCurve;
        private float timer = 0;
        private Movement movement;

        public JumpAction(Transform movingTransform, Edge edge,Graph graph) : base(movingTransform, edge, graph)
        {
            bezierCurve = edge.BezierCurve;    
        }

        protected override void End()
        {            
            movement.RigidBodyIsKinematic(false);
            movement.StopMovement();
            Completed = true;
            Debug.Log("Stop jumping");
        }

        protected override void Move()
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

        public override void Start()
        {
            movement = movingTransform.GetComponent<Movement>();
            movement.FaceTarget(targetPosition);
            movement.StopMovement();
            movement.RigidBodyIsKinematic(true);
            Completed = false;
            Started = true;
        }
    }
}
