using UnityEngine;

public class ResultsButtons : MonoBehaviour
{
    [Header("Menus")]
    public GameObject resultsScreen;

    [Header("Results Text")]
    public GameObject winText;
    public GameObject loseText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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


}
