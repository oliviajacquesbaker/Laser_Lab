using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    new Camera camera;
    public float cropAmount = 1.25f;

    public void PositionCamera(Vector3 center, Vector2Int size)
    {
        if (!camera)
            camera = GetComponent<Camera>();

        float sensorY = camera.sensorSize.x / camera.aspect;

        float ax = (camera.sensorSize.x) - (camera.sensorSize.x * camera.lensShift.x) * 2;
        float ay = (sensorY) - (sensorY * camera.lensShift.y) * 2;

        float yMod = Mathf.Abs(Mathf.Cos(transform.rotation.eulerAngles.x));

        float b = camera.focalLength;

        float ratioX = ax / b;
        float ratioY = ay / b / yMod;

        float distanceX = (size.x - cropAmount) / ratioX;
        float distanceY = (size.y - cropAmount) / ratioY;

        float distance = distanceX > distanceY ? distanceX : distanceY;

        Vector3 forward = transform.forward;

        transform.position = center - (distance * forward);
    }

}
