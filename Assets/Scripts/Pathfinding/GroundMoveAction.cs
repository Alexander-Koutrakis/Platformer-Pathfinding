using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    public class GroundMoveAction : MovementAction
    {


        public GroundMoveAction(Transform movingTransform, Edge edge,Graph graph) : base(movingTransform, edge,graph)
        {

        }

        protected override void Move()
        {

        }

        public override void Start()
        {
            Completed = false;

        }

        protected override void End()
        {
            Completed = true;
        }
    }
}
