  using System;
using System.Collections.Generic;
using UnityEngine;
namespace Pathfinding
{
    public class Path
    {
        private Queue<MovementAction> movementActionPath=new Queue<MovementAction>();
        private List<Vector2> positions = new List<Vector2>();
        public bool IsCompleted {
            get
            {
                return movementActionPath.Count > 0 ? false : true;
            }
        }        
        public int Length
        {
            get
            {
                return movementActionPath.Count;
            }
        }
        public Path(Stack<Edge> path,Transform transform,Graph graph)
        {
            while (path.Count > 0)
            {
                Edge edge = path.Pop();
                if (edge.HasCurve)
                {
                    Vector2 target = graph.PathNodes[edge.DestinationNodeHashCode].Position;
                    JumpAction jumpAction = new JumpAction(edge, target);
                    movementActionPath.Enqueue(jumpAction);
                }
                else
                {
                    Vector2 target = graph.PathNodes[edge.DestinationNodeHashCode].Position;
                    GroundMoveAction groundMoveAction = new GroundMoveAction(edge, target);
                    movementActionPath.Enqueue(groundMoveAction);
                }

                positions.Add(graph.PathNodes[edge.DestinationNodeHashCode].Position);
            }
        }

        public MovementAction NextAction()
        {

            if (movementActionPath.Count > 0)
            {
                return movementActionPath.Dequeue();
            }
            return null;
            
        }

        public void DebugPath()
        {
            for(int i = 0; i < positions.Count; i++)
            {
                if(i< positions.Count - 2)
                {
                    Debug.DrawLine(positions[i], positions[i + 1], Color.cyan, 10f);
                }
            }
        }

        
    }
}
