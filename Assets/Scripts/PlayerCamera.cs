using System;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private Transform _head;
    [SerializeField]private float mouseSensitivity = 50f;
    [SerializeField]private float bottomCameraAngle = 0.5f, topCameraAngle = -0.5f;
    private float xRotation = 0f;

    private void Start()
    {
        _head = transform.Find("Head");
    }
    
    private void Update()
    {
        CameraRotation();
    }

    public void CameraRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity; 
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        Debug.Log(_head.transform.rotation.x);
        
        if (_head.transform.rotation.x > bottomCameraAngle && mouseY > 0)
        {
            mouseY = 0;
        }
        if (_head.transform.rotation.x < topCameraAngle && mouseY < 0)
        {
            mouseY = 0;
        }
        
        _head.transform.Rotate(Vector3.left * (mouseY * 3));
        
        transform.Rotate(Vector3.up * (mouseX * 3));
    }
}
