using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    public class Navigator:MonoBehaviour
    {
        private const float ASK_PATH_INTERVAL = 1f;
        private float askPathTimeRemaining=1;
        private Path path;
        private bool readyToRequest;
        private Movement movement;
        private MovementAction currentAction=null;
        public Transform Transform { private set; get; }
        public Transform TargetTransform { private set; get;}

        private void Awake()
        {
            movement = GetComponent<Movement>();
            Transform = transform;
            TargetTransform = GameObject.FindGameObjectWithTag("Target").transform;
        }
        private void Start()
        {   
            PathRequest();  
        }

        private void PathRequest()
        {
            PathfinderMediator.Instance.PathResquest(this, TargetTransform.position);
        }

        public void SetPath(Path path)
        {
            
            this.path = path;
            this.path.DebugPath();

            currentAction = this.path.NextAction();
            readyToRequest = false;
        }
        public void Update()
        {
            if (askPathTimeRemaining > 0)
            {
                askPathTimeRemaining -= Time.deltaTime;
                if (askPathTimeRemaining <= 0)
                {
                    readyToRequest = true;
                    askPathTimeRemaining = ASK_PATH_INTERVAL;
                }
            }

            if (currentAction != null)
                FollowPath();

            if (path != null)
                path.DebugPath();
        }

        

        private void FollowPath()
        {

            currentAction.Update(movement);

            
            if (currentAction.Completed)
            {
                ActionComplete();
            }
        }

        private void ActionComplete()
        {
            if (!path.IsCompleted)
            {
                currentAction = path.NextAction();
                currentAction.Start(movement);
            }
            else
            {
                movement.StopMovement();
            }

            if (readyToRequest)
            {
                PathRequest();
            }
        }
    }

   
}
