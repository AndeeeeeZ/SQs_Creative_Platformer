using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    public UnityEvent OnTriggered;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return; 
        
        OnTriggered?.Invoke(); 
        gameObject.SetActive(false); 
    }
}
