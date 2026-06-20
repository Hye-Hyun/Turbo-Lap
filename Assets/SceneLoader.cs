using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Image fadeImage;

    private bool isLoading = false;

    // Update is called once per frame
    void Update()
    {
        if (isLoading) return;

        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            StartCoroutine(LoadScene());
        }
    }

    private IEnumerator LoadScene()
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

        SceneManager.LoadScene("Ready Scene");
    }
}
