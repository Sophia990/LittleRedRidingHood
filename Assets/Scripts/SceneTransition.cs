using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1.0f;

    private void Start()
    {
        // Start the scene with a fade-in effect
        StartCoroutine(FadeIn());
    }

    public void LoadScene(string sceneName)
    {
        // Start the scene transition coroutine

        StartCoroutine(SceneTransitionCoroutine(sceneName));
    }

    public void LoadSceneWithWait(string sceneName, int wait)
    {
        StartCoroutine(LoadSceneWithWaitCoroutine(sceneName, wait));
    }

    IEnumerator LoadSceneWithWaitCoroutine(string sceneName, int wait)
    {

        new WaitForSeconds(wait);

        LoadScene(sceneName);
        yield return null;
    }

    IEnumerator SceneTransitionCoroutine(string sceneName)
    {
        // Start the fade-out effect
        yield return StartCoroutine(FadeOut());

        // Load the new scene
        SceneManager.LoadScene(sceneName);

        // Start the fade-in effect
        yield return StartCoroutine(FadeIn());

    }

    IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }
    }

    IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }
    }
}
