using UnityEngine;

public class Cable : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("CableDock"))
        {
            other.gameObject.GetComponent<Dock>().CableConnection();
        }



    }
}
