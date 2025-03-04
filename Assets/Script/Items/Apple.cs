using UnityEngine;

public class Apple : PickUp
{
    LevelGenerator levelGenerator;

    void Start()
    {
        levelGenerator = FindFirstObjectByType<LevelGenerator>();
    }

    protected override void OnPickUp()
    {
        levelGenerator.ChangeMoveSpeed(2);
        Debug.Log("Add 50 points!");
    }
}
