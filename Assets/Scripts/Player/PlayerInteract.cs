using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    private FirstPersonRaycast raycast;
    private GameInput input; 

    private void Awake()
    {
        input = new GameInput(); 
        raycast = GetComponent<FirstPersonRaycast>(); 
    }

    private void OnEnable()
    {
        input.Enable(); 
        input.Player.Interact.performed += TryInteract; 
    }

    private void OnDisable()
    {
        input.Player.Interact.performed -= TryInteract;
        input.Disable();  
    }

    private void TryInteract(InputAction.CallbackContext context)
    {
        IInteractable currInteractable = raycast.GetCurrentInteractable(); 
        if (currInteractable == null)
        {
            Debug.LogWarning("Unable to interact with anything at the moment"); 
            return; 
        }

        currInteractable.OnInteract(transform); 
    }
}
