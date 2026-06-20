using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class ReadySceneLoader : MonoBehaviour
{
    [SerializeField] private Image fadeImage;

    private bool isLoading = false;

    public void StartGame()
    {
        Debug.Log("play button clicked");

        if (isLoading) return;

        StartCoroutine(LoadGameScene());
    }

    private IEnumerator LoadGameScene()
    {
        isLoading = true;

        float duration = 0.5f;
        float time = 0f;

        Color color = fadeImage.color;

        while (time < duration)
        {
            time += Time.deltaTime;

            color.a = Mathf.Lerp(0f, 1f, time / duration);
            fadeImage.color = color;

            yield return null;
        }

        SceneManager.LoadScene("Game Scene");
    }
}