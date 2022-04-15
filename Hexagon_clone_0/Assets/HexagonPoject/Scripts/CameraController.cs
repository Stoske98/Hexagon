using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float rotateSpeed = 1f;
    float X;
    float Y;

    public float minDistance = 2f;
    public float maxDistance = 8f;
    public float scrollSensitivity = 1f;

    private Transform camera;
    private void Update()
    {
        if(camera != null)
        {
            if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftAlt))
            {
                transform.Rotate(new Vector3(Input.GetAxis("Mouse Y") * rotateSpeed, -Input.GetAxis("Mouse X") * rotateSpeed, 0));
                X = transform.rotation.eulerAngles.x;
                Y = transform.rotation.eulerAngles.y;
                transform.rotation = Quaternion.Euler(X, Y, 0);
            }

            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                Vector3 direction = (transform.position - camera.position).normalized;
                float distance = (transform.position - camera.position).magnitude;
                if (distance > minDistance)
                    camera.position += direction * scrollSensitivity;

            }

            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                Vector3 direction = (transform.position - camera.position).normalized;
                float distance = (transform.position - camera.position).magnitude;
                if (distance < maxDistance)
                    camera.position -= direction * scrollSensitivity;
            }

        }
    }

    public void SetCameraController(Transform c)
    {
        camera = c;
    }
}
