using UnityEditor.Embree;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    private Rigidbody _rb;
    public float speed = 5;
    public float jumpForce = 5;
    public float energyCurrent = 100;
    public float energyRegen = 1;
    public float energyMax = 100;
    public float jumpCost = 10;
    private PlayerUI _playerUI;
    private bool _isGrounded;
    [SerializeField]private float mouseSensitivity = 50f;
    [SerializeField]private float minCameraview = -70f, maxCameraview = 80f;
    private Camera _camera;
    private float xRotation = 0f;
    private Transform _head;
    

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _playerUI = FindFirstObjectByType<PlayerUI>();
        InvokeRepeating(nameof(EnergyRegen), 0f, 1f);
        _camera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        _head = transform.Find("Head");
    }
    
    void Update()
    {
        Move();
        CameraRotation();
        Jump();
        EnergyRegen();
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
    
    private void CameraRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity; 
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        //xRotation -= mouseY;
        //xRotation = Mathf.Clamp(xRotation, minCameraview, maxCameraview);
        //_camera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        
        
        _head.transform.Rotate(Vector3.left * (mouseY * 3));
        transform.Rotate(Vector3.up * (mouseX * 3));
    }
    
    private void Move()
    {
        var rotation = transform.rotation;
        Vector3 move = Vector3.zero;
        
        if (Input.GetKey(KeyCode.W))
        {
            move += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            move += Vector3.back;
        }
        if (Input.GetKey(KeyCode.A))
        {
            move += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            move += Vector3.right;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            move += Vector3.up * jumpForce;
        }
        
        Vector3 lVelocity = new Vector3((rotation * move * speed).x, _rb.linearVelocity.y,(rotation * move * speed).z) ;
        _rb.linearVelocity = lVelocity;
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && energyCurrent > 1)
        {
            if (energyCurrent < jumpCost) { energyCurrent = 0; }
            else { energyCurrent -= jumpCost; }
            _playerUI.UpdateEnergyBar(energyCurrent, energyMax);
            
            _rb.AddForce(Vector3.up * (jumpForce + jumpForce * energyCurrent/energyMax), ForceMode.Impulse);
            
            _isGrounded = false;
        }
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }
    
    private void EnergyRegen()
    {
        if (energyCurrent < energyMax && _isGrounded)
        {
            energyCurrent += 1;
            _playerUI.UpdateEnergyBar(energyCurrent, energyMax);
        }
    }
}
