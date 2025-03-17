using UnityEngine;

public class Apple : PickUp
{
    LevelGenerator levelGenerator;
    [SerializeField] float speedUp = 2;
    

    void Start()
    {
        levelGenerator = FindFirstObjectByType<LevelGenerator>();
    }

    protected override void OnPickUp()
    {
        levelGenerator.ChangeMoveSpeed(speedUp);
        ScoreManagers.ScoreManagerSingleton.AddScore(5);
        Debug.Log("Add 50 points!");
    }
}
