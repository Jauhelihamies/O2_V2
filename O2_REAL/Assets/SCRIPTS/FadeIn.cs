using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeController : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1.0f;

    void Start()
    {
        // Start the fade-in as soon as the game begins
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float elapsedTime = 0;
        Color color = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            // Lerp from 1 (opaque) to 0 (transparent)
            color.a = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        // Ensure it's fully transparent at the end
        color.a = 0;
        fadeImage.color = color;
        // Optional: Disable the image so it doesn't block clicks
        fadeImage.gameObject.SetActive(false);
    }
}