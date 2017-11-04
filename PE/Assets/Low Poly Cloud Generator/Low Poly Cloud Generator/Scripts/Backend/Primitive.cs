using UnityEngine;

namespace LowPolyCloudGenerator
{
    public class Primitive : MonoBehaviour
    {
        [Range(0f, 2f)]
        public float multiplier = 1f;

        void OnDrawGizmos()
        {
            if (transform.parent)
            {
                Gizmos.DrawWireSphere(transform.position, multiplier * transform.parent.localScale.x);
            }
            else
            {
                Gizmos.DrawWireSphere(transform.position, multiplier);
            }
        }
    }
}