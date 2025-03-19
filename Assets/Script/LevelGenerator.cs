using System.Collections.Generic;
// using Unity.Burst.Intrinsics;
using UnityEngine;
// using UnityEngine.UIElements;

public class LevelGenerator : MonoBehaviour
{

    [Header("References")]
    [SerializeField] CameraLensFOV cameraController;
    [SerializeField] GameObject[] chunkPrefabs;
    [SerializeField] GameObject chunkCheckPointPrefab;
    [SerializeField] Transform chunkParent;
    [SerializeField] PlayerMovement playerMovement;

    [Header("Level Settings")]
    [Tooltip("The amount of chunks we start with")]
    [SerializeField] int chunkQuantity = 12;
    [Tooltip("Do not change chunk length value unless chunk prefab size reflects change")]
    [SerializeField] int chunkDistance = 10;
    List<GameObject> chunks = new List<GameObject>();
    [SerializeField] float movementSpeed = 8f;
    [SerializeField] float minMovementSpeed = 2f;
    [SerializeField] float maxMovementSpeed = 20f;
    [SerializeField] float minGravity = -22f;
    [SerializeField] float maxGravity = -2f;

    int chunkCount = 0;
    int chunkInterval = 8;

    void Start()
    {
        ChunkGenerator();
    }
    void Update()
    {
        ChunkMovement();
    }

    public void ChangeMoveSpeed(float speedAmount)
    {
        float newMoveSpeed = movementSpeed + speedAmount;
        newMoveSpeed = Mathf.Clamp(newMoveSpeed, minMovementSpeed, maxMovementSpeed);
        if (newMoveSpeed != movementSpeed)
        {
            playerMovement.ChangeSpeed(speedAmount);
            movementSpeed = newMoveSpeed;
            float newGravity = Physics.gravity.z - speedAmount;
            newGravity = Mathf.Clamp(newGravity, minGravity, maxGravity);
            Physics.gravity = new Vector3(Physics.gravity.x, Physics.gravity.y, newGravity);
            cameraController.ChangeCameraFOV(speedAmount);

        }
    }

    void ChunkGenerator()
    {
        for (int i = 0; i < chunkQuantity; i++)
        {
            SpawnChunk(CalculatePosition());
        }
    }

    void ChunkMovement()
    {
        for (int i = 0; i < chunks.Count; i++)
        {
            GameObject chunkObject = chunks[i];
            chunkObject.transform.Translate(-transform.forward * (movementSpeed * Time.deltaTime));
            if (chunkObject.transform.position.z <= Camera.main.transform.position.z - 5f)
            {
                chunks.Remove(chunkObject);
                Destroy(chunkObject);
                SpawnChunk(CalculatePosition());
            }

        }
    }

    float CalculatePosition()
    {
        float zPosition;

        if (chunks.Count == 0)
        {
            zPosition = transform.position.z;
        }
        else
        {

            zPosition = chunks[chunks.Count - 1].transform.position.z + chunkDistance;
        }
        return zPosition;
    }

    GameObject ChunkToSpawn()
    {
        if (chunkCount % chunkInterval == 0 && chunkCount != 0)
        {
            chunkCount = 0;
            return chunkCheckPointPrefab;
        }
        GameObject chunkToSpanw = chunkPrefabs[Random.Range(0, chunkPrefabs.Length)];
        return chunkToSpanw;
    }

    void SpawnChunk(float zValue)
    {
        chunkCount++;
        Vector3 direction = new Vector3(0, 0, zValue);
        GameObject newchunkGO = Instantiate(ChunkToSpawn(), direction, Quaternion.identity, chunkParent);
        chunks.Add(newchunkGO);
        Chunk newChunk = newchunkGO.GetComponent<Chunk>();
        newChunk.Init(this);
    }
}
