using UnityEngine;

public class PlayerMarkController : MonoBehaviour
{
    private SpriteRenderer sr;
    private Animator animator; 

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>(); 
        animator = GetComponent<Animator>(); 
    }

    public void StartWalking()
    {
        animator.Play("Walk"); 
    }

    public void StopWalking()
    {
        animator.Play("Idle"); 
    }

    public void SetDirection(float direction)
    {
        sr.flipX = direction <= 0; 
    }
}
