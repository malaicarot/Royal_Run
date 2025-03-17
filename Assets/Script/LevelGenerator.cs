using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] CameraLensFOV cameraController;
    [SerializeField] GameObject chunkPrefab;
    [SerializeField] Transform chunkParent;
    [SerializeField] int chunkQuantity = 12;
    [SerializeField] int chunkDistance = 10;

    List<GameObject> chunks = new List<GameObject>();
    [SerializeField] float movementSpeed = 8f;
    [SerializeField] float minMovementSpeed = 2f;




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
        movementSpeed += speedAmount;
        if (movementSpeed <= minMovementSpeed)
        {
            movementSpeed = minMovementSpeed;
        }
        Physics.gravity = new Vector3(Physics.gravity.x, Physics.gravity.y, Physics.gravity.z - speedAmount);
        cameraController.ChangeCameraFOV(speedAmount);
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
            if (chunkObject.transform.position.z <= Camera.main.transform.position.z)
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

    void SpawnChunk(float zValue)
    {
        Vector3 direction = new Vector3(0, 0, zValue);
        GameObject newchunk = Instantiate(chunkPrefab, direction, Quaternion.identity, chunkParent);
        chunks.Add(newchunk);
    }
}
