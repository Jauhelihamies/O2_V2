using System.Security.Cryptography;
using UnityEngine;

public class Rotate : MonoBehaviour
{

    public float HappiKerroin = 0.2f;
    private float Energia = 10f;
    private bool OnkoSähköä = false;
    private float Hapentuotto = 0f;



    public void Start()
    {
        OnkoSähköä=true;
    }
    public void Energy()
    {
        // ÖÖÖÖ....
        Energia += 10;

    }


    void Update()
    {
        


        if (OnkoSähköä == true)
        {
            Hapentuotto += Time.deltaTime *Energia*HappiKerroin;
            Debug.Log(Hapentuotto.ToString());
        }

    }
}
