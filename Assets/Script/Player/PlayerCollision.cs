using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    const string hitString = "Hit";
    [SerializeField] Animator animator;
    LevelGenerator levelGenerator;
    float countDown = 1f;
    float timer = 0;

    void Start()
    {
        levelGenerator = FindFirstObjectByType<LevelGenerator>();
    }

    void Update()
    {
        timer += Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (timer < countDown) return;
        levelGenerator.ChangeMoveSpeed(-2);
        animator.SetTrigger(hitString);
        timer = 0;
    }
}
