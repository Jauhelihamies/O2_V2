using UnityEngine;

public class PrefabManager : MonoBehaviour
{

    public static PrefabManager Instance { get; private set; }


    public GameObject prefabCoins;
    public GameObject prefabO2;


    public GameObject ValittuPrefab { get; private set; }

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
        if (tyyppi == 1)
        {
            ValittuPrefab = prefabCoins;

        }
        else if (tyyppi == 2)
        {
            ValittuPrefab = prefabO2;

        }
    }
}