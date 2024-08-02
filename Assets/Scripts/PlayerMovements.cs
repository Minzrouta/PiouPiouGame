using System;
using System.Linq;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
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
    
    public KeyCode forward = KeyCode.W;
    public KeyCode backward = KeyCode.S;
    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;
    public KeyCode jump = KeyCode.Space;
    public KeyCode sprint = KeyCode.LeftShift;



    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _playerUI = FindFirstObjectByType<PlayerUI>();
        InvokeRepeating(nameof(EnergyRegen), 0f, 1f);
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    void FixedUpdate()
    {
        if (_isGrounded) {Move(1);}
        else {MoveInAir();}
        EnergyRegen();
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void Update()
    {
        Jump();
    }


    private void Move(float power)
    {
        var rotation = transform.rotation;
        Vector3 move = Vector3.zero;
        Vector3 currentVelocity = _rb.linearVelocity;
        bool isMoving = false;
        if (Input.GetKey(forward))
        {
            move += Vector3.forward;
            isMoving = true;
        }
        if (Input.GetKey(backward))
        {
            move += Vector3.back;
            isMoving = true;
        }
        if (Input.GetKey(left))
        {
            move += Vector3.left;
            isMoving = true;
        }
        if (Input.GetKey(right))
        {
            move += Vector3.right;
            isMoving = true;
        }
        
        move = move.normalized;
        
        if (isMoving)
        {
            _rb.linearVelocity = new Vector3(
                (rotation * move * speed).x*power,
                currentVelocity.y,
                (rotation * move * speed).z*power);
        }
    }

    private void MoveInAir()
    {
        var rotation = transform.rotation;
        Vector3 move = Vector3.zero;
        if (Input.GetKey(forward))
        {
            move += Vector3.forward;
        }
        if (Input.GetKey(backward))
        {
            move += Vector3.back;
        }
        if (Input.GetKey(left))
        {
            move += Vector3.left;
        }
        if (Input.GetKey(right))
        {
            move += Vector3.right;
        }
        
        move = move.normalized;
        _rb.AddForce(new Vector3((rotation * move * speed).x, 0, (rotation * move * speed).z)*3, ForceMode.Impulse);
        
    }

    private void Jump()
    {
        if (Input.GetKeyDown(jump) && (energyCurrent > 1 || _isGrounded))
        {
            if (_isGrounded) { }
            else if (energyCurrent < jumpCost) { energyCurrent = 0; }
            else { energyCurrent -= jumpCost; }
            _playerUI.UpdateEnergyBar(energyCurrent, energyMax);

            var lVelocity = _rb.linearVelocity;
            lVelocity = new Vector3(lVelocity.x, 0, lVelocity.z);
            _rb.linearVelocity = lVelocity;
            _rb.AddForce(Vector3.up * (jumpForce + jumpForce * energyCurrent/energyMax), ForceMode.Impulse);
            Move(0.5f);
            
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
            energyCurrent += energyRegen;
            _playerUI.UpdateEnergyBar(energyCurrent, energyMax);
        }
    }
}
