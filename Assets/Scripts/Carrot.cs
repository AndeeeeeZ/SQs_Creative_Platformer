using UnityEngine;

public class Carrot : MonoBehaviour
{
    [SerializeField] private GameObject parent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            OnInteract(); 
    }

    public void OnInteract()
    {
        Debug.Log("Collected carrot"); 
        Destroy(parent); 
    }
}
