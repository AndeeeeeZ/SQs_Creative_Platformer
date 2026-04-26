using UnityEngine;

// Make object spin and move up and down
public class ObjectDisplay : MonoBehaviour
{
    [Header("Spin")]
    [SerializeField] private float spinSpeed; // Degrees per second

    [Header("Vertical Movement")]
    [SerializeField] private float moveSpeed; // How fast it bobs
    [SerializeField] private float moveRange; // How high it goes

    private Vector3 startPos;

    private void Start()
    {
        // Store initial position so movement is relative
        startPos = transform.position;
    }

    private void Update()
    {
        // Constant spin around Y axis
        transform.Rotate(0f, spinSpeed * Time.deltaTime, 0f);

        // Smooth up/down motion using sine wave
        float yOffset = Mathf.Sin(Time.time * moveSpeed) * moveRange;

        // Apply vertical movement
        transform.position = new Vector3(
            startPos.x,
            startPos.y + yOffset,
            startPos.z
        );
    }
}