using UnityEngine;

public class DoodleJumpCameraLock : MonoBehaviour
{
    float maxY;

    void Start()
    {
        maxY = transform.position.y;
    }

    void LateUpdate()
    {
        if (transform.position.y > maxY)
        {
            maxY = transform.position.y;
        }

        transform.position = new Vector3(
            transform.position.x,
            maxY,
            transform.position.z
        );
    }
}
