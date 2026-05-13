using TMPro;
using UnityEngine;

public class Random_Quote : MonoBehaviour
{
    // Lista teksteistä, jotka näkyvät Unityn Inspector-ikkunassa
    [SerializeField] private string[] tekstit = { "Moi!", "Hei maailma!", "Tervetuloa peliin!", "Onnea matkaan!" };

    // Viittaus UI-tekstikomponenttiin
    [SerializeField] private TextMeshProUGUI uiTeksti;

    void Start()
    {
        TulostaSatunnainenTeksti();
    }


    public void TulostaSatunnainenTeksti()
    {
        if (tekstit.Length == 0)
        {
            Debug.LogWarning("Tekstitaulukko on tyhjä!");
            return;
        }


        int satunnainenIndeksi = Random.Range(0, tekstit.Length);


        string valittuTeksti = tekstit[satunnainenIndeksi];


        Debug.Log("Valittu teksti: " + valittuTeksti);


        if (uiTeksti != null)
        {
            uiTeksti.text = valittuTeksti;
        }
    }
}
