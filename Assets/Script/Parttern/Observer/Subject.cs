using System.Collections.Generic;
using UnityEngine;

public abstract class Subject : MonoBehaviour
{
    List<IObserver> observers = new List<IObserver>();

    public void AddObserver(IObserver observer)
    {
        observers.Add(observer);
    }
    public void RemoveObserver(IObserver observer)
    {
        observers.Remove(observer);
    }

    protected void OnNotifyObserver(PlayerAction playerAction)
    {
        observers.ForEach((observers) =>{
            observers.OnNotify(playerAction);
        });
    }

}
