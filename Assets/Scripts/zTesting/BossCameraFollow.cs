using UnityEngine;

public class BossCameraFollow : MonoBehaviour
{
    public Transform CameraTransform;

    private void FixedUpdate()
    {
        var pos = CameraTransform.position;
        pos.y = transform.position.y;
        transform.position = pos;
    }
}
