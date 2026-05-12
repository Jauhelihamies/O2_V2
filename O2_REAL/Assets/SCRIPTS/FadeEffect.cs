using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeEffect : MonoBehaviour
{
    public Image imageToFade;
    public float fadeDuration = 2f;
    public float startDelay = 1f;   // UUSI: Viive sekunteina ennen aloitusta

    void Start()
    {
        if (imageToFade != null)
        {
            // Asetetaan kuva nollaan heti alussa
            Color tempColor = imageToFade.color;
            tempColor.a = 0f;
            imageToFade.color = tempColor;

            StartCoroutine(FadeIn());
        }
    }

    IEnumerator FadeIn()
    {
        // Odotetaan haluttu aika ennen kuin jatketaan koodin suoritusta
        yield return new WaitForSeconds(startDelay);

        float alpha = 0f;
        while (alpha < 1f)
        {
            alpha += Time.deltaTime / fadeDuration;

            Color tempColor = imageToFade.color;
            tempColor.a = Mathf.Clamp01(alpha);
            imageToFade.color = tempColor;

            yield return null;
        }
    }
}