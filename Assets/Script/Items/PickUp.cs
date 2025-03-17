using NUnit.Framework.Constraints;
using TMPro;
using UnityEngine;

public abstract class PickUp : MonoBehaviour
{
    [SerializeField] float score = 0;
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
            Destroy(gameObject);
        }
    }

    protected abstract void OnPickUp();
}
