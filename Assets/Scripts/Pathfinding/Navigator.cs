using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    public class Navigator:MonoBehaviour
    {
        private const float ASK_PATH_INTERVAL = 0.2f;
        private float askPathTimeRemaining = 0.2f;
        private Path path;
        private MovementAction currentAction;
        public Transform Transform { private set; get; }
        public Transform TargetTransform { private set; get;}
        private void AskActions()
        {
            PathfinderMediator.Instance.PathResquest(this, TargetTransform.position);
        }

        public void SetTarget(Transform target)
        {
            Transform = transform;
            TargetTransform = target;
            AskActions();
        }
        public void SetPath(Path path)
        {
            this.path = path;
            path.DebugPath();
        }
        public void Update()
        {
            if (askPathTimeRemaining > 0)
            {
                askPathTimeRemaining -= Time.deltaTime;
                if (askPathTimeRemaining <= 0)
                {
                    AskActions();
                    currentAction = path.NextAction();
                    askPathTimeRemaining = ASK_PATH_INTERVAL;
                }
            }

            if (currentAction != null) { }
               // FollowPath();
            
        }

        private void FollowPath()
        {
                if (!currentAction.Completed)
                {
                    currentAction.Update();
                }
                else if (currentAction.Completed)
                {
                    if (!path.IsCompleted)
                    {
                        currentAction = path.NextAction();
                        currentAction.Start();
                    }                    
                }
        }
    }

   
}
