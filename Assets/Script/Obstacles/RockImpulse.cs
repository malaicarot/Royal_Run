using Unity.Cinemachine;
using UnityEngine;

public class RockImpulse : MonoBehaviour
{
    [SerializeField] ParticleSystem impulseParticle;
    [SerializeField] float shakeModifier = 10f;
    CinemachineImpulseSource cinemachineImpulseSource;
    AudioSource audioSource;
    float collisionCoolDownTime = 1f;
    float timeActive = 1f;
 
    void Awake()
    {
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        collisionCoolDownTime += Time.deltaTime;
    }


    void OnCollisionEnter(Collision collision)
    {
        if(collisionCoolDownTime < timeActive) return;
        FireImpulse();
        RockCollisionFX(collision);
        collisionCoolDownTime = 0;
    }

    void FireImpulse()
    {
        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        float shakeSensity = (1 / distance) * shakeModifier;
        shakeSensity = Mathf.Min(shakeSensity, 1f);
        cinemachineImpulseSource.GenerateImpulse(shakeSensity);
    }

    void RockCollisionFX(Collision collision)
    {
        ContactPoint contactPoint = collision.contacts[0];
        impulseParticle.transform.position = contactPoint.point;
        PlayVFX();

    }
    void PlayVFX()
    {
        impulseParticle.Play();
        audioSource.Play();
    }
}
