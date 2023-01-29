using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 3;
    public Vector3 offset = new Vector3(0, 3, -10);
    public bool shouldLockOnY;

    private void LateUpdate()
    {
        Vector3 desiredPos = target.position + offset;
        Vector3 yLock = new Vector3(target.position.x, 3.484877f, target.position.z - 10);
        if (shouldLockOnY == true)
        {
            Vector3 smoothedPos = Vector3.Lerp(transform.position, yLock, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPos;
        }
        else
        {
            Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPos;
        }
        
    }
}
