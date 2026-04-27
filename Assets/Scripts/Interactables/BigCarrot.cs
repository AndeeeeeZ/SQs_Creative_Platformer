using UnityEngine;

public class BigCarrot : MonoBehaviour, IInteractable
{
    [SerializeField] private int clickRequired; 
    [SerializeField] private int particleAmount; 
    private int currentClick;
    private ParticleSystem particle;

    private void Awake()
    {
        currentClick = 0; 
        particle = GetComponent<ParticleSystem>(); 
    }
    public void OnEnterRange()
    {
    }

    public void OnExitRange()
    {

    }

    public void OnInteract(Transform interactor)
    {
        currentClick++;
        particle.Emit(particleAmount); 
        if (currentClick >= clickRequired)
        {
            Destroy(gameObject); 
        }
    }
}
