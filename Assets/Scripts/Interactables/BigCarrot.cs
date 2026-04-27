using UnityEngine;

public class BigCarrot : MonoBehaviour, IInteractable
{
    [SerializeField] private ParticleSystem[] particle;
    [SerializeField] private GameObject[] UI; 
    [SerializeField] private int clickRequired;
    [SerializeField] private int particleAmount;
    private int currentClick;


    private void Awake()
    {
        currentClick = 0;
        SetActiveness(false);
    }
    public void OnEnterRange()
    {
        SetActiveness(true); 
    }

    public void OnExitRange()
    {
        SetActiveness(false); 
    }

    private void SetActiveness(bool v)
    {
        foreach(GameObject g in UI)
            g.SetActive(v); 
    }

    public void OnInteract(Transform interactor)
    {
        currentClick++;
        foreach (ParticleSystem p in particle)
            p.Emit(particleAmount);

        if (currentClick >= clickRequired)
        {
            Destroy(gameObject);
        }
    }
}
