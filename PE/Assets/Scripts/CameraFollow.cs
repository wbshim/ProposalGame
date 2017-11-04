using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;

    public float smoothSpeed = 0.1f;
    public Vector3 offset;
    public float cameraClampX = 17.5f;
    public float cameraClampY = 5;

    void Start()
    {
        Vector3 desiredPosition = new Vector3(Mathf.Clamp(target.position.x, -cameraClampX, cameraClampX), target.position.y, Mathf.Clamp(target.position.z, -cameraClampY, cameraClampY)) + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition;
        transform.LookAt(target);
    }

    void FixedUpdate()
    {

        Vector3 desiredPosition = new Vector3(Mathf.Clamp(target.position.x,-cameraClampX, cameraClampX), target.position.y, Mathf.Clamp(target.position.z, -cameraClampY, cameraClampY)) + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
