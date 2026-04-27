using UnityEngine;

public class GroundDetection : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Floor"))
        {
            playerMovement.TryGrounding(); 
        }
    }
}
