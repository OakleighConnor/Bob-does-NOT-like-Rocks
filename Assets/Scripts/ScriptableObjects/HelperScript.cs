using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Helper Script", menuName = "Helper /Helper")]
public class HelperScript : ScriptableObject
{
    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}