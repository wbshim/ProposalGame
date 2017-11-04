using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yarn.Unity.Example
{
    public class CameraFollowDetroit : MonoBehaviour
    {
        Camera cam;
        public Transform Target;
        public bool FollowTargetForward = false;
        public float Smooth;
        public float XOffset;
        public float YOffset;
        public float ZOffset;

        Vector3 targetPosition;
        Vector3 smoothedPosition;
        Quaternion targetRotation;

        private void Start()
        {
            cam = Camera.main;
        }
        // Update is called once per frame
        void FixedUpdate()
        {

            // Smooth position
            targetPosition = Target.position + new Vector3(-XOffset, YOffset, ZOffset);
            smoothedPosition = Vector3.Lerp(transform.position, targetPosition, Smooth * Time.fixedDeltaTime);
            transform.position = smoothedPosition;

            // Smooth rotation
            if (FollowTargetForward)
            {
                targetRotation = Quaternion.LookRotation(Target.forward);
            }
            else
            {
                targetRotation = Quaternion.LookRotation(Vector3.right);
            }
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Smooth * Time.fixedDeltaTime);
        }
        public void SetFollowTargetForward(bool setFollowTargetForward)
        {
            FollowTargetForward = setFollowTargetForward;
        }
        public void SetTarget(Transform _target)
        {
            Target = _target;
        }


    }
}

