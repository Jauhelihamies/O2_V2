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
        int satunnainenIndeksi = Random.Range(0, tekstit.Length);


        string valittuTeksti = tekstit[satunnainenIndeksi];





        if (uiTeksti != null)
        {
            uiTeksti.text = valittuTeksti;
        }
    }
}
