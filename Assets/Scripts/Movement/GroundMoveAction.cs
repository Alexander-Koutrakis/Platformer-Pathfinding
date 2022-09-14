using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    public class GroundMoveAction : MovementAction
    {

        private Movement movement;
        public GroundMoveAction(Transform movingTransform, Edge edge,Graph graph) : base(movingTransform, edge,graph)
        {
            
        }

        protected override void Move()
        {
            movement.Move();
        }

        public override void Start()
        {
            movement = movingTransform.GetComponent<Movement>(); 
            movement.FaceTarget(targetPosition);
            Completed = false;
            Started = true;
        }

        protected override void End()
        {
            Completed = true;
        }
    }
}
