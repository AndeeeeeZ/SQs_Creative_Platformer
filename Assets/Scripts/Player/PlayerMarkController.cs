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
        animator.SetBool("IsWalking", true);
    }

    public void StopWalking()
    {
        animator.SetBool("IsWalking", false);
        animator.SetBool("IsWalkingBackward", false); 
    }

    public void SetDirection(float direction)
    {
        sr.flipX = direction <= 0;
    }

    public void StartWalkingBack()
    {
        StartWalking(); 
        animator.SetBool("IsWalkingBackward", true); 
    }
}
