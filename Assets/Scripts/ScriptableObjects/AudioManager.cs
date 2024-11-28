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
    public AudioMixer audioMixer;

    [Header("Audio sources")]
    [SerializeField] AudioSource musicSourcePref;
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSourcePref;
    [SerializeField] AudioSource sfxSource;

    public AudioTrack track;
    public enum AudioTrack
    {
        title, level
    }


    //TODO: Check that the music plays upon loading a different scene;


    public float SetMusicSpeed(float speed)
    {

        musicSource.pitch = speed;

        if(speed >)
        audioMixer.SetFloat("Pitch", 1f / speed);

        if (speed <= 1)
        {
            speed = 1;
        }
        else
        {
            speed -= 0.005f;
        }

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
}
