using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    const string hitString = "Hit";
    [SerializeField] Animator animator;
    [SerializeField] float speedDown = -2;
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
        levelGenerator.ChangeMoveSpeed(speedDown);
        animator.SetTrigger(hitString);
        GameManagers.ManagerSingleton.AddScore(-5);
        timer = 0;
    }
}
