using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    ObstacleSpawner obstacleSpawner;
    [SerializeField] float timeDecrease = 0.2f;

    void Start()
    {
        obstacleSpawner = FindFirstObjectByType<ObstacleSpawner>();
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")){
            GameManagers.ManagerSingleton.AddTime();
            obstacleSpawner.DecreaseObstacleSpawnTime(timeDecrease);
        }
    }
}
