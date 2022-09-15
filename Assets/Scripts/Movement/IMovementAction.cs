using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Pathfinding
{
    public interface IMovementAction
    {
        void Start(Movement movement);
        void Update(Movement movement);
        bool Completed { get; }
        bool Started { get; }

        void CreateAction(Edge edge, Graph graph);
    }
}
