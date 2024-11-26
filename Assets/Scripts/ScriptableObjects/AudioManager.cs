using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Audio Manager", menuName = "Audio Manager/Audio Manager")]
public class AudioManager : ScriptableObject
{
    public float startingPitch;

    [Header("Music")]
    public AudioClip title;
    public AudioClip level;

    [Header("Music Manager")]
    public AudioClip currentTrack;
    public AudioClip targetTrack;

    [Header("SFX")]
    public AudioClip caveAmbience;
    public AudioClip moleDigging;
    public AudioClip waterDrips;
    public AudioClip loading;
    public AudioClip key;
    public AudioClip rock;
    public AudioClip dig;
    public AudioClip doorOpen;
    public AudioClip playerEscape;

    [Header("Mixers")]
    [SerializeField] private AudioMixer audioMixer;

    [Header("Audio sources")]
    [SerializeField] AudioSource musicSourcePref;
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSourcePref;
    [SerializeField] AudioSource sfxSource;

    [Header("UI")]
    [SerializeField] private Slider musicSlider; 
    [SerializeField] private Slider sfxSlider;

    public AudioTrack track;
    public enum AudioTrack
    {
        title, level
    }


    //TODO: Check that the music plays upon loading a different scene;


    public float SetMusicSpeed(float speed)
    {

        musicSource.pitch = speed;

        if(speed <= 1)
        {
            speed = 1;
        }
        else
        {
            speed -= 0.005f;
        }

        audioMixer.SetFloat("PitchOfMusic", 1f / speed);

        return speed;
    }


    public void ChangeMusic(AudioClip clip)
    {
        musicSource = Instantiate(musicSourcePref).GetComponent<AudioSource>();

        if (PlayerPrefs.HasKey("musicVolume"))
        {
            //LoadVolume();
        }
        else
        {
            //SetMusicVolume();
        }
        
        musicSource.clip = clip;
        musicSource.Play();

        musicSource.pitch = startingPitch;
    }

    public void RestartMusic()
    {
        if(!musicSource.enabled)
        {
            Debug.Log("Music source disabled");
        }

        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        
        sfxSource.PlayOneShot(clip);
    }

    /*public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        audioMixer.SetFloat("music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }
    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        audioMixer.SetFloat("sfx", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }
    public void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");

        SetMusicVolume();
        SetSFXVolume();
    }*/
}
