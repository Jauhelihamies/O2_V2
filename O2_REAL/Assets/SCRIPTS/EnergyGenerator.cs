using Unity.VisualScripting;
using UnityEngine;

public class EnergyGenerator : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("CableDock"))
        {
            Debug.Log("found");
            other.gameObject.GetComponent<Dock>().GridConnection();

        }
            

        
    }
}
