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



    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _playerUI = FindFirstObjectByType<PlayerUI>();
        InvokeRepeating(nameof(EnergyRegen), 0f, 1f);
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    void Update()
    {
        Move();
        Jump();
        EnergyRegen();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
    

    
    private void Move()
    {
        if (!_isGrounded) return;
        
        var rotation = transform.rotation;
        Vector3 move = Vector3.zero;
        Vector3 currentVelocity = _rb.linearVelocity;
        bool[] isMoving = new bool[4] {false, false, false, false};
        if (Input.GetKey(KeyCode.W))
        {
            move += Vector3.forward;
            isMoving[0] = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            move += Vector3.back;
            isMoving[1] = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            move += Vector3.left;
            isMoving[2] = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            move += Vector3.right;
            isMoving[3] = true;
        }
        
        if (isMoving != new bool[4]{false, false, false, false})
        {

            _rb.linearVelocity = new Vector3(
                (rotation * move * speed).x,
                currentVelocity.y,
                (rotation * move * speed).z);
        }
    }

    private void MoveInAir()
    {
        
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
            energyCurrent += energyRegen;
            _playerUI.UpdateEnergyBar(energyCurrent, energyMax);
        }
    }
}
