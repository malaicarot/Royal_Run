using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] obstacle;
    [SerializeField] Transform parent;
    [SerializeField] int quantity = 5;
    [SerializeField] float waitTime = 1;
    [SerializeField] float xPosition = 2;


    void Start()
    {
        Spawner();
    }

    void Spawner(){
        StartCoroutine(WaitForSpawn());
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
