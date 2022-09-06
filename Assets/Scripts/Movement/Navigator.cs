using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    public class Navigator:MonoBehaviour
    {
        private const float ASK_PATH_INTERVAL = 1f;
        private float askPathTimeRemaining=0;
        private Path path;
        private bool readyToRequest;
        [SerializeField]private MovementAction currentAction=null;
        public Transform Transform { private set; get; }
        public Transform TargetTransform { private set; get;}

        private void Start()
        {
            TargetTransform = GameObject.FindGameObjectWithTag("Target").transform;
            Transform = transform;
            PathRequest();  
        }

        private void PathRequest()
        {
            PathfinderMediator.Instance.PathResquest(this, TargetTransform.position);
        }

        #if UNITY_EDITOR 
        public void SetTargetTesting(Transform target)
        {
            Transform = transform;
            TargetTransform = target;
            PathRequest();
        }
        #endif


        public void SetPath(Path path)
        {
            
            this.path = path;
            this.path.DebugPath();

            currentAction = this.path.NextAction();
            askPathTimeRemaining = ASK_PATH_INTERVAL;
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
                }
            }

            if (currentAction != null)
                FollowPath();
            
            if (path != null)
                path.DebugPath();
        }

        private void FollowPath()
        {
            if (!currentAction.Started)
            {
                currentAction.Start();
            }
           
            if (!currentAction.Completed)
            {
                 currentAction.Update();
            }
            else if (currentAction.Completed)
            {
                if (!path.IsCompleted)
                {
                    currentAction = this.path.NextAction();
                }

                if (readyToRequest)
                {
                    PathRequest();
                }
             }
        }
    }

   
}
