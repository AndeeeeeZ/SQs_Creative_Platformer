using UnityEngine;

public class FirstPersonRaycast : MonoBehaviour
{
    [SerializeField] private bool SHOW_DEBUG_RAY;
    [SerializeField] private Camera firstPersonCamera;
    [Header("Stats")]
    [SerializeField] private float range;
    [SerializeField] private LayerMask interactableLayer;

    private IInteractable currentInteractable;

    // Shoot a ray and 
    private void ShootRay()
    {
        Ray ray = firstPersonCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (SHOW_DEBUG_RAY)
            Debug.DrawRay(ray.origin, ray.direction * range, Color.red);

        if (Physics.Raycast(ray, out RaycastHit hit, range, interactableLayer))
        {
            if (hit.collider.TryGetComponent<IInteractable>(out var interactable))
            {
                if (currentInteractable != interactable)
                {
                    currentInteractable?.OnExitRange();
                    interactable.OnEnterRange();
                    currentInteractable = interactable;
                }
            }
            else
            {
                Debug.LogWarning($"{hit.collider.gameObject.name} does not contain a IInteractable");
            }
        }
        else // When the raycast didn't hit anything
        {
            // NOTE: this is for solving issue where interfacing is not null when gameObject is destroyed 
            if (currentInteractable is MonoBehaviour mb && mb == null)
            {
                currentInteractable = null;
                return;
            }
            currentInteractable?.OnExitRange();
            currentInteractable = null;
        }
    }

    private void Update()
    {
        ShootRay();
    }

    public IInteractable GetCurrentInteractable()
    {
        if (currentInteractable == null) return null;
        return currentInteractable;
    }

}
