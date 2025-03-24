using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class SoundManagers : MonoBehaviour
{
    public static SoundManagers soundManagersSingleTon;
    [SerializeField] AudioClip[] backgroudAudioClip;
    [SerializeField] AudioClip[] SFXAudioClip;

    AudioSource audioSourceMusic;
    AudioSource SFXcSource;

    public float soundVolume;
    public float SFXVolume;

    void Awake()
    {
        if (soundManagersSingleTon == null)
        {
            soundManagersSingleTon = this;
            DontDestroyOnLoad(soundManagersSingleTon);
        }
        else
        {
            Destroy(gameObject);
        }
        audioSourceMusic = GetComponents<AudioSource>()[0];
        SFXcSource = GetComponents<AudioSource>()[1];
        GetVolumeValue();

    }

    void GetVolumeValue(){
        soundVolume = PlayerPrefs.GetFloat("BackgroundMusic", 0.5f);
        SFXVolume = PlayerPrefs.GetFloat("SFXVolume", 0.5f);
        audioSourceMusic.volume = soundVolume;
        SFXcSource.volume = SFXVolume;
    }



    public void BackgroundMusic()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        StartAudio(audioSourceMusic, backgroudAudioClip[index]);

    }

    public void SFXSound(int index)
    {
        StartAudio(SFXcSource, SFXAudioClip[index]);
    }

    void StartAudio(AudioSource audioSource, AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Stop();
        audioSource.Play();
    }

    public void SettingSoundVolume(float _soundVolume)
    {
        soundVolume = _soundVolume;
        audioSourceMusic.volume = soundVolume;
        PlayerPrefs.SetFloat("BackgroundMusic", soundVolume);
        PlayerPrefs.Save();
    }

    public void SettingSFXVolume(float _soundVolume)
    {
        SFXVolume = _soundVolume;
        SFXcSource.volume = SFXVolume;
        PlayerPrefs.SetFloat("SFXVolume", SFXVolume);
        PlayerPrefs.Save();
    }

    void StopAudio(AudioClip audioClip)
    {
        audioSourceMusic.clip = audioClip;
        if (audioSourceMusic.isPlaying)
        {
            audioSourceMusic.Stop();
        }
    }
}
