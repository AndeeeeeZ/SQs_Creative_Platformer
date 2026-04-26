using UnityEngine;

public class Carrot : MonoBehaviour, IInteractable
{
    public void OnEnterRange()
    {
    }

    public void OnExitRange()
    {
    }

    public void OnInteract()
    {
        Debug.Log("Collected carrot"); 
        Destroy(gameObject); 
    }
}
