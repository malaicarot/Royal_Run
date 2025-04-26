using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : Subject
{
    const string apple = "ApplePickUp";
    const string coin = "CoinPickUp";


    void OnCollisionEnter(Collision collision)
    {
        OnNotifyObserver(PlayerAction.Blocked);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == apple)
        {
            OnNotifyObserver(PlayerAction.PickedApple);
        }
        else if (other.name == coin)
        {
            OnNotifyObserver(PlayerAction.PickedCoin);

        }

    }
}
