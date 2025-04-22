using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] GameObject fencePrefab;
    [SerializeField] GameObject applePrefab;
    [SerializeField] GameObject coinPrefab;

    LevelGenerator levelGenerator;


    [SerializeField] float[] lanes = { -2.5f, 0, 2.5f };
    List<int> availableLane = new List<int>() { 0, 1, 2 };

    [SerializeField] float coinPercentage = 0.5f;
    [SerializeField] float applePercentage = 0.3f;
    [SerializeField] float zPositionValueCoin = 2;

    void Start()
    {
        SpawnFences();
        SpawnApples();
        SpawnCoins();
    }

    public void Init(LevelGenerator levelGenerator)
    {
        this.levelGenerator = levelGenerator;
    }

    void SpawnFences()
    {
        int fenceTospawn = Random.Range(0, lanes.Length);

        for (int i = 0; i < fenceTospawn; i++)
        {
            if (availableLane.Count <= 0) break;
            int selectedLane = AvailablePosition();
            Vector3 spawnPosition = new Vector3(lanes[selectedLane], transform.position.y, transform.position.z);
            Instantiate(fencePrefab, spawnPosition, Quaternion.identity, this.transform);
        }
    }

    void SpawnApples()
    {
        if (availableLane.Count <= 0 || Random.value > applePercentage) return;
        int selectedLane = AvailablePosition();
        Vector3 spawnPosition = new Vector3(lanes[selectedLane], transform.position.y, transform.position.z);
        
        ItemMarkPool item = ItemPool.ItemPoolSingleton.GetItem(applePrefab.name, spawnPosition, Quaternion.identity);
        Apple newApple = item.GetComponent<Apple>();
        newApple.transform.SetParent(this.transform);
        newApple.Init(levelGenerator);
    }

    void SpawnCoins()
    {
        if (availableLane.Count <= 0 || Random.value > coinPercentage) return;
        int maxCoins = 6;
        int coinTospawn = Random.Range(0, maxCoins);
        float topOffChunkZPos = transform.position.z + (zPositionValueCoin * 2f);
        int selectedLane = AvailablePosition();
        for (int i = 0; i < coinTospawn; i++)
        {
            float zPositionSpawn = topOffChunkZPos - (i * zPositionValueCoin);
            Vector3 spawnPosition = new Vector3(lanes[selectedLane], transform.position.y, zPositionSpawn);

            ItemMarkPool item = ItemPool.ItemPoolSingleton.GetItem(coinPrefab.name, spawnPosition, Quaternion.identity);
            Coin newCoin = item.GetComponent<Coin>();
            newCoin.transform.SetParent(this.transform);
        }
    }


    int AvailablePosition()
    {
        int randomXIndex = Random.Range(0, availableLane.Count);
        int selectedLane = availableLane[randomXIndex];
        availableLane.RemoveAt(randomXIndex);
        return selectedLane;
    }
}
