using UnityEngine;
using System.Collections;

public class PrefabManager : MonoBehaviour
{
    public static PrefabManager Instance { get; private set; }

    public GameObject prefabCoins;
    public GameObject prefabO2;

    public GameObject ValittuPrefab { get; private set; }
    private GameObject nykyinenObjekti;

    private bool isSpawning = false;
    public float spawnDelay = 1f;

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

    public void ValitsePrefab(int tyyppi)
    {
        if (tyyppi == 1) ValittuPrefab = prefabCoins;
        else if (tyyppi == 2) ValittuPrefab = prefabO2;
    }

    public void SpawnValittuPrefab(Vector3 kohdePaikka, GridCell kohdeRuutu)
    {
        if (ValittuPrefab == null || isSpawning) return;

        // Sallitaan spawn vain jos edellisen objektin luontiviive on ohi
        if (!isSpawning)
        {
            isSpawning = true;
            kohdeRuutu.AsetaVaratuksi();
            StartCoroutine(SpawnWithDelay(kohdePaikka));
        }
    }

    private IEnumerator SpawnWithDelay(Vector3 kohdePaikka)
    {
        nykyinenObjekti = Instantiate(ValittuPrefab, kohdePaikka, Quaternion.identity);

        yield return new WaitForSeconds(spawnDelay);

        isSpawning = false;
    }
}