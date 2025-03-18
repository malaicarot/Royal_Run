using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] obstacle;
    [SerializeField] Transform parent;
    [SerializeField] float waitTime = 1;
    [SerializeField] float minObstacleSpawnTime = 0.2f;
    [SerializeField] float xPosition = 2;


    void Start()
    {
        Spawner();
    }

    void Spawner(){
        StartCoroutine(WaitForSpawn());
    }

    public void DecreaseObstacleSpawnTime(float amount){
        waitTime -= amount;
        if(waitTime < minObstacleSpawnTime){
            waitTime = minObstacleSpawnTime;
        }
    }
    IEnumerator WaitForSpawn(){
        while (true)
        {
            GameObject obstaclePrefabs = obstacle[Random.Range(0, obstacle.Length)];
            float randomX = Random.Range(-xPosition, xPosition);
            Vector3 spawnPosition = new Vector3(randomX, transform.position.y, transform.position.z);
            Instantiate(obstaclePrefabs, spawnPosition, Random.rotation, parent);
            yield return new WaitForSeconds(waitTime);
        }
    }
}
