using System.Collections;
using UnityEngine;

public class SpriteFader : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 1.0f;

    private SpriteRenderer spriteRenderer;

    void Start()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {

            StartCoroutine(FadeLoop());
        }

    }

    private IEnumerator FadeLoop()
    {
        while (true)
        {

            yield return StartCoroutine(FadeSprite(1f, 0f));


            yield return StartCoroutine(FadeSprite(0f, 1f));
        }
    }

    private IEnumerator FadeSprite(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;
        Color currentColor = spriteRenderer.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;

            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);

            spriteRenderer.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);

            yield return null;
        }

        spriteRenderer.color = new Color(currentColor.r, currentColor.g, currentColor.b, endAlpha);
    }
}