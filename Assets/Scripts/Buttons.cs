using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    [Header("Menus")]
    public GameObject titleMenu;
    public GameObject settingsMenu;
    public GameObject creditsMenu;

    [Header("Scriptable Objects")]
    public AudioManager am;
    public HelperScript helper;

    [Header("UI")]
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    // States
    public ActiveUI activeUI;

    public enum ActiveUI
    {
        title,
        settings,
        credits
    }

    void Start()
    {
        am.ChangeMusic(am.title);
    }

    void Update()
    {
        ScreenManager();
    }

    void ScreenManager()
    {
        if (activeUI == ActiveUI.title)
        {
            titleMenu.SetActive(true);
            settingsMenu.SetActive(false);
            creditsMenu.SetActive(false);
        }
        else if (activeUI == ActiveUI.settings)
        {
            titleMenu.SetActive(false);
            settingsMenu.SetActive(true);
            creditsMenu.SetActive(false);
        }
        else if (activeUI == ActiveUI.credits)
        {
            titleMenu.SetActive(false);
            settingsMenu.SetActive(false);
            creditsMenu.SetActive(true);
        }
    }

    public void Title()
    {
        activeUI = ActiveUI.title;
    }

    public void Settings()
    {
        activeUI = ActiveUI.settings;
    }

    public void Credits()
    {
        activeUI = ActiveUI.credits;
    }

    public void Play()
    {
        helper.ChangeScene("Level1");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        am.audioMixer.SetFloat("music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        am.audioMixer.SetFloat("sfx", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }

    public void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");

        SetMusicVolume();
        SetSFXVolume();
    }

}
