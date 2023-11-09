using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCameraFollow : MonoBehaviour
{
    public Transform CameraTransform;

    void FixedUpdate()
    {
        var pos = CameraTransform.position;
        pos.y = transform.position.y;
        transform.position = pos;
    }
}
