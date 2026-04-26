using UnityEngine;

public class Stair : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform teleportTransform; // position + rotation
    [SerializeField] private Stair correspondingStair; 
    [SerializeField] private GameObject UI;
    [SerializeField] private PlayerMovement.Direction directionWhenTeleport;

    private void Awake()
    {
        UI.SetActive(false); 
    }
    public void OnEnterRange()
    {
        UI.SetActive(true); 
    }

    public void OnExitRange()
    {
        UI.SetActive(false); 
    }

    public Transform GetTeleportTransform()
    {
        return teleportTransform;
    }

    public PlayerMovement.Direction GetTeleportDirection()
    {
        return directionWhenTeleport; 
    }

    public void OnInteract(Transform interactor)
    {
        Transform targetTransform = correspondingStair.GetTeleportTransform(); 
        interactor.position = targetTransform.position; 
        if (interactor.TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement))
        {
            playerMovement.SetPlayerDirection(correspondingStair.GetTeleportDirection()); 
        }
        else
            Debug.LogWarning($"Unable to get player movement component from {interactor.gameObject.name}"); 
    }
}
