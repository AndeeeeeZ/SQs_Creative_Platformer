using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float jumpForce;

    private GameInput input;
    private Rigidbody rb;

    // Movement
    private float direction = 0;
    private bool isMoving = false;

    // Rotation
    private float targetYRotation;

    private void Awake()
    {
        input = new GameInput();
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        targetYRotation = transform.eulerAngles.y;
    }

    private void Update()
    {
        RotationUpdate();
    }

    private void RotationUpdate()
    {
        Vector3 angles = transform.eulerAngles;
        float yRotation = Mathf.MoveTowardsAngle(angles.y, targetYRotation, turnSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(angles.x, yRotation, angles.z);
    }

    private void FixedUpdate()
    {
        MoveUpdate();
    }

    private void MoveUpdate()
    {
        float xVelocity = 0f;
        if (isMoving)
            xVelocity = direction * moveSpeed;
        rb.velocity = new Vector3(xVelocity, rb.velocity.y, rb.velocity.z);
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
        isMoving = true;
        direction = context.ReadValue<float>();

        // Adjust target rotation based on direction
        targetYRotation = direction > 0 ? 0f : 180f;
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
}
