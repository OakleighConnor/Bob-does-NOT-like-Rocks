using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Helper Script", menuName = "Helper /Helper")]
public class HelperScript : ScriptableObject
{
    [Header("Scenes")]
    public string[] scenes;
    public string randomScene;
    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void RandomScene()
    {
        randomScene = scenes[Random.Range(0, scenes.Length)];
        ChangeScene(randomScene);
    }
}