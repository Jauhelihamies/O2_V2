using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkeneLataaja : MonoBehaviour
{
    public string skenenNimi;
    public float viive = 2.0f;
    public bool lataaHetiKaynnistyksessa = false;

    private void Start()
    {
        if (lataaHetiKaynnistyksessa)
        {
            AloitaLataus();
        }
    }
    public void AloitaLataus()
    {
        if (!string.IsNullOrEmpty(skenenNimi))
        {
            StartCoroutine(LataaSkeneViiveella());
        }
        else
        {

        }
    }

    private IEnumerator LataaSkeneViiveella()
    {
        yield return new WaitForSeconds(viive);
        SceneManager.LoadScene(skenenNimi);
    }
}