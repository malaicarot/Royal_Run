using UnityEngine;

public class ObstacleDestroyTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Falling")){
            Destroy(other.gameObject);
        }
    }
}
