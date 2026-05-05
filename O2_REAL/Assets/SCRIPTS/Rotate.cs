using System.Security.Cryptography;
using UnityEngine;

public class Rotate : MonoBehaviour
{

    public float nopeus = 100f;

    public Vector3 suunta = new Vector3(0,1,0); 

    void Update()
    {

        transform.Rotate(suunta * nopeus * Time.deltaTime);
    }
}
