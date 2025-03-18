using UnityEngine;

public class Apple : PickUp
{
    LevelGenerator levelGenerator;
    [SerializeField] float speedUp = 2;
    
    public void Init(LevelGenerator levelGenerator)
    {
        this.levelGenerator = levelGenerator;
    }

    protected override void OnPickUp()
    {
        levelGenerator.ChangeMoveSpeed(speedUp);
        // Managers.ScoreManagerSingleton.AddScore(5);
        Debug.Log("Add 50 points!");
    }
}
