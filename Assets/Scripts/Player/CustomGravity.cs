using UnityEngine;

public class CustomGravity : MonoBehaviour
{
    [SerializeField] private float normalGravity, fallGravity;

    private float currGravity;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // Disable Unity default gravity so we fully control it
        rb.useGravity = false;

        currGravity = normalGravity;
    }

    public void SwitchToNormalGravity()
    {
        currGravity = normalGravity;
    }

    public void SwitchToFallGravity()
    {
        currGravity = fallGravity;
    }

    private void FixedUpdate()
    {
        // Apply gravity manually every physics step
        rb.AddForce(Vector3.down * currGravity, ForceMode.Acceleration);
    }
}