using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed, jumpForce;
    
    private GameInput input;
    private Rigidbody rb; 

    // Movement
    private float direction = 0;
    private bool isMoving = false;
    

    private void Awake()
    {
        input = new GameInput();
        rb = GetComponent<Rigidbody>(); 
    }

    private void OnEnable()
    {
        input.Enable();
        input.Player.Move3D.performed += Move;
        input.Player.Move3D.canceled += StopMoving;
        input.Player.Jump.performed += Jump; 
    }
    private void OnDisable()
    {
        input.Player.Move3D.performed -= Move;
        input.Player.Move3D.canceled -= StopMoving;
        input.Player.Jump.performed -= Jump; 
        input.Disable();
    }
    private void Move(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<float>();
        isMoving = true;
    }
    private void StopMoving(InputAction.CallbackContext context)
    {
        isMoving = false;
    }

    // Use before applying upward force for jump
    // Necessary so the negative downward force doesn't cancel out the upward jump force
    private void ResetYVelocity()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); 
    }

    private void ApplyJumpForce()
    {
        ResetYVelocity(); 
        Vector3 force = Vector3.up * jumpForce; 
        rb.AddForce(force, ForceMode.Impulse); 
    }
    private void Jump(InputAction.CallbackContext context)
    {
        ApplyJumpForce(); 
    }
    private void MoveUpdate()
    {
        float xVelocity = 0f; 
        if (isMoving)
            xVelocity = direction * moveSpeed; 
        rb.velocity = new Vector3(xVelocity, rb.velocity.y, rb.velocity.z); 
    }

    private void FixedUpdate()
    {
        MoveUpdate();
    }
}
