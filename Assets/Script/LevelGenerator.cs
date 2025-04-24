using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    [SerializeField] ObjectPool chunkObjPool;

    [Header("References")]
    [SerializeField] CameraLensFOV cameraController;
    [SerializeField] string[] chunkPrefabs;
    [SerializeField] string chunkCheckPointPrefab;
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
            playerMovement.ChangeJumpForce(speedAmount);
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
                Chunk chunk = chunkObject.GetComponent<Chunk>();
                chunk.ReturnToPool();
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

    string ChunkToSpawn()
    {
        if (chunkCount % chunkInterval == 0 && chunkCount != 0)
        {
            chunkCount = 0;
            return chunkCheckPointPrefab;
        }
        string chunkToSpanw = chunkPrefabs[Random.Range(0, chunkPrefabs.Length)];
        return chunkToSpanw;
    }

    void SpawnChunk(float zValue)
    {
        chunkCount++;
        Vector3 direction = new Vector3(0, 0, zValue);
        PooledObject newchunkGO = chunkObjPool.GetPooledObject(ChunkToSpawn());

        newchunkGO.transform.position = direction;
        newchunkGO.transform.rotation = Quaternion.identity;
        newchunkGO.transform.SetParent(chunkParent);
        chunks.Add(newchunkGO.gameObject);
        Chunk newChunk = newchunkGO.GetComponent<Chunk>();
        newChunk.Init(this);
    }
}
