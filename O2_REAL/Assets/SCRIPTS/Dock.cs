using UnityEngine;
using UnityEngine.Events;

public class Dock : MonoBehaviour
{
    private bool OnkoEnergiaa = false;
    private bool OnkoJohtoKiinni = false;
   
    public void CableConnection()
    {
        OnkoJohtoKiinni = true;

    }


    public void GridConnection()
    {
        OnkoEnergiaa = true;
        Debug.Log("energia liikkuu");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("O2"))
        {
            if (OnkoEnergiaa == true && OnkoJohtoKiinni && OnkoEnergiaa == true)
            {
                other.gameObject.GetComponent<Rotate>().Energy();
            }
        }



    }


}
