using UnityEngine;

public class Stair : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform spawnTransform; // position + rotation
    [SerializeField] private Stair correspondingStair; 
    [SerializeField] private GameObject UI;

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

    public void OnInteract()
    {
        Debug.Log("Interacted with stair"); 
    }
}
