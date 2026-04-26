using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerMarkController playerMark; // 2D representation of the player

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sprintExtraSpeed = 5f;
    [SerializeField] private float turnSpeed = 360f;
    [SerializeField] private float jumpForce = 5f;

    private GameInput input;
    private Rigidbody rb;

    private float targetYRotation;

    private Direction faceDirection = Direction.RIGHT; // default facing right

    private bool move2DHeld;
    private bool move3DHeld;

    private float move2DInput;
    private float move3DInput;

    private bool sprinting = false;

    private enum Direction
    {
        NONE = 0,
        LEFT = -1,
        RIGHT = 1
    }

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

    private void FixedUpdate()
    {
        MoveUpdate();
    }

    private void OnEnable()
    {
        input.Enable();

        input.Player.Move2D.performed += Move2D;
        input.Player.Move2D.canceled += StopMove2D;

        input.Player.Move3D.performed += Move3D;
        input.Player.Move3D.canceled += StopMove3D;

        input.Player.Sprint.performed += StartSprint;
        input.Player.Sprint.canceled += StopSprint;

        input.Player.Jump.performed += Jump;
    }

    private void OnDisable()
    {
        input.Player.Move2D.performed -= Move2D;
        input.Player.Move2D.canceled -= StopMove2D;

        input.Player.Move3D.performed -= Move3D;
        input.Player.Move3D.canceled -= StopMove3D;

        input.Player.Sprint.performed -= StartSprint;
        input.Player.Sprint.canceled -= StopSprint;

        input.Player.Jump.performed -= Jump;

        input.Disable();
    }

    private void RotationUpdate()
    {
        Vector3 angles = transform.eulerAngles;
        float yRotation = Mathf.MoveTowardsAngle(angles.y, targetYRotation, turnSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(angles.x, yRotation, angles.z);
    }

    private void MoveUpdate()
    {
        float velocityX = 0f;
        float speed = moveSpeed + (sprinting ? sprintExtraSpeed : 0f);

        // Prioritize 2D input if held
        if (move2DHeld)
        {
            velocityX = move2DInput * speed;
        }
        else if (move3DHeld)
        {
            // Move relative to facing direction
            velocityX = (int)faceDirection * move3DInput * speed;
        }
        
        rb.velocity = new Vector3(velocityX, rb.velocity.y, rb.velocity.z);
    }

    // A / D input
    // Sets facing + direct movement
    private void Move2D(InputAction.CallbackContext context)
    {
        move2DHeld = true;
        move2DInput = context.ReadValue<float>();

        if (move2DInput > 0f)
        {
            faceDirection = Direction.RIGHT;
            targetYRotation = 0f;
        }
        else if (move2DInput < 0f)
        {
            faceDirection = Direction.LEFT;
            targetYRotation = 180f;
        }

        playerMark.StartWalking();
        playerMark.SetDirection((float)faceDirection);
    }

    private void StopMove2D(InputAction.CallbackContext context)
    {
        move2DHeld = false;

        if (!move3DHeld)
        {
            playerMark.StopWalking();
        }
    }

    // W / S input
    // Moves relative to facing, does NOT change rotation
    private void Move3D(InputAction.CallbackContext context)
    {
        move3DHeld = true;
        move3DInput = context.ReadValue<float>();

        if (move3DInput > 0f)
        {
            // Forward
            playerMark.StartWalking();
        }
        else if (move3DInput < 0f)
        {
            // Backward
            playerMark.StartWalkingBack();
        }
    }

    private void StopMove3D(InputAction.CallbackContext context)
    {
        move3DHeld = false;

        if (!move2DHeld)
        {
            playerMark.StopWalking();
        }
    }

    private void StartSprint(InputAction.CallbackContext context)
    {
        sprinting = true;
    }

    private void StopSprint(InputAction.CallbackContext context)
    {
        sprinting = false;
    }


    #region Jump
    private void Jump(InputAction.CallbackContext context)
    {
        ApplyJumpForce();
    }

    // Reset vertical velocity before jump
    private void ResetYVelocity()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
    }

    private void ApplyJumpForce()
    {
        ResetYVelocity();
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    #endregion
}