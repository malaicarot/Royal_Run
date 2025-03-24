using UnityEngine;

public class AnimationEnd : MonoBehaviour
{
    Animator animator;
    void Awake()
    {
        animator = GetComponent<Animator>();
        if(animator == null){
            Debug.Log("asddas");
        }
    }

    public void HappyAnimation()
    {
        animator.SetBool("IsHappy", true);
    }

}
