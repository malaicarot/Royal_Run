using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraLensFOV : MonoBehaviour
{
    CinemachineCamera cinemachineCamera;
    [SerializeField] float minFOV = 20f;
    [SerializeField] float maxFOV = 120f;
    [SerializeField] float zoomDuration = 1f;
    [SerializeField] float zoomSpeedModier = 5f;
    [SerializeField] ParticleSystem speedUpParticleSystem;


    void Awake()
    {
        cinemachineCamera = GetComponent<CinemachineCamera>();
    }


    public void ChangeCameraFOV(float speedAmount)
    {
        StopAllCoroutines();
        StartCoroutine(ChangeCameraFOVRoutine(speedAmount * zoomSpeedModier));
        if(speedAmount > 0){
            speedUpParticleSystem.Play();
        }
    }

    IEnumerator ChangeCameraFOVRoutine(float speedAmount)
    {
        float startFOV = cinemachineCamera.Lens.FieldOfView;
        float TargerFOV = Mathf.Clamp(startFOV + speedAmount, minFOV, maxFOV);
        float escape = 0;

        while (escape < zoomDuration)
        {
            escape += Time.deltaTime;
            cinemachineCamera.Lens.FieldOfView = Mathf.Lerp(startFOV, TargerFOV, escape / zoomDuration);
            yield return null;
        }
        cinemachineCamera.Lens.FieldOfView = TargerFOV;
    }
}
