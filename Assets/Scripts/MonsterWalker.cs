using UnityEngine;
using UnityEngine.Events;

public class MonsterWalker : MonoBehaviour
{
    public UnityEvent OnCaught; 
    [SerializeField] private float speed = 3f;
    [SerializeField] private SpriteRenderer spriteRenderer; 

    private int direction = 1; // 1 = right, -1 = left

    private void Update()
    {
        // Move along X axis
        transform.Translate(Vector3.right * direction * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            // Reverse direction
            direction *= -1;

            spriteRenderer.flipX = !spriteRenderer.flipX; 
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            OnCaught?.Invoke(); 
        }
    }
}