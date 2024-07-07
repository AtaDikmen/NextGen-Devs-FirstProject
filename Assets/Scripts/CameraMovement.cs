using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target; // The character's transform
    public Vector3 offset = new Vector3(0, 10, -15); // Offset from the target position

    void LateUpdate()
    {
        if (target != null)
        {
            // Follow the character's position but do not rotate
            transform.position = target.position + offset;
        }
    }
}
