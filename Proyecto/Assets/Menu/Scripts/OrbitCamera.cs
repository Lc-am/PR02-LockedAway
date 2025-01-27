using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoOrbitCamera : MonoBehaviour
{
    public Transform target;

    public float distance = 10.0f;

    public float rotationSpeed = 20.0f;

    public float height = 2.0f;

    void LateUpdate()
    {
        if (target)
        {
            float angle = rotationSpeed * Time.deltaTime;

            transform.RotateAround(target.position, Vector3.up, angle);

            Vector3 desiredPosition = (transform.position - target.position).normalized * distance + target.position;
            desiredPosition.y = target.position.y + height;

            transform.position = desiredPosition;

            transform.LookAt(target);
        }
    }
}