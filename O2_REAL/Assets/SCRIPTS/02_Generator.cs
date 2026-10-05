using UnityEngine;
using UnityEngine.SceneManagement;

public class HappiManager : MonoBehaviour
{
    // Singleton-viittaus, jonka avulla muut skriptit p‰‰sev‰t t‰h‰n k‰siksi
    public static HappiManager Instance { get; private set; }

    [Header("UI & Visuals")]
    [SerializeField] private SpriteRenderer happiMittariSprite;
    private Vector3 mittarinAlkuperainenKoko;

    [Header("Oxygen Settings")]
    private float maxHappi =300f;
    private float nykyinenHappi = 300f;
    private float maxTuottoKatto = 40f;
    private float hapentuotto = 0f;
    private float hapenKulutus = 0f;
    private float tulo = 0f;

    public int CurrentNPC { get; private set; } = 0;
    private int aktiivisetHappiPrefabit = 0;

    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        hapentuotto += 0.5f;
        if (happiMittariSprite != null)
        {
            mittarinAlkuperainenKoko = happiMittariSprite.transform.localScale;
        }
        LaskeTulo();

    }


    public void GetNewNpc()
    {
        CurrentNPC++;
        hapenKulutus += 1f; 
        LaskeTulo();
    }

    public void NpcKilled()
    {
        CurrentNPC--;
        hapenKulutus -= 1f;

        if (hapenKulutus < 0) hapenKulutus = 0;
        LaskeTulo();
    }

   
    public void RegisterOxygenGenerator()
    {
        aktiivisetHappiPrefabit++;

        hapentuotto += 4f;
        LaskeTulo();
    }


    public void LaskeTulo()
    {
        tulo = hapentuotto - hapenKulutus;

        if (tulo > maxTuottoKatto)
        {
            tulo = maxTuottoKatto;
        }
    }

    void Update()
    {
        nykyinenHappi += tulo * Time.deltaTime;
        nykyinenHappi = Mathf.Clamp(nykyinenHappi, 0f, maxHappi);

        PaivitaMittarinKoko();

        if (nykyinenHappi <= 0)
        {
            SceneManager.LoadScene(2); // Game Over
        }
    }

    private void PaivitaMittarinKoko()
    {
        if (happiMittariSprite != null)
        {
            float happiProsentti = nykyinenHappi / maxHappi;

            happiMittariSprite.transform.localScale = new Vector3(
                mittarinAlkuperainenKoko.x * happiProsentti,
                mittarinAlkuperainenKoko.y,
                mittarinAlkuperainenKoko.z
            );
        }
    }
}