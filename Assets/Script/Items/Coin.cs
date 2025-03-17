using TMPro;
using UnityEngine;

public class Coin : PickUp
{
    protected override void OnPickUp()
    {
        ScoreManagers.ScoreManagerSingleton.AddScore(10);
    }
}
