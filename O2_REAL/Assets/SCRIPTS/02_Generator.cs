using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rotate : MonoBehaviour
{
    [SerializeField] private SpriteRenderer happiMittariSprite;
    private Vector3 mittarinAlkuperainenKoko;

    private int MAX_tuotto = 10;
    private float NykyinenHappi = 100;

    private float Energia = 10f;
    private bool OnkoSähköä = false;
    private float Hapentuotto = 0f;

    private float TULO = 0f;

    private float HapenKulutus = 0f;
    public List<Fan> kaikki = new List<Fan>();

    public int CurrentNPC = 0;

    public void Start()
    {
        if (happiMittariSprite != null)
        {
            mittarinAlkuperainenKoko = happiMittariSprite.transform.localScale;
        }

        OnkoSähköä = true;
        Energy();
    }

    public void Energy()
    {
        Energia += 10;
        foreach (Fan k in kaikki)
        {
            k.RotateFan();
        }
    }

    public void GetNewNpc()
    {
        CurrentNPC++;
        HapenKulutus += 1;
        Laske();
    }

    public void NpcKilled()
    {
        CurrentNPC--;
        HapenKulutus -= 1;
        Laske();
    }

    public void Laske()
    {
        TULO = Hapentuotto - HapenKulutus;
        if (TULO > 10)
        {
            TULO = 10;
        }
    }

    void Update()
    {
        if (OnkoSähköä == true)
        {
            if (Hapentuotto < MAX_tuotto)
            {
                Hapentuotto += Time.deltaTime * Energia;
            }
            if (Hapentuotto > MAX_tuotto)
            {
                Hapentuotto = MAX_tuotto;
            }
        }

        // Lasketaan TULO uudestaan, koska Hapentuotto kasvaa ajassa
        Laske();

        // Happi muuttuu tasaisesti joka sekunti TULO-arvon mukaan
        NykyinenHappi += TULO * Time.deltaTime;

        // Pidetään happi rajojen (0-100) sisällä
        NykyinenHappi = Mathf.Clamp(NykyinenHappi, 0f, 100f);

        // Päivitetään mittarin vaakataso (X-akseli)
        PaivitaMittarinKoko();
        if (NykyinenHappi <= 0)
        {
            SceneManager.LoadScene(2);
        }
    }

    private void PaivitaMittarinKoko()
    {
        if (happiMittariSprite != null)
        {
            float happiProsentti = NykyinenHappi / 100f;

            // Muuttaa vain X-akselia (vaakasuunta)
            happiMittariSprite.transform.localScale = new Vector3(
                mittarinAlkuperainenKoko.x * happiProsentti,
                mittarinAlkuperainenKoko.y,
                mittarinAlkuperainenKoko.z
            );
        }
    }
}