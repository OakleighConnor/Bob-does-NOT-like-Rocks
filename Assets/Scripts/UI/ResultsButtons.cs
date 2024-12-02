using UnityEngine;
using UnityEngine.Video;

public class ResultsButtons : MonoBehaviour
{
    public GameObject pauseButton;

    [Header("Menus")]
    public GameObject resultsScreen;
    public GameObject pauseMenu;

    [Header("Results Text")]
    public GameObject winText;
    public GameObject loseText;

    [Header("References")]
    public PlayerScript player;

    void Start()
    {
        player = FindAnyObjectByType<PlayerScript>();
    }

    public void GameOver()
    {
        OpenMenu();
        winText.SetActive(false);
        loseText.SetActive(true);
    }

    public void Win()
    {
        OpenMenu();
        winText.SetActive(true);
        loseText.SetActive(false);
    }

    public void OpenMenu()
    {
        resultsScreen.SetActive(true);
    }

    public void OpenPause()
    {
        pauseButton.SetActive(false);
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        player.stop = true;
    }   

    public void ClosePause()
    {
        pauseButton.SetActive(true);
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        player.stop = false;
    }
}
