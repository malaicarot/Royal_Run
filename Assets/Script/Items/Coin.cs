using TMPro;
using UnityEngine;

public class Coin : PickUp
{
    protected override void OnPickUp()
    {
        GameManagers.ManagerSingleton.AddScore(10);
        SoundManagers.soundManagersSingleTon.SFXSound(1);
    }
}
