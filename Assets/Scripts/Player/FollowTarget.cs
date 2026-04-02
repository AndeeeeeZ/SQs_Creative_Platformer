using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;
    private void LateUpdate()
    {
        float targetX = targetTransform.position.x; 
        float targetY = targetTransform.position.y; 
        transform.position = new Vector3(targetX, targetY, transform.position.z);
    }
}
