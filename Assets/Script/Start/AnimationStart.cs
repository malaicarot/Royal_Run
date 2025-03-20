using UnityEngine;

public class AnimationStart : MonoBehaviour
{
    Animator animator;
    [SerializeField] float duration = 0.8f;
    [SerializeField] float currentTime = 0;
    [SerializeField] float timer = 0;
    [SerializeField] float blendFloat = 8f;

    void Start()
    {
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        if (currentTime <= blendFloat) // 1 is properties blend change
        {
            timer += Time.deltaTime;
            currentTime = Mathf.Lerp(0f, blendFloat, timer / duration);
            animator.SetFloat("Blend", currentTime);
        }
    }
}
