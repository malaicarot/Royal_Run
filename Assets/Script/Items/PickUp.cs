
using UnityEngine;

public abstract class PickUp : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 100f;
    const string playerTag = "Player";
    void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            OnPickUp();
            PooledObject pooledObject = this.GetComponent<PooledObject>();
            pooledObject.Release();
        }
    }

    protected abstract void OnPickUp();
}
