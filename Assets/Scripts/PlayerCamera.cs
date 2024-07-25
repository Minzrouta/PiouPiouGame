using System;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private Transform _head;
    [SerializeField]private float mouseSensitivity = 50f;
    [SerializeField]private float bottomCameraAngle = 0.5f, topCameraAngle = -0.5f;

    private void Start()
    {
        _head = transform.Find("Head");
    }
    
    private void Update()
    {
        CameraRotation();
    }

    private void CameraRotation()
    {
        var mouseX = Input.GetAxis("Mouse X") * mouseSensitivity; 
        var mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        if ((_head.transform.rotation.x > bottomCameraAngle && mouseY > 0) || (_head.transform.rotation.x < topCameraAngle && mouseY < 0))
        {
            mouseY = 0;
        }

        _head.transform.Rotate(Vector3.left * (mouseY * 3));
        transform.Rotate(Vector3.up * (mouseX * 3));
    }
}
