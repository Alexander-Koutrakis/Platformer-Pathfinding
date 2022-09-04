using UnityEngine;

namespace Pathfinding
{
    public class JumpAction : MovementAction
    {
        private BezierCurve bezierCurve;
        private Rigidbody2D body2D;
        private float timer = 0;

        public JumpAction(Transform movingTransform, Edge edge,Graph graph) : base(movingTransform, edge, graph)
        {
            //body2D = movingTransform.GetComponent<Rigidbody2D>();
            bezierCurve = edge.BezierCurve;    
        }

        protected override void End()
        {
            body2D.isKinematic = false;
            Completed = true;
        }

        protected override void Move()
        {
            //movement.FollowCurve(bezierCurve, timer);
            timer += Time.deltaTime;
        }

        public override void Start()
        {
            body2D.isKinematic = false;
            Completed = false;
        }
    }
}
