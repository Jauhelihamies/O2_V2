using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnOnClick : MonoBehaviour
{

    [SerializeField] private GameObject objekti1Prefab;
    [SerializeField] private GameObject objekti2Prefab;


    void Update()
    {
        // Kun painetaan numeronäppäintä 1
        if (Keyboard.current != null && Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            SpawnObject(objekti1Prefab);
        }

        // Kun painetaan numeronäppäintä 2
        if (Keyboard.current != null && Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            SpawnObject(objekti2Prefab);
        }
    }

    // Erillinen apufunktio, joka hoitaa objektin luomisen turvallisesti
    private void SpawnObject(GameObject prefab)
    {
        if (prefab != null)
        {
            // Luodaan objekti tämän kyseisen GameObjectin sijaintiin ja rotaatioon
            Instantiate(prefab, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Prefabia ei ole asetettu Inspectorissa!");
        }
    }
}