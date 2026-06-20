using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultSceneLoader : MonoBehaviour
{
    public void RetryGame()
    {
        SceneManager.LoadScene("Game Scene");
    }

    public void GoHome()
    {
        SceneManager.LoadScene("Ready Scene");
    }
}