using System;
using UnityEngine;

public class MonsterMover : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 2f;

    [Header("Visuals")]
    [SerializeField] private Transform modelTransform;     // 3D model root
    [SerializeField] private SpriteRenderer spriteRenderer;

    private float leftX;
    private float rightX;
    private float targetX;
    private int direction; // -1 = left, +1 = right

    private Action onRemoved;

    public void Initialize(Transform player, float leftBound, float rightBound, Action removedCallback)
    {
        leftX = Mathf.Min(leftBound, rightBound);
        rightX = Mathf.Max(leftBound, rightBound);
        onRemoved = removedCallback;

        // Decide direction based on spawn side relative to player
        if (transform.position.x < player.position.x)
        {
            targetX = rightX;
            direction = 1;
        }
        else
        {
            targetX = leftX;
            direction = -1;
        }

        ApplyFacing();
    }

    private void Update()
    {
        float newX = Mathf.MoveTowards(transform.position.x, targetX, moveSpeed * Time.deltaTime);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);

        // Despawn at bound
        if (Mathf.Abs(transform.position.x - targetX) < 0.05f)
        {
            Destroy(gameObject);
        }
    }

    private void ApplyFacing()
    {
        // Flip sprite
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = direction > 0;
        }

        // Rotate 3D model (Y axis)
        if (modelTransform != null)
        {
            modelTransform.localRotation = Quaternion.Euler(0f, direction == 1 ? -90f : 90f, 0f);
        }
    }

    private void OnDestroy()
    {
        onRemoved?.Invoke();
        onRemoved = null;
    }
}