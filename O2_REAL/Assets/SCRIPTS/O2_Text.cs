using UnityEngine;

public class O2_Text : MonoBehaviour


{
    [Header("Asetukset")]
    [Tooltip("Liikenopeus (yksikköä sekunnissa)")]
    public float nopeus = 0.5f;

    [Tooltip("Kuinka monta yksikköä kuva laskeutuu alaspäin")]
    public float matka = 2.0f;

    private Vector3 aloitusPiste;
    private Vector3 kohdePiste;
    private bool liikkuu = true;

    void Start()
    {
        // Tallennetaan kuvan alkuperäinen sijainti
        aloitusPiste = transform.position;

        // Lasketaan kohdepiste vähentämällä matka Y-akselilta
        kohdePiste = aloitusPiste + Vector3.down * matka;
    }

    void Update()
    {
        if (liikkuu)
        {
            // Liikutetaan kuvaa kohti kohdepistettä tasaisella nopeudella
            transform.position = Vector3.MoveTowards(transform.position, kohdePiste, nopeus * Time.deltaTime);

            // Pysäytetään liike, kun kohde on saavutettu
            if (transform.position == kohdePiste)
            {
                liikkuu = false;
            }
        }
    }
}