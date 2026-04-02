using UnityEngine;

public class FirstPersonRaycast : MonoBehaviour
{
    [SerializeField] private bool SHOW_DEBUG_RAY;
    [SerializeField] private Camera firstPersonCamera;
    [Header("Stats")]
    [SerializeField] private float range;
    [SerializeField] private LayerMask hitLayers;
    [SerializeField] private string interactableTag = "Interactable";

    // Shoot a ray and 
    private void ShootRay()
    {
        Ray ray = firstPersonCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        if (SHOW_DEBUG_RAY)
            Debug.DrawRay(ray.origin, ray.direction * range, Color.red);

        if (Physics.Raycast(ray, out hit, range, hitLayers))
        {
            if (hit.collider.CompareTag(interactableTag))
            {
                // Trigger interactable
            }
        }
    }

    private void Update()
    {
        ShootRay();
    }

}
