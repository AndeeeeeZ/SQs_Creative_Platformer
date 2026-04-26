using UnityEngine;

public class Stair : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform teleportTransform; // position + rotation
    [SerializeField] private Stair correspondingStair; 
    [SerializeField] private GameObject[] UI;
    [SerializeField] private PlayerMovement.Direction directionWhenTeleport;

    private void Awake()
    {
        SetUIActiveness(false);
    }
    public void OnEnterRange()
    {
        SetUIActiveness(true);
    }

    public void OnExitRange()
    {
        SetUIActiveness(false);
    }

    public void SetUIActiveness(bool v)
    {
        foreach (GameObject u in UI)
        {
            u.SetActive(v); 
        }
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
